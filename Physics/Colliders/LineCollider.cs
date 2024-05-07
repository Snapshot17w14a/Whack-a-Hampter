using System;

namespace GXPEngine.Physics.Colliders
{
    internal class LineCollider : Collider
    {
        public Vec2 StartPosition { get; protected set; }
        public Vec2 EndPosition { get; protected set; }

        public LineCollider(GameObject parent, Vec2 position, Vec2 startPosition, Vec2 endPosition) : base(parent, position, ColliderType.Line)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
        }

        public override CollisionInfo GetCollision(Collider other) => throw new Exception("Line collider GetCollision() function should not be called as it is stationary");

        public override bool IsColliding(Collider other) => throw new Exception("Line collider IsColliding() function should not be called as it is stationary");

        public override void ResolveCollision(CollisionInfo col) => throw new Exception("Line collider ResolveCollision() function should not be called as it is stationary");
    }
}
