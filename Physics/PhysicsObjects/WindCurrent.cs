using System;

namespace GXPEngine
{
    internal class WindCurrent : Sprite
    {
        private float _reach;
        private float _strength;
        private Vec2 _normalizedDirection;

        public WindCurrent(float strength, float angle) : base("colors.png")
        {
            collider.isTrigger = true;
            visible = false;
            _strength = strength;
            _normalizedDirection = Vec2.GetUnitVectorDeg(angle);
        }

        private void Update()
        {
            if (GameData.ShowColliders) Gizmos.DrawRectangle(x + width / 2, y + height / 2, width, height, color: GameData.ColliderColor);
            if (HitTest(GameData.ActivePlayer)) GameData.ActivePlayer.Collider.SetAcceleration(_normalizedDirection * _strength);
            else GameData.ActivePlayer.Collider.SetAcceleration(Vec2.zero);
        }
    }
}