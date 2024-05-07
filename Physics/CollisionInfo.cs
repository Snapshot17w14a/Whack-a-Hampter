namespace GXPEngine.Physics
{
    internal class CollisionInfo
    {
        public Vec2 normal;
        public Collider other;
        public float timeOfImpact;

        public CollisionInfo(Collider other, Vec2 normal, float timeOfImpact)
        {
            this.other = other;
            this.normal = normal;
            this.timeOfImpact = timeOfImpact;
        }

        public override string ToString() => $"other: {other}, normal: {normal}, toi: {timeOfImpact}";
    }
}
