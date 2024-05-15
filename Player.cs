using System;
using System.Collections.Generic;
using GXPEngine.Physics.Shapes;

namespace GXPEngine
{
    internal class Player : AnimatedCircle
    {
        private readonly Arrow _shootStrengthArrow;
        private bool _isPlayerMoving = false;

        public Player(float x = 0, float y = 0) : base(32, "hampter_shiit.png", 11, 1)
        {
            SetOrigin(width / 2, height / 2);
            scale = 2f;
            Collider.SetPosition(new Vec2(x, y));
            Collider.LoseVelocityOverTime = true;
            AddChild(_shootStrengthArrow = new Arrow(Collider.Position, Vec2.zero, 1, pLineWidth: 3));
        }

        private void Update()
        {
            if (Collider.IsActive)
            {
                Console.WriteLine(Collider.SlowdownFactor);
                _isPlayerMoving = !Vec2.IsZero(Collider.Velocity, GameData.PlayerIsZeroThreshold);
                SetLocalSlowdown(Collider.Position);
                CheckMousePosition();
                UpdateArrows();
                AimTowardsMouse();
            }
        }

        private void CheckMousePosition()
        {
            Vec2 mouseVector = new Vec2(Input.mouseX, Input.mouseY) - Collider.Position;
            if (!_isPlayerMoving)
            {
                var strength = Mathf.Clamp(mouseVector.Length(), 0, GameData.PlayerMouseMaxStrengthThreshold) / GameData.PlayerMouseMaxStrengthThreshold;
                _shootStrengthArrow.vector = mouseVector.Normalized() * strength * GameData.PlayerMouseMaxStrengthThreshold;
                _shootStrengthArrow.color = strength <= 0.5f ?
                    Mathf.LerpColor(GameData.ArrowStartColor, GameData.ArrowMedianColor, strength * 2) : strength <= 1f ?
                    Mathf.LerpColor(GameData.ArrowMedianColor, GameData.ArrowEndColor, (strength - 0.5f) * 2) :
                    0xffffffff;
                if(Input.GetMouseButtonDown(0)) Collider.SetVelocity(mouseVector.Normalized() * (GameData.PlayerMaxHitStrength * strength));
            }
            else _shootStrengthArrow.vector = Vec2.zero;
        }

        private void AimTowardsMouse()
        {
            if (_isPlayerMoving)
            {
                Animate(Collider.Velocity.Length() * 2 / GameData.PlayerMaxHitStrength);
                rotation = Collider.Velocity.GetAngleDegrees() + 90f; // Adjusting for sprite orientation
            }
            else
            {
                SetFrame(0);
                rotation = (new Vec2(Input.mouseX, Input.mouseY) - new Vec2(Collider.Position.x, Collider.Position.y)).GetAngleDegrees() + 90f; // Adjusting the angle of the sprite
            }
        }

        private void UpdateArrows()
        {
            _shootStrengthArrow.startPoint = Collider.Position;
        }

        private void SetLocalSlowdown(Vec2 position)
        {
            var currentTile = GameData.TileValues[(int)(position.x / 64f), (int)(position.y / 64f)];
            if (GameData.TileSlowdownValues.ContainsKey(currentTile)) Collider.SetSlowdownFactor(GameData.TileSlowdownValues[currentTile]);
            else Collider.SetSlowdownFactor(0.98f);
        }

        public void DeathXD() 
        {
            Environment.Exit(0);
        }
    }
}
