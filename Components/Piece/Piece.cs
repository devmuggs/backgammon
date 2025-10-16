using Godot;
using System;

public partial class Piece : RigidBody3D
{
	private Clickable _clickable;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		base._Ready();
		_clickable = GetNode<Clickable>("Clickable");
		if (_clickable == null) GD.PrintErr("Piece must have a Clickable child!");
		_clickable.OnClick += HandleClick;
	}

	private void HandleClick()
	{
		GD.Print($"Piece: Clicked on piece {Name}");
	}
}
