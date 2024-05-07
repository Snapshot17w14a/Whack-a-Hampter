using System;
using GXPEngine.Physics.Shapes;

namespace GXPEngine
{
    internal class Player : AnimatedCircle
    {
        private Arrow shootStrengthArrow;
        private Arrow velocityArrow;

        private bool _isPlayerMoving = false;

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
        }

        private void CheckMousePosition()
        {
            Vec2 mouseVector = new Vec2(Input.mouseX, Input.mouseY) - Collider.Position;
            if (!_isPlayerMoving) shootStrengthArrow.vector = mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 30), 0f, 10f);
            else shootStrengthArrow.vector = Vec2.zero;
            if (Input.GetMouseButtonDown(0) && !_isPlayerMoving) Collider.SetVelocity(mouseVector.Normalized() * Mathf.Clamp((mouseVector.Length() / 30), 0f, 10f));
        }

        private void UpdateArrows()
        {
            velocityArrow.startPoint = Collider.Position;
            shootStrengthArrow.startPoint = Collider.Position;
            velocityArrow.vector = Collider.Velocity;
        }
    }
}
