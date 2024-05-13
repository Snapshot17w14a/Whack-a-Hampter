using System;
using System.Collections.Generic;

namespace GXPEngine.Physics
{
    internal abstract class Collider
    {
        public enum ColliderType { Box, Circle, Line, LineSegment }

        public GameObject Parent { get; protected set; }
        public ColliderType Type { get; protected set; }
        public Vec2 Acceleration { get; protected set; } = Vec2.zero;
        public Vec2 OldPosition { get; protected set; } = Vec2.zero;
        public Vec2 Position { get; protected set; } = Vec2.zero;
        public Vec2 Velocity { get => velocity; protected set => velocity = value; }

        protected Vec2 velocity = Vec2.zero;
        
        public bool LoseVelocityOverTime = false;

        public string Tag { get; protected set; } = null;
        public List<string> IgnoredTags { get; protected set; } = new List<string>();

        public float Bounciness { get; protected set; } = 0.98f;
        public float Mass { get; protected set; } = 1f;

        public Action<Collider> OnCollision;

        protected Collider(GameObject parent, Vec2 position, ColliderType type)
        {
            Position = position;
            Parent = parent;
            Type = type;
        }

        /// <summary>This method is called by the <see cref="PhysicsManager"/>, it moves the object based on its acceleration (if <paramref name="addAcceleration"/> is set to true), velocity and position</summary>
        public void Move(bool addAcceleration = true)
        {
            OldPosition = Position;
            if (addAcceleration) velocity += Acceleration;
            Position += velocity;
            Parent.SetXY(Position.x, Position.y);
            if (LoseVelocityOverTime) velocity *= 0.98f;
        }

        public void Destroy()
        {
            Parent = null;
            OnCollision = null;
            PhysicsManager.RemoveCollider(this);
        }

        /// <summary>Set the acceleration of the collider. This will be added to the velocity every frame.</summary>
        public void SetAcceleration(Vec2 acceleration) => Acceleration = acceleration;

        /// <summary>Set the velocity of the collider.</summary>
        public void SetVelocity(Vec2 velocity) => Velocity = velocity;

        /// <summary>Set the position of the collider. This also effects the sprite it is attached to.</summary>
        public void SetPosition(Vec2 position) => Position = position;

        /// <summary>Add the provided velocity to the current velocity.</summary>
        public void AddVelocity(Vec2 velocity) => Velocity += velocity;

        /// <summary>Set the bounciness coefficient of the collider.</summary>
        public void SetBounciness(float bounciness) => Bounciness = bounciness;

        /// <summary>Set the mass of the collider, this effects the amount of momentum retained after collision.</summary>
        public void SetMass(float mass) => Mass = mass;

        /// <summary>Add a tag to the list of ignored tags.</summary>
        public void AddIgnoredTag(string tag) => IgnoredTags.Add(tag);

        /// <summary>Set the tag of the collider. This will add the tag to the ignored tags list.</summary>
        public void SetTag(string tag) => Tag = tag;

        public abstract CollisionInfo GetCollision(Collider other);
        public abstract void ResolveCollision(CollisionInfo col);
        public abstract bool IsColliding(Collider other);

        public override string ToString()
        {
            string toPrint = $"Collider: {Type} at {Position}";
            if (Type == ColliderType.LineSegment || Type == ColliderType.Line) toPrint += $" from {((Line)Parent).StartPosition} to {((Line)Parent).EndPosition}";
            if (Tag != null) toPrint += $" with tag {Tag}";
            if (IgnoredTags.Count > 0) toPrint += $" ignoring tags: {string.Join(", ", IgnoredTags)}";
            return toPrint;
        } 
    }
}
