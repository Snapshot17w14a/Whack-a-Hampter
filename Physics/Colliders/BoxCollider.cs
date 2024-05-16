//using System;

//namespace GXPEngine.Physics.Colliders
//{
//    internal class BoxCollider : Collider
//    {
//        public Vec2 Size { get; private set; }

//        public BoxCollider(GameObject parent, Vec2 position, Vec2 size) : base(parent, position, ColliderType.Box)
//        {
//            Size = size;
//        }
        
//        public override CollisionInfo GetCollision(Collider other) { throw new Exception("Box colliders cannot get collision as they are stationary and should only be used for ") }
//        public override void ResolveCollision(CollisionInfo col) { }
//        public override bool IsColliding(Collider other) { return false; }
//    }