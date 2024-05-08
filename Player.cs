using System;
using GXPEngine.Physics.Shapes;

namespace GXPEngine
{
    internal class Player : AnimatedCircle
    {
        private Arrow shootStrengthArrow;
        private Arrow velocityArrow;

        private bool _isPlayerMoving = false;

        public Player() : base(GameData.PlayerRadius, "circle.png", 1, 1)
        {
            Collider.SetPosition(GameData.PlayerSpawnPosition);
            Collider.LoseVelocityOverTime = true;
            velocityArrow = new Arrow(Vec2.zero, Collider.Velocity, GameData.GlobalArrowSize);
            shootStrengthArrow = new Arrow(Vec2.zero, Vec2.zero, GameData.GlobalArrowSize);
            AddChild(velocityArrow);
            AddChild(shootStrengthArrow);
        }

        private void Update()
        {
            if (Input.GetKeyDown(Key.R)) ResetPlayer();
            _isPlayerMoving = !Vec2.IsZero(Collider.Velocity, 0.01f);
            CheckMousePosition();
            UpdateArrows();
            PringDebugData();
        }

        private void ResetPlayer()
        {
            Collider.SetPosition(GameData.PlayerSpawnPosition);
            Collider.SetVelocity(Vec2.zero);
        }

        private void CheckMousePosition()
        {
            Vec2 mouseVector = new Vec2(Input.mouseX, Input.mouseY) - Collider.Position;
            if (!_isPlayerMoving) shootStrengthArrow.vector = mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 30), 0f, GameData.PlayerMaxVelocity);
            else shootStrengthArrow.vector = Vec2.zero;
            if (Input.GetMouseButtonDown(0) && !_isPlayerMoving) Collider.SetVelocity(mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 30), 0f, GameData.PlayerMaxVelocity));
        }

        private void UpdateArrows()
        {
            velocityArrow.startPoint = Collider.Position;
            shootStrengthArrow.startPoint = Collider.Position;
            velocityArrow.vector = Collider.Velocity;
        }

        private void PringDebugData()
        {
            Console.Clear();
            Console.WriteLine($"Player position: {Collider.Position}");
            Console.WriteLine($"Player velocity: {Collider.Velocity}");
        }
    }
}
