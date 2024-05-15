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
        Line windmillWall;
        

        public Windmill() : base("colors.png")
        {
            visible = false;
            collider.isTrigger = true;
            windmillWall = new Line(Vec2.zero, Vec2.zero);
            SceneManager.SceneManager.CurrentScene.AddChild(windmillWall);
        }

        private void Update()
        {
            SetColliderPosition();
            if (GameData.ShowColliders) Gizmos.DrawRectangle(x + width / 2, y + height / 2, width, height, color: GameData.ColliderColor);
        }

        private void SetColliderPosition()
        {
            windmillWall.CallCollider(new Vec2(x, y + height / 2), new Vec2(x + width, y + height / 2));
        }
    }
}
