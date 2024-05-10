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
                do { modifications = ConsolidateColliders(); } while (modifications);
            }
            _colliderData.ForEach(_colliderData => Console.WriteLine(_colliderData));
            AddColliders();
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

        private static void AddColliders()
        { 
            foreach (ColliderData data in _colliderData)
            {
                Line line = new Line(data.Start, data.End);
                Game.main.AddChild(line);
            }
        }

        /// <summary>Returns the offset of the collider based on the collider property.</summary>
        public static ColliderData GetOffset(string colliderProperty, int width, int height)
        {
            ColliderData colliderData = new ColliderData(Vec2.zero, Vec2.zero);
            switch (colliderProperty)
            {
                case "top":
                    colliderData.Start = new Vec2(-width / 2, -height / 2);
                    colliderData.End = new Vec2(width / 2, -height / 2);
                    break;
                case "right":
                    colliderData.Start = new Vec2(width / 2, -height / 2);
                    colliderData.End = new Vec2(width / 2, height / 2);
                    break;
                case "bottom":
                    colliderData.Start = new Vec2(width / 2, height / 2);
                    colliderData.End = new Vec2(-width / 2, height / 2);
                    break;
                case "left":
                    colliderData.Start = new Vec2(-width / 2, height / 2);
                    colliderData.End = new Vec2(-width / 2, -height / 2);
                    break;
            }
            return colliderData;
        }

    }

    /// <summary>Collider data struct to store the start and end position of a collider.</summary>
    struct ColliderData
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