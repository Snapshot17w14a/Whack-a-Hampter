using System.Collections.Generic;
using GXPEngine.Physics;
using System;

namespace GXPEngine
{
    internal static class ColliderLoader
    {
        private static readonly List<ColliderData> _colliderData = new List<ColliderData>();

        public static void AddColliderData(ColliderData data) => _colliderData.Add(data);

        ///<summary>Instantiates the <see cref="Physics.Colliders.LineSegmentCollider">s based on the stored <see cref="ColliderData"/> data.</summary>
        ///<paramref name="consolidateColliders"/>If true, multiple colliders will be consolidated into one collider.</paramref>
        public static void InstantiateColliders(bool consolidateColliders = true)
        {
            if (consolidateColliders)
            {
                bool modifications = true;
                do { modifications = ConsolidateColliders(); Console.WriteLine(modifications); } while (modifications);
            }
            _colliderData.ForEach(_colliderData => Console.WriteLine(_colliderData));
            foreach (ColliderData data in _colliderData)
            {
                Line line = new Line(data.Start, data.End);
                Game.main.AddChild(line);
            }
        }

        private static bool ConsolidateColliders()
        {
            //Horizontal consolidation
            foreach (ColliderData collData in _colliderData)
            {
                foreach (ColliderData otherData in _colliderData)
                {
                    if (collData == otherData) continue;
                    if (collData.End == otherData.Start && collData.End.y == otherData.End.y && collData.Start.y == otherData.End.y)
                    {
                        ColliderData newCollider = new ColliderData(collData.Start, otherData.End);
                        _colliderData.Remove(collData);
                        _colliderData.Remove(otherData);
                        _colliderData.Add(newCollider);
                        return true;
                    }
                }
            }
            //Vertical consolidation
            foreach (ColliderData collData in _colliderData)
            {
                foreach (ColliderData otherData in _colliderData)
                {
                    if (collData == otherData) continue;
                    if (collData.End == otherData.Start && collData.End.x == otherData.End.x && collData.Start.x == otherData.End.x)
                    {
                        ColliderData newCollider = new ColliderData(collData.Start, otherData.End);
                        _colliderData.Remove(collData);
                        _colliderData.Remove(otherData);
                        _colliderData.Add(newCollider);
                        return true;
                    }
                }
            }
            return false;
        }

        public static void AddColliders(string colliderProperty, Sprite sprite)
        {
            var colliders = GetOffset(colliderProperty, sprite);
            foreach (ColliderData data in colliders) AddColliderData(data);
        }

        /// <summary>Returns the offset of the collider based on the collider property.</summary>
        private static List<ColliderData> GetOffset(string colliderProperty, Sprite sprite)
        {
            string[] strings = colliderProperty.Split(';');
            List<ColliderData> colliderData = new List<ColliderData>();
            foreach (string colliderProp in strings)
            {
                switch(colliderProp)
                {
                    case "top":
                        colliderData.Add(new ColliderData(new Vec2(sprite.x, sprite.y), new Vec2(sprite.width + sprite.x, sprite.y)));
                        break;
                    case "right":
                        colliderData.Add(new ColliderData(new Vec2(sprite.width + sprite.x, sprite.y), new Vec2(sprite.width + sprite.x, sprite.height + sprite.y)));
                        break;
                    case "bottom":
                        colliderData.Add(new ColliderData(new Vec2(sprite.width + sprite.x, sprite.height + sprite.y), new Vec2(sprite.x, sprite.height + sprite.y)));
                        break;
                    case "left":
                        colliderData.Add(new ColliderData(new Vec2(sprite.x, sprite.height + sprite.y), new Vec2(sprite.x, sprite.y)));
                        break;
                    case "topleftbottomright":
                        colliderData.Add(new ColliderData(new Vec2(sprite.x, sprite.y), new Vec2(sprite.width + sprite.x, sprite.height + sprite.y)));
                        break;
                    case "toprightbottomleft":
                        colliderData.Add(new ColliderData(new Vec2(sprite.width + sprite.x, sprite.y), new Vec2(sprite.x, sprite.height + sprite.y)));
                        break;
                    case "bottomlefttopright":
                        colliderData.Add(new ColliderData(new Vec2(sprite.x, sprite.height + sprite.y), new Vec2(sprite.width + sprite.x, sprite.y)));
                        break;
                    case "bottomrighttopleft":
                        colliderData.Add(new ColliderData(new Vec2(sprite.width + sprite.x, sprite.height + sprite.y), new Vec2(sprite.x, sprite.y)));
                        break;
                }
            }
            return colliderData;
        }
    }

    /// <summary>Collider data struct to store the start and end position of a collider.</summary>
#pragma warning disable CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
#pragma warning disable CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
    struct ColliderData
#pragma warning restore CS0661 // Type defines operator == or operator != but does not override Object.GetHashCode()
#pragma warning restore CS0660 // Type defines operator == or operator != but does not override Object.Equals(object o)
    {
        public ColliderData(Vec2 start, Vec2 end)
        {
            Start = start;
            End = end;
        }

        public Vec2 Start;
        public Vec2 End;

        public static bool operator ==(ColliderData data1, ColliderData data2)
        {
            return data1.Start == data2.Start && data1.End == data2.End;
        }

        public static bool operator !=(ColliderData data1, ColliderData data2)
        {
            return data1.Start != data2.Start && data1.End != data2.End;
        }

        public override string ToString()
        {
            return $"Start: {Start}, End: {End}";
        }
    }
}