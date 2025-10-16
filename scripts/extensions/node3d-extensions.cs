using Godot;

namespace Scripts.Extensions
{
    /// <summary>
    /// <para>Extension methods for Node3D to safely get common related nodes like CollisionObject3D, Camera3D, and World3D.</para>
    /// <para>Node3Dの拡張メソッドで、CollisionObject3D、Camera3D、World3Dなどの関連ノードを安全に取得します。</para>
    /// </summary>
    public static class Node3DExtensions
    {
        /// <summary>
        /// Safely gets the CollisionObject3D parent of the node.
        /// <para>ノードのCollisionObject3D親を安全に取得します。</para>
        /// </summary>
        public static CollisionObject3D GetCollisionObjectSafe(this Node3D node)
        {
            var obj = node.GetParent<CollisionObject3D>();
            if (obj == null)
                GD.PrintErr($"{node.Name} must be a child of CollisionObject3D!");
            return obj;
        }

        /// <summary>
        /// Safely gets the Camera3D from the node's viewport.
        /// <para>ノードのビューポートからCamera3Dを安全に取得します。</para>
        /// </summary>
        public static Camera3D GetCameraSafe(this Node3D node)
        {
            var cam = node.GetViewport().GetCamera3D();
            if (cam == null)
                GD.PrintErr($"{node.Name} could not find Camera3D!");
            return cam;
        }

        /// <summary>
        /// Safely gets the World3D associated with the node.
        /// <para>ノードに関連付けられたWorld3Dを安全に取得します。</para>
        /// </summary>
        public static World3D GetWorldSafe(this Node3D node)
        {
            var world = node.GetWorld3D();
            if (world == null)
                GD.PrintErr($"{node.Name} could not find World3D!");
            return world;
        }
    }
}