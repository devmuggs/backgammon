using Godot;

/// <summary>
/// A Piece component representing a game piece in a board game.
/// <para>ボードゲームのゲームピースを表すPieceコンポーネントです。</para>
/// </summary>
public partial class Piece : RigidBody3D
{
	private Clickable _clickable;

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
