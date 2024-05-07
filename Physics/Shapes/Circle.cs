namespace GXPEngine.Physics.Shapes
{
    internal class Circle : Sprite
    {
        public float Radius { get; private set; }
        private readonly Collider _collider;
        public Collider Collider => _collider;

        public Circle(int radius, string filename) : base(filename, addCollider: false)
        {
            Radius = radius;
            SetScaleXY(radius * 2 / 64f, radius * 2 / 64f);
            SetOrigin(radius, radius);
            _collider = PhysicsManager.AddCollider(this, Collider.ColliderType.Circle);
        }

        private void Update()
        {
            SetXY(_collider.Position.x, _collider.Position.y);
        }

        public override void Destroy()
        {
            base.Destroy();
            _collider.Destroy();
        }
    }
}
