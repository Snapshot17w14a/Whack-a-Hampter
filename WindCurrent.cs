using System;

namespace GXPEngine
{
    internal class WindCurrent : Sprite
    {
        private float _reach;
        private float _strength;
        private Vec2 _normalizedDirection;

        public WindCurrent(float reach, float strength, float angle) : base("colors.png", false, false)
        {
            _reach = reach;
            _strength = strength;
            _normalizedDirection = Vec2.GetUnitVectorDeg(angle);
            Console.WriteLine("called");
        }
    }
}