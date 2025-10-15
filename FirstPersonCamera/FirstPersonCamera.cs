using Godot;
using System;

public partial class FirstPersonCamera : Camera3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
    {
		// lock the mouse cursor to the center of the screen
		Input.MouseMode = Input.MouseModeEnum.Captured;
    }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Allow flying with keyboard input
		Vector3 direction = Vector3.Zero;
		if (Input.IsPhysicalKeyPressed(Key.W))
		{
			direction -= Transform.Basis.Z;
		}
		if (Input.IsPhysicalKeyPressed(Key.S))
		{
			direction += Transform.Basis.Z;
		}
		if (Input.IsPhysicalKeyPressed(Key.A))
		{
			direction -= Transform.Basis.X;
		}
		if (Input.IsPhysicalKeyPressed(Key.D))
		{
			direction += Transform.Basis.X;
		}

		float speed = 10.0f; // Movement speed
		float sprintMultiplier = 2.0f; // Sprint speed multiplier
		if (Input.IsPhysicalKeyPressed(Key.Shift))
		{
			speed *= sprintMultiplier;
		}

		// Move the camera
		if (direction != Vector3.Zero)
		{
			Transform = Transform.Translated(direction.Normalized() * speed * (float)delta);
		}
	}

	public override void _Input(InputEvent @event)
	{
		// Control camera with mouse movement
		if (@event is InputEventMouseMotion mouseMotionEvent)
		{
			float mouseSensitivity = 0.1f;

			// Rotate the camera based on mouse movement
			RotateY(Mathf.DegToRad(-mouseMotionEvent.Relative.X * mouseSensitivity));

			// Limit vertical rotation to avoid flipping
			float verticalRotation = RotationDegrees.X - mouseMotionEvent.Relative.Y * mouseSensitivity;
			verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
			RotationDegrees = new Vector3(verticalRotation, RotationDegrees.Y, RotationDegrees.Z);
		}

		// Recapture focus on left mouse click
		if (@event is InputEventMouseButton mouseButtonEvent && mouseButtonEvent.ButtonIndex == MouseButton.Left && mouseButtonEvent.Pressed)
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}

		// Unlock the mouse cursor when Escape is pressed
		if (@event is InputEventKey keyEvent && keyEvent.Keycode == Key.Escape && keyEvent.Pressed)
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
	}
}
