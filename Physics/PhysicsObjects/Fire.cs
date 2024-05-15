using GXPEngine.Managers;
using System;

namespace GXPEngine
{
    internal class Fire : AnimationSprite
    {
        public Fire() : base("fire_animation.png", 5, 1)
        {

        }

        private void Update()
        {
            Animate(GameData.FireSpeed);
        }
    }
}