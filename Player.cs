using System;
using TiledMapParser;
using GXPEngine.Physics.Shapes;

namespace GXPEngine
{
    internal class Player : AnimatedCircle
    {
        private Arrow shootStrengthArrow;
        private Arrow velocityArrow;

        private bool _isPlayerMoving = false;
        private Vec2 _position; // Do I need to explain?

        public Player() : base(32, "circle.png", 1, 1)
        {
            Collider.SetPosition(new Vec2(game.width / 2, game.height / 2));
            Collider.LoseVelocityOverTime = true;
            velocityArrow = new Arrow(Vec2.zero, Collider.Velocity, 30);
            shootStrengthArrow = new Arrow(Vec2.zero, Vec2.zero, 30);
            AddChild(velocityArrow);
            AddChild(shootStrengthArrow);
        }

        private void Update()
        {
            _isPlayerMoving = !Vec2.IsZero(Collider.Velocity, 0.01f);
            CheckMousePosition();
            UpdateArrows();
            AimTowardsMouse();
        }

        private void CheckMousePosition()
        {
            Vec2 mouseVector = new Vec2(Input.mouseX, Input.mouseY) - Collider.Position;
            if (!_isPlayerMoving)
            {
                shootStrengthArrow.vector = mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 30), 0f, 10f);
                if (shootStrengthArrow.vector.Length() > 0 && shootStrengthArrow.vector.Length() <= 5.0f)
                {
                    shootStrengthArrow.color = 0xFF00FF00;
                }
                else if (shootStrengthArrow.vector.Length() > 5.0f && shootStrengthArrow.vector.Length() <= 9.0f)
                {
                    shootStrengthArrow.color = 0xFFFFFF00;
                }
                else if (shootStrengthArrow.vector.Length() > 9.0f)
                {
                    shootStrengthArrow.color = 0xFFFF0000;
                }
            }
            else shootStrengthArrow.vector = Vec2.zero;
            if (Input.GetMouseButtonDown(0) && !_isPlayerMoving) Collider.SetVelocity(mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 10), 0f, 1000f));
        }

        private void AimTowardsMouse()
        {
            if (!_isPlayerMoving)
            {
                Vec2 mouse = new Vec2(Input.mouseX, Input.mouseY);
                Vec2 playerPosition = new Vec2(Collider.Position.x, Collider.Position.y);

                Vec2 mouseDirection = mouse - playerPosition;
                float angle = mouseDirection.GetAngleDegrees();
                rotation = angle + 90; // adjusting the angle of the sprite because I am too lazy to open paint.net
            }
            
        }

        private void UpdateArrows()
        {
            //velocityArrow.startPoint = Collider.Position;
            shootStrengthArrow.startPoint = Collider.Position;
            //velocityArrow.vector = Collider.Velocity;
        }
    }
}
