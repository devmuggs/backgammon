using Godot;
using System;

using Scripts.Extensions;

public partial class Clickable : Node3D
{
	[Export] public float ClickDistance { get; private set; } = 100f;
	[Export] public bool IsClickable { get; private set; } = true;
	[Export] public float ClickCooldownSeconds { get; private set; } = 0.5f; // seconds

	public event Action OnClick;

	private Camera3D _camera;
	private World3D _world;
	private CollisionObject3D _collisionObject;
	private DateTime _lastClickTime = DateTime.MinValue;

	public void SetClickable(bool clickable)
	{
		IsClickable = clickable;
	}

	public override void _Ready()
	{
		base._Ready();

		_collisionObject = this.GetCollisionObjectSafe();
		_camera = this.GetCameraSafe();
		_world = this.GetWorldSafe();

		GD.Print($"Clickable: Ready on {Name}");
	}

	public override void _Input(InputEvent @event)
	{
		base._Input(@event);
		if (@event is not InputEventMouseButton mouseEvent) return;
		if (@event.IsActionPressed("left_click") && mouseEvent.Pressed)
		{
			_OnMouseClick(mouseEvent.Position);
		}
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

	private void _OnMouseClick(Vector2 mousePosition)
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
}
