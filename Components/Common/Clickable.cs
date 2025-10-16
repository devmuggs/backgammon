using Godot;
using System;

public partial class Clickable : Node3D
{
	[Export] public float ClickDistance { get; private set; } = 100f;
	[Export] public bool IsClickable { get; private set; } = true;
	[Export] public float ClickCooldownSeconds { get; private set; } = 0.5f; // seconds

	public event Action OnClick;

	private Camera3D _camera;
	private World3D _world;
	private CollisionObject3D _collisionObject;
	private DateTime _lastClickTime;
	

	public override void _Ready()
	{
		base._Ready();

		_collisionObject = GetParent<CollisionObject3D>();
		if (_collisionObject == null) GD.PrintErr("Clickable must be a child of a CollisionObject3D!");

		if (_camera == null)
		{
			_camera = GetViewport().GetCamera3D();
			if (_camera == null) GD.PrintErr("Clickable could not find Camera3D!");
		}

		_world = GetWorld3D();
		if (_world == null) GD.PrintErr("Clickable could not find World3D!");

		GD.Print($"Clickable: Ready on {Name}");
	}

	private bool CheckMouseOver(Vector2 mousePosition)
	{
		Vector3 from = _camera.ProjectRayOrigin(mousePosition);
		Vector3 to = from + _camera.ProjectRayNormal(mousePosition) * 1000f;

		var spaceState = _world.DirectSpaceState;
		var query = new PhysicsRayQueryParameters3D
		{
			From = from,
			To = to,
			CollisionMask = _collisionObject.CollisionLayer // only this object's layer
		};

		var result = spaceState.IntersectRay(query);
		return result.Count > 0;
	}

	public void OnMouseClick(Vector2 mousePosition)
	{
		GD.Print($"Clickable: Mouse click at {mousePosition} on {Name}");
		if (!IsClickable) return;

		float secondsSinceLastClick = (float)(DateTime.Now - _lastClickTime).TotalSeconds;
		bool cooldownElapsed = secondsSinceLastClick >= ClickCooldownSeconds;
		GD.Print($"Clickable: Seconds since last click: {secondsSinceLastClick}, cooldownElapsed: {cooldownElapsed}");

		if (CheckMouseOver(mousePosition) && cooldownElapsed)
		{
			_lastClickTime = DateTime.Now;
			OnClick?.Invoke();
			GD.Print($"Clickable: Clicked on {Name} at {mousePosition}");
		}
	}

	public void SetClickable(bool clickable)
	{
		IsClickable = clickable;
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is not InputEventMouseButton mouseEvent) return;
		if (@event.IsActionPressed("left_click") && mouseEvent.Pressed)
		{
			OnMouseClick(mouseEvent.Position);
		}
	}
}
