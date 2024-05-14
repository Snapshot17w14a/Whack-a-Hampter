using System;

namespace GXPEngine.Physics.Colliders
{
    internal class LineSegmentCollider : Collider
    {
        public CircleCollider StartCap => _startCap;
        public CircleCollider EndCap => _endCap;

        public Vec2 EndPosition { get; protected set; }
        public Vec2 StartPosition { get; protected set; }


        private CircleCollider _startCap;
        private CircleCollider _endCap;

        public LineSegmentCollider(GameObject parent, Vec2 position, Vec2 startPosition, Vec2 endPosition) : base(parent, position, ColliderType.LineSegment)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            _startCap = new CircleCollider(parent, startPosition, 0);
            _endCap = new CircleCollider(parent, endPosition, 0);
        }

        /// <summary>Updates the position of the line segment and end caps, only used if necessary to update the position of the line segment.</summary>
        public void UpdatePosition(Vec2 startPosition, Vec2 endPosition)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            _startCap.SetPosition(startPosition);
            _endCap.SetPosition(endPosition);
        }

        public override CollisionInfo GetCollision(Collider other) => throw new Exception("Linesegment collider GetCollision() function should not be called as it is stationary");

        public override bool IsColliding(Collider other) => throw new Exception("Linesegment collider IsColliding() function should not be called as it is stationary");

        public override void ResolveCollision(CollisionInfo col) => throw new Exception("Linesegment collider ResolveCollision() function should not be called as it is stationary");
    }
}
