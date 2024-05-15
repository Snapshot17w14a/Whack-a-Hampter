using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GXPEngine.SceneManager;

namespace GXPEngine.Physics.PhysicsObjects
{
    internal class Windmill : Sprite
    {
        public float Strength { get; private set; }
        public Vec2 NormalizedDirection { get; private set; }

        Line windmillWall;
        

        public Windmill(float strength, float angle) : base("colors.png")
        {
            visible = true;
            collider.isTrigger = true;
            Strength = strength;
            NormalizedDirection = Vec2.GetUnitVectorDeg(angle);
            windmillWall = new Line(new Vec2(x + width/2 + 32, y + height/2), new Vec2(x + width / 2 - 32, y + height / 2));
            SceneManager.SceneManager.CurrentScene.AddChild(windmillWall);
        }

        private void Update()
        {
            windmillWall.CallCollider(new Vec2(x + width / 2 + 32, y + height / 2), new Vec2(x + width / 2 - 32, y + height / 2));

            if (GameData.ShowColliders) Gizmos.DrawRectangle(x + width / 2, y + height / 2, width, height, color: GameData.ColliderColor);

        }
    }
}
