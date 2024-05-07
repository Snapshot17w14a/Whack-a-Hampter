using GXPEngine.Physics.Colliders;
using System.Collections.Generic;
using System;

namespace GXPEngine.Physics
{
    internal static class PhysicsManager
    {
        private static readonly List<Collider> _colliders = new List<Collider>();

        /// <summary>Create a new collider of the given type and add it to the list of colliders</summary>
        /// <param name="parent">The <see cref="GameObject"/> that the collider will be attached to</param>
        /// <param name="collidertype">The <see cref="Collider.ColliderType"/> that should be created</param>
        /// <param name="addToList">Wheter the collider should be added to the list of simulated objects (useful when making one compound collider consisting of multiple different colliders eg.: <see cref="LineSegmentCollider"/>)</param>
        /// <returns>Returns the created collider to be stored by the parent <see cref="GameObject"/></returns>
        public static Collider AddCollider(GameObject parent, Collider.ColliderType collidertype, bool addToList = true)
        {
            Collider collider = null;
            switch (collidertype)
            {
                case Collider.ColliderType.Box:
                    //collider = new BoxCollider();
                    goto default;
                case Collider.ColliderType.Circle:
                    collider = new CircleCollider(parent, new Vec2(parent.x, parent.y), ((Circle)parent).Radius);
                    break;
                case Collider.ColliderType.Line:
                    collider = new LineCollider(parent, Vec2.zero, ((Line)parent).StartPosition, ((Line)parent).EndPosition);
                    break;
                case Collider.ColliderType.LineSegment:
                    collider = new LineSegmentCollider(parent, Vec2.zero, ((Line)parent).StartPosition, ((Line)parent).EndPosition);
                    break;
                default:
                    throw new Exception("Collider type incorrect or does not exist");
            }
            if (addToList) _colliders.Add(collider);
            return collider;
        }

        public static void Step()
        {
            foreach (Collider collider in _colliders) CheckForCollision(collider);
        }

        /// <summary>Check for collision with all other colliders from the perspective of the provided collider</summary>
        /// <param name="collider">The collider to check all collisions against</param>
        /// <param name="addAcceleration">Set to true to add the collider's acceleration to its velocity when moving it</param>
        public static void CheckForCollision(Collider collider, bool addAcceleration = true)
        {
            if (collider is LineCollider || collider is LineSegmentCollider) return;
            collider.Move(addAcceleration);
            CollisionInfo earliestCollision = null;
            foreach (Collider other in _colliders)
            {
                if (other != collider)
                {
                    if(IsTagIgnored(collider, other)) continue;
                    CollisionInfo collisionInfo = collider.GetCollision(other);
                    if (earliestCollision == null || (collisionInfo != null && earliestCollision.timeOfImpact > collisionInfo.timeOfImpact)) earliestCollision = collisionInfo;
                }
            }
            if (earliestCollision != null) collider.ResolveCollision(earliestCollision);
        }

        /// <summary>Check whether the other collider has a tag that is ignored by the collider</summary>
        private static bool IsTagIgnored(Collider collider, Collider other)
        {
            if (collider.Tag == null || other.Tag == null || collider.IgnoredTags.Count == 0) return false;
            bool result = false;
            foreach (string tag in collider.IgnoredTags) if (other.Tag == tag) result = true;
            return result;
        }

        /// <summary>Returns a list of colliders the given collider is overlapping with</summary>
        public static List<Collider> GetOverlaps(Collider collider)
        {
            List<Collider> result = new List<Collider>();
            foreach (Collider other in _colliders)
            {
                if (other != collider && collider.IsColliding(other)) result.Add(other);
            }
            return result;
        }

        /// <summary>Removes the given collider from the list of simulated and collision checked colliders</summary>
        public static void RemoveCollider(Collider collider) => _colliders.Remove(collider);
    }
}