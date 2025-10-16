using Godot;
using Godot.Collections;
using System;



public partial class FirstPersonCamera : Camera3D
{

	[Export] public float MouseSensitivity { get; private set; } = 0.1f;
	[Export] public float MovementSpeed { get; private set; } = 10.0f;
	[Export] public float SprintMultiplier { get; private set; } = 2.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		// lock the mouse cursor to the center of the screen
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	private void HandleKeyboardInput(double delta)
	{
		// Allow flying with keyboard input
		Vector3 direction = Vector3.Zero;
		
		if (Input.IsPhysicalKeyPressed(Key.W)) direction -= Transform.Basis.Z;
		if (Input.IsPhysicalKeyPressed(Key.S)) direction += Transform.Basis.Z;
		if (Input.IsPhysicalKeyPressed(Key.A)) direction -= Transform.Basis.X;
		if (Input.IsPhysicalKeyPressed(Key.D)) direction += Transform.Basis.X;
		if (Input.IsPhysicalKeyPressed(Key.Space)) direction += Transform.Basis.Y;
		if (Input.IsPhysicalKeyPressed(Key.Ctrl)) direction -= Transform.Basis.Y;

		float speed = MovementSpeed; // Movement speed
		float sprintMultiplier = SprintMultiplier; // Sprint speed multiplier

		if (Input.IsPhysicalKeyPressed(Key.Shift))
		{
			speed *= sprintMultiplier;
		}

		// Physically move the camera
		if (direction != Vector3.Zero)
		{
			Transform = Transform.Translated(direction.Normalized() * speed * (float)delta);
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		base._Process(delta);
		HandleKeyboardInput(delta);
	}

	private void HandleMouseMovement(InputEvent @event)
	{
		if (@event is not InputEventMouseMotion mouseMotionEvent) return;

		float mouseSensitivity = MouseSensitivity;

		// Rotate the camera based on mouse movement
		RotateY(Mathf.DegToRad(-mouseMotionEvent.Relative.X * mouseSensitivity));

		// Limit vertical rotation to avoid flipping
		float verticalRotation = RotationDegrees.X - mouseMotionEvent.Relative.Y * mouseSensitivity;
		verticalRotation = Mathf.Clamp(verticalRotation, -90, 90);
		RotationDegrees = new Vector3(verticalRotation, RotationDegrees.Y, RotationDegrees.Z);
	}

	private void HandleMouseClick(InputEvent @event)
	{
		if (@event is not InputEventMouseButton mouseButtonEvent) return;
		if (mouseButtonEvent.ButtonIndex != MouseButton.Left || !mouseButtonEvent.Pressed) return;

		// Recapture focus on left mouse click
		Input.MouseMode = Input.MouseModeEnum.Captured;
	}

	private void HandleEscapeKey(InputEvent @event)
	{
		if (@event is not InputEventKey keyEvent) return;
		if (keyEvent.Keycode != Key.Escape || !keyEvent.Pressed) return;

		// Unlock the mouse cursor when Escape is pressed
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}
	
	private void HandleScroll(InputEvent @event)
	{
		if (@event is not InputEventMouseButton mouseButtonEvent) return;
		if (mouseButtonEvent.ButtonIndex != MouseButton.WheelUp && mouseButtonEvent.ButtonIndex != MouseButton.WheelDown) return;

		
		if (mouseButtonEvent.ButtonIndex == MouseButton.WheelUp)
		{
			MovementSpeed += 1.0f; // increase movement speed
		}
		else if (mouseButtonEvent.ButtonIndex == MouseButton.WheelDown)
		{
			MovementSpeed -= 1.0f; // decrease movement speed
		}
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);

		HandleMouseMovement(@event);
		HandleMouseClick(@event);
		HandleEscapeKey(@event);
		HandleScroll(@event);
	}
}
