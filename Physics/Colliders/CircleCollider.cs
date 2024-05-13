namespace GXPEngine.Physics.Colliders
{
    internal class CircleCollider : Collider
    {
        public float Radius { get; private set; }
        private bool _firstCollision = true;

        public CircleCollider(GameObject parent, Vec2 position, float radius) : base(parent, position, ColliderType.Circle)
        {
            Radius = radius;
        }

        public override CollisionInfo GetCollision(Collider other)
        {
            if(GameData.ShowColliders)
            {
                Gizmos.DrawCross(Position.x, Position.y, Radius, color: GameData.ColliderColor, width: 2);
                Gizmos.DrawPlus(Position.x, Position.y, Radius, color: GameData.ColliderColor, width: 2);
            }
            if (other is CircleCollider circleCollider) return GetCollision(circleCollider);
            else if (other is LineCollider lineCollider) return GetCollision(lineCollider);
            else if (other is LineSegmentCollider lineSegmentCollider) return GetLineSegmentCollision(lineSegmentCollider);
            return null;
        }

        private CollisionInfo GetCollision(CircleCollider other)
        {
            Vec2 relativePosition = OldPosition - other.Position;
            float b = 2 * relativePosition.Dot(Velocity);
            float c = relativePosition.SquareLength() - (Radius + other.Radius) * (Radius + other.Radius);
            if (c < 0)
            {
                if (b < 0) return new CollisionInfo(other, relativePosition.Normalized(), 0);
                else return null;
            }
            float a = Velocity.SquareLength();
            float d = b * b - 4 * a * c;
            if (Velocity == Vec2.zero || d < 0) return null;
            float t = (-b - Mathf.Sqrt(d)) / (2 * a);
            if (t >= 0 && t < 1) return new CollisionInfo(other, (OldPosition + Velocity * t - other.Position).Normalized(), t);
            return null;
        }

        private CollisionInfo GetCollision(LineCollider other)
        {
            Vec2 linevector = other.EndPosition - other.StartPosition;
            Vec2 difference = Position - other.StartPosition;
            float b = velocity.Dot(linevector.Normal());
            if (b <= 0) return null;
            float a = difference.Dot(linevector.Normal());
            float t;
            if (a >= 0) t = a / b;
            else if (a >= -Radius) t = 0;
            else return null;
            if(t <= 1) return new CollisionInfo(other, linevector.Normal(), t);
            return null;
        }

        private CollisionInfo GetLineSegmentCollision(LineSegmentCollider other)
        {
            CollisionInfo earliestCollision = null;
            CollisionInfo collisionInfo = GetCollision(other.StartCap);
            if (collisionInfo != null && (earliestCollision == null || collisionInfo.timeOfImpact < earliestCollision.timeOfImpact)) earliestCollision = collisionInfo;
            collisionInfo = GetCollision(other.EndCap);
            if (collisionInfo != null && (earliestCollision == null || collisionInfo.timeOfImpact < earliestCollision.timeOfImpact)) earliestCollision = collisionInfo;
            collisionInfo = GetCollision(other);
            if (collisionInfo != null && (earliestCollision == null || collisionInfo.timeOfImpact < earliestCollision.timeOfImpact)) earliestCollision = collisionInfo;
            return earliestCollision;
        }

        private CollisionInfo GetCollision(LineSegmentCollider other)
        {
            Vec2 linevector = other.EndPosition - other.StartPosition;
            Vec2 difference = Position - other.StartPosition;
            float b = velocity.Dot(linevector.Normal());
            if (b <= 0) return null;
            float a = difference.Dot(linevector.Normal());
            float t;
            if (a >= 0) t = a / b;
            else if (a <= Radius && a >= -Radius) t = 0;
            else return null;
            if (t <= 1)
            {
                Vec2 poi = OldPosition + velocity * t;
                float linedistance = (poi - other.StartPosition).Dot(linevector.Normalized());
                if (linedistance >= 0 && linedistance * linedistance <= linevector.SquareLength()) return new CollisionInfo(other, linevector.Normal(), t);
            }
            return null;
        }

        public override void ResolveCollision(CollisionInfo col)
        {
            OnCollision?.Invoke(col.other);
            col.other.OnCollision?.Invoke(this);
            Position = OldPosition + Velocity * col.timeOfImpact;
            velocity.Reflect(col.normal, Bounciness);
            //if (col.other is CircleCollider circle && Velocity.Dot(circle.Velocity) > 0) // Conservation of momentum has to be fixed here!!! TODO
            //{
            //    Vec2 u = (Velocity * Mass + circle.Velocity * circle.Mass) / (Mass + circle.Mass);
            //    velocity -= (1 + Bounciness) * (Velocity - u).Dot(col.normal) * col.normal;
            //    // changing own vel twice, not changing other vel...
            //    // Also: check whether it's a real collision (relative vel)
            //}
            if (_firstCollision && IsApporximately(col.timeOfImpact, 0))
            {
                _firstCollision = false;
                PhysicsManager.CheckForCollision(this, false);
            }
            else _firstCollision = true;
        }

        public override bool IsColliding(Collider other)
        {
            if(other is CircleCollider circle) return (Position - circle.Position).SquareLength() < (Radius + circle.Radius) * (Radius + circle.Radius);
            if(other is LineCollider line) return (Position - line.StartPosition).Dot((line.EndPosition - line.StartPosition).Normal()) < Radius;
            return false;
        }

        public static bool IsApporximately(float a, float b, float tolerance = 0.00001f)
        {
            return Mathf.Abs(a - b) < tolerance;
        }
    }
}
