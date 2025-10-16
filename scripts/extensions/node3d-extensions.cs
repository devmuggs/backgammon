using Godot;

namespace Scripts.Extensions
{
    public static class Node3DExtensions
{
    public static CollisionObject3D GetCollisionObjectSafe(this Node3D node)
    {
        var obj = node.GetParent<CollisionObject3D>();
        if (obj == null)
            GD.PrintErr($"{node.Name} must be a child of CollisionObject3D!");
        return obj;
    }

    public static Camera3D GetCameraSafe(this Node3D node)
    {
        var cam = node.GetViewport().GetCamera3D();
        if (cam == null)
            GD.PrintErr($"{node.Name} could not find Camera3D!");
        return cam;
    }

    public static World3D GetWorldSafe(this Node3D node)
    {
        var world = node.GetWorld3D();
        if (world == null)
            GD.PrintErr($"{node.Name} could not find World3D!");
        return world;
    }
}

}