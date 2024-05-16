using System;

namespace GXPEngine
{
    internal class WindCurrent : Sprite
    {
        public float Strength { get; private set; }
        public Vec2 NormalizedDirection { get; private set; }

        public WindCurrent(float strength, float angle) : base("colors.png")
        {
            visible = false;
            collider.isTrigger = true;
            Strength = strength;
            NormalizedDirection = Vec2.GetUnitVectorDeg(angle);
        }

        private void Update()
        {
            if (GameData.ShowColliders) Gizmos.DrawRectangle(x + width / 2, y + height / 2, width, height, color: GameData.ColliderColor);
        }
    }
}