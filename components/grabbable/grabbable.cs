using Godot;
using System;

public partial class Grababble : Node3D
{
	[Export] public float GrabStrength { get; set; } = 10f;
	[Export] public float Dampening { get; set; } = 5f;
	[Export] public float MaxGrabDistance { get; set; } = 20f;
	[Export] public float HoverDistance { get; set; } = 4f;
	[Export] public float PullStrength { get; set; } = 2f;

	private Camera3D _camera;
	private RigidBody3D _rigidBody;
	private bool _isGrabbed = false;
	private Vector3 _targetPosition;
	private Vector3 _grabOffset;
	private World3D _world;
	private readonly Random _rng = new();
	private Vector3 _localGrabOffset = Vector3.Zero;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_rigidBody = GetParent<RigidBody3D>();
		if (_rigidBody == null) GD.PrintErr("Grababble must be a child of a RigidBody3D!");

		if (_camera == null)
		{
			_camera = GetViewport().GetCamera3D();
			if (_camera == null) GD.PrintErr("Grababble could not find Camera3D!");
		}
		
		_world = GetWorld3D();
		if (_world == null) GD.PrintErr("Grababble could not find World3D!");
	}

	private bool _IsMouseOver(Vector2 mousePosition)
	{
		Vector3 from = _camera.ProjectRayOrigin(mousePosition);
		Vector3 to = from + _camera.ProjectRayNormal(mousePosition) * MaxGrabDistance;

		var spaceState = _world.DirectSpaceState;
		var query = new PhysicsRayQueryParameters3D
		{
			From = from,
			To = to
		};

		var result = spaceState.IntersectRay(query);
		return result.Count > 0;
	}

	private void StartGrab(Vector3 hitPoint)
	{
		_isGrabbed = true;
		if (_rigidBody != null)
		{
			_localGrabOffset = _rigidBody.GlobalPosition - hitPoint;
		}
	}

	private Vector3? RaycastHitPointUnderMouse()
	{
		if (_camera == null || _rigidBody == null) return null;

		Vector2 mousePos = GetViewport().GetMousePosition();
		Vector3 from = _camera.ProjectRayOrigin(mousePos);
		Vector3 to = from + _camera.ProjectRayNormal(mousePos) * MaxGrabDistance;

		var spaceState = _world.DirectSpaceState;
		var query = new PhysicsRayQueryParameters3D
		{
			From = from,
			To = to,
			CollisionMask = _rigidBody.CollisionLayer // only this object's layer
		};

		var result = spaceState.IntersectRay(query);
		if (result.Count == 0) return null;

		if (result.TryGetValue("collider", out var colliderObj))
		{
			var collider = colliderObj.AsGodotObject() as RigidBody3D;
			if (collider == _rigidBody)
				return (Vector3)result["position"];
		}

		return null;
	}
	public override void _Input(InputEvent @event)
	{
		// reset position on 'r'
		if (@event is InputEventKey keyEvent && keyEvent.Keycode == Key.R && keyEvent.Pressed)
		{
			if (_rigidBody != null)
			{
				_rigidBody.GlobalPosition = Vector3.Zero;
				_rigidBody.LinearVelocity = Vector3.Zero;
				_rigidBody.AngularVelocity = Vector3.Zero;
			}
			return;
		}

		if (@event is not InputEventMouseButton mouseEvent || !mouseEvent.IsAction("left_click")) return;
		GD.Print($"Mouse Event: {mouseEvent.ButtonIndex}, Pressed: {mouseEvent.Pressed}");

		if (mouseEvent.IsPressed() && !_isGrabbed)
		{
			Vector3? hitPoint = RaycastHitPointUnderMouse();
			if (hitPoint.HasValue)
				StartGrab(hitPoint.Value);
		}
		else if (mouseEvent.IsReleased() && _isGrabbed)
		{
			GD.Print("Released grab");
			_isGrabbed = false;
			_localGrabOffset = Vector3.Zero;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _PhysicsProcess(double delta)
	{
		if (!_isGrabbed) return;
		if (_rigidBody == null || _camera == null) return;

		Vector2 mousePos = GetViewport().GetMousePosition();
		Vector3 from = _camera.ProjectRayOrigin(mousePos);
		Vector3 dir = _camera.ProjectRayNormal(mousePos);

		// Current distance from camera
		float distanceFromCamera = (_rigidBody.GlobalPosition - from).Length();

		// Move toward camera but no closer than HoverDistance
		float offsetDistance = Mathf.Max(distanceFromCamera - PullStrength, HoverDistance);
		Vector3 targetGrabPoint = from + dir * Mathf.Min(offsetDistance, MaxGrabDistance);

		// Compute center position so the grabbed point hits targetGrabPoint
		Vector3 targetCenter = targetGrabPoint + _localGrabOffset;

		// displacement vector for velocity
		Vector3 displacement = targetCenter - _rigidBody.GlobalPosition;
		if (!displacement.IsFinite()) return;

		// gravity-gun style velocity
		Vector3 desiredVelocity = displacement * (float)delta * GrabStrength;
		_rigidBody.LinearVelocity = (_rigidBody.LinearVelocity * (1f - Dampening * (float)delta)) + desiredVelocity;

		// // small torque to make the object feel alive
		// _rigidBody.ApplyTorqueImpulse(new Vector3(
		// 	(float)(_rng.NextDouble() * 2 - 1),
		// 	(float)(_rng.NextDouble() * 2 - 1),
		// 	(float)(_rng.NextDouble() * 2 - 1)
		// ) * 0.02f);
	}
}
