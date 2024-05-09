namespace GXPEngine.Physics
{
    internal class Line : GameObject
    {
        public Vec2 StartPosition { get; protected set; }
        public Vec2 EndPosition { get; protected set; }

        private readonly Collider _collider;
        public Collider Collider => _collider;
        private readonly LineSegment _line;
        public LineSegment LineSegment => _line;

        public Line(Vec2 startPosition, Vec2 endPosition, bool isSegment = true)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            _line = new LineSegment(startPosition, endPosition);
            AddChild(_line);
            _collider = isSegment ? PhysicsManager.AddCollider(this, Collider.ColliderType.LineSegment) : PhysicsManager.AddCollider(this, Collider.ColliderType.Line);
        }
    }
}
