using GXPEngine.Physics.Colliders;

namespace GXPEngine.Physics
{
    internal class Line : GameObject
    {
        public Vec2 StartPosition { get; protected set; }
        public Vec2 EndPosition { get; protected set; }

        private readonly Collider _collider;
        private readonly LineSegment _line;

        public Collider Collider => _collider;
        public LineSegment LineSegment => _line;

        public Line(Vec2 startPosition, Vec2 endPosition, bool isSegment = true)
        {
            StartPosition = startPosition;
            EndPosition = endPosition;
            if (GameData.ShowColliders)
            {
                _line = new LineSegment(startPosition, endPosition, GameData.ColliderColor, 2);
                Gizmos.DrawCross(startPosition.x, startPosition.y, 5, color: GameData.ColliderColor);
                Gizmos.DrawCross(endPosition.x, endPosition.y, 5, color: GameData.ColliderColor);
                AddChild(_line);
            }
            _collider = isSegment ? PhysicsManager.AddCollider(this, Collider.ColliderType.LineSegment) : PhysicsManager.AddCollider(this, Collider.ColliderType.Line);
        }

        public void CallCollider(Vec2 start, Vec2 end)
        {
            StartPosition = start;
            EndPosition = end;
            if (GameData.ShowColliders)
            {
                _line.start = start;
                _line.end = end;
            }
            ((LineSegmentCollider)_collider).UpdatePosition(start, end);
        }


        void Update()
        {
            if (GameData.ShowColliders)
            {
                Gizmos.DrawCross(StartPosition.x, StartPosition.y, 5, color: GameData.ColliderColor);
                Gizmos.DrawCross(EndPosition.x, EndPosition.y, 5, color: GameData.ColliderColor);
            }
        }
    }
}
