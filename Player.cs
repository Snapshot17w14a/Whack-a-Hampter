using GXPEngine.Physics.Shapes;

namespace GXPEngine
{
    internal class Player : AnimatedCircle
    {
        private readonly Arrow _shootStrengthArrow;

        private bool _isPlayerMoving = false;

        public Player() : base(32, "hamter.png", 1, 1)
        {
            SetOrigin(width / 2, height / 2);
            scale = 2f;
            Collider.SetPosition(new Vec2(game.width / 2, game.height / 2));
            Collider.LoseVelocityOverTime = true;
            _shootStrengthArrow = new Arrow(Vec2.zero, Vec2.zero, 30, pLineWidth: 3);
            AddChild(_shootStrengthArrow);
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
                float normalizedLength = Mathf.Clamp(mouseVector.Length() / 30f, 0f, GameData.PlayerMaxHitStrength / (GameData.PlayerMaxHitStrength / 10f));
                _shootStrengthArrow.vector = mouseVector.Normalized() * normalizedLength;
                // Interpolate colors
                _shootStrengthArrow.color = normalizedLength <= 5f ?
                    LerpColor(GameData.ArrowStartColor, GameData.ArrowMedianColor, normalizedLength / 5f) : normalizedLength <= 10f ?
                    LerpColor(GameData.ArrowMedianColor, GameData.ArrowEndColor, (normalizedLength - 5f) / 5f) :
                    0xffffffff;
            }
            else _shootStrengthArrow.vector = Vec2.zero;

            if (Input.GetMouseButtonDown(0) && !_isPlayerMoving)
            {
                Collider.SetVelocity(mouseVector.Normalized() * GameData.PlayerMaxHitStrength * (Mathf.Clamp(mouseVector.Length() / GameData.PlayerMouseMaxStrengthThreshold, 0, 1)));
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
            rotation = _isPlayerMoving ?
                Collider.Velocity.GetAngleDegrees() + 90f : 
                (new Vec2(Input.mouseX, Input.mouseY) - new Vec2(Collider.Position.x, Collider.Position.y)).GetAngleDegrees() + 90f;
        }

        private void UpdateArrows()
        {
            _shootStrengthArrow.startPoint = Collider.Position;
        }
    }
}
