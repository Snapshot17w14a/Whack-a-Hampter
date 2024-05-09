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
                float maxStrength = 10f;
                float normalizedLength = Mathf.Clamp(mouseVector.Length() / 30, 0f, maxStrength);
                shootStrengthArrow.vector = mouseVector.Normalized() * normalizedLength;

                // Define color stops
                uint startColor = 0xFF00FF00; // Green
                uint midColor = 0xFFFFFF00; // Yellow
                uint endColor = 0xFFFF0000; // Red

                // Interpolate colors
                if (normalizedLength <= 5.0f)
                {
                    shootStrengthArrow.color = LerpColor(startColor, midColor, normalizedLength / 5.0f);
                }
                else if (normalizedLength <= 10.0f)
                {
                    shootStrengthArrow.color = LerpColor(midColor, endColor, (normalizedLength - 5.0f) / 5.0f);
                }
            }
            else
            {
                shootStrengthArrow.vector = Vec2.zero;
            }

            if (Input.GetMouseButtonDown(0) && !_isPlayerMoving)
            {
                Collider.SetVelocity(mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 10), 0f, 1000f));
            }
        }

        private uint LerpColor(uint startColor, uint endColor, float t)
        {
            byte startR = (byte)((startColor >> 16) & 0xFF);
            byte startG = (byte)((startColor >> 8) & 0xFF);
            byte startB = (byte)(startColor & 0xFF);

            byte endR = (byte)((endColor >> 16) & 0xFF);
            byte endG = (byte)((endColor >> 8) & 0xFF);
            byte endB = (byte)(endColor & 0xFF);

            byte newR = (byte)(startR + (endR - startR) * t);
            byte newG = (byte)(startG + (endG - startG) * t);
            byte newB = (byte)(startB + (endB - startB) * t);

            return (uint)((0xFF << 24) | (newR << 16) | (newG << 8) | newB);
        }


        private void AimTowardsMouse()
        {
            if (_isPlayerMoving)
            {
                float angle = Collider.Velocity.GetAngleDegrees();
                rotation = angle + 90; // Adjusting for sprite orientation
            }
            else
            {
                Vec2 mouse = new Vec2(Input.mouseX, Input.mouseY);
                Vec2 playerPosition = new Vec2(Collider.Position.x, Collider.Position.y);
                Vec2 mouseDirection = mouse - playerPosition;
                float angle = mouseDirection.GetAngleDegrees();
                rotation = angle + 90; // Adjusting the angle of the sprite
            }
        }

        private void UpdateArrows()
        {
            //velocityArrow.startPoint = Collider.Position;
            shootStrengthArrow.startPoint = Collider.Position;
            //velocityArrow.vector = Collider.Velocity
        }
    }
}
