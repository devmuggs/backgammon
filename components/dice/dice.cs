using Godot;
using System;
using System.Diagnostics;

public partial class Dice : RigidBody3D
{
	[Export] public float RollStrength { get; set; } = 10f;
	[Export] public float MaxAngularVelocity { get; set; } = 10f;
	[Signal] public delegate void RolledEventHandler(int value);

	private readonly Random rng = new();
	private readonly float upwardForce = 3f;
	
	public void Roll()
	{
		var direction = new Vector3(
			(float)(rng.NextDouble() * 2 - 1),
			upwardForce,
			(float)(rng.NextDouble() * 2 - 1)
		).Normalized();
		
		var torque = new Vector3(
			(float)(rng.NextDouble() * 2 - 1),
			(float)(rng.NextDouble() * 2 - 1),
			(float)(rng.NextDouble() * 2 - 1)
		) * (RollStrength);

		ApplyImpulse(Vector3.Zero, direction * RollStrength);
		ApplyTorqueImpulse(torque);

		var rollInt = rng.Next(1, 7); // Example: 1–6 for dice
		Debug.WriteLine($"Dice rolled: {rollInt}");

		EmitSignal(SignalName.Rolled, rollInt);
	}

	public override void _Ready()
	{
		AngularDamp = 1.0f;
		LinearDamp = 0.2f;
		Mass = 1.0f;
	}

	public override void _PhysicsProcess(double delta)
	{
		if (LinearVelocity.Length() < 0.1f && AngularVelocity.Length() < 0.1f)
		{
			LinearVelocity = Vector3.Zero;
			AngularVelocity = Vector3.Zero;
		}

		// clamp the angular velocity to prevent excessive spinning
		if (AngularVelocity.Length() > MaxAngularVelocity)
		{
			AngularVelocity = AngularVelocity.Normalized() * MaxAngularVelocity;
		}
	}
	
	public override void _Input(InputEvent @event)
	{
		if (@event.IsActionPressed("roll_dice"))
		{
			Roll();
		}
	}
}
