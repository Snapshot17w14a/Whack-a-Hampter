using System;
using GXPEngine.Physics.PhysicsObjects;
using GXPEngine.SceneManagement;
using GXPEngine.Physics.Shapes;
using GXPEngine.Scenes;

namespace GXPEngine
{
    internal class Player : AnimatedCircle
    {
        private readonly Arrow _shootStrengthArrow;
        private bool _isPlayerMoving = false;
        private bool _wasSFXPlayed = false;
        private Vec2 _hitPosition;

        public Player(float x = 0, float y = 0) : base(16, "hampter_shiit.png", 11, 1)
        {
            SetOrigin(width / 2, height / 2);
            Collider.SetPosition(new Vec2(x, y));
            Collider.LoseVelocityOverTime = true;
            AddChild(_shootStrengthArrow = new Arrow(Collider.Position, Vec2.zero, 1, pLineWidth: 3));
        }

        private void Update()
        {
            if (Collider.IsActive)
            {
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
                if (Input.GetMouseButtonDown(0))
                {
                    Collider.SetVelocity(mouseVector.Normalized() * (GameData.PlayerMaxHitStrength * strength));
                    _hitPosition = Collider.Position;
                    GameData.SoundHandler.PlaySound("HitHampter");
                    ((TiledScene)SceneManager.CurrentScene).hitCount++;
                }
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
            var currentTile = GameData.TileValues[Mathf.Clamp((int)(position.x / 32f), 0, GameData.TileValues.GetLength(0) - 1), Mathf.Clamp((int)(position.y / 32f), 0, GameData.TileValues.GetLength(1) - 1)];
            Collider.SetSlowdownFactor(GameData.TileSlowdownValues.ContainsKey(currentTile) ? GameData.TileSlowdownValues[currentTile] : 0.98f);
            if(!_wasSFXPlayed && Collider.SlowdownFactor == 0.8f) { GameData.SoundHandler.PlaySound("MudSFX"); _wasSFXPlayed = true; }
            else if(!_wasSFXPlayed && Collider.SlowdownFactor == 0.5f) { GameData.SoundHandler.PlaySound("WaterSFX"); _wasSFXPlayed = true; }
            else if (_wasSFXPlayed && Collider.SlowdownFactor == 0.98f) _wasSFXPlayed = false;
        }

        public void ResetPosition() 
        {
            Collider.SetPosition(_hitPosition);
            Collider.SetVelocity(Vec2.zero);
        }
    }
}
