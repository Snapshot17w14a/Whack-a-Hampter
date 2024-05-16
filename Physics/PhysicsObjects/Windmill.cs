using System;
using GXPEngine.SceneManagement;

namespace GXPEngine.Physics.PhysicsObjects
{
    internal class Windmill : Sprite
    {
        Line[] windmillBlades;
        float rotationSpeed = -GameData.windmillSpinSpeed; // Negative for clockwise rotation
        float currentAngle = 0f;

        public Windmill() : base("windmill_2.png")
        {
            SetOrigin(width / 2, height / 2); // Set the origin to the center of the sprite
            x += width / 2; // Offset position to the right
            y += height / 2; // Offset position down
            visible = true;
            collider.isTrigger = true;
            InitializeBlades();
        }

        private void InitializeBlades()
        {
            windmillBlades = new Line[4]; // Adjust number of blades here
            for (int i = 0; i < windmillBlades.Length; i++)
            {
                windmillBlades[i] = new Line(Vec2.zero, Vec2.zero);
                SceneManager.CurrentScene.AddChild(windmillBlades[i]);
            }
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            RotateWindmill(deltaTime);
            CheckCollisionWithPlayer();
            if (GameData.ShowColliders) Gizmos.DrawRectangle(x, y, width, height, color: GameData.ColliderColor);
        }

        private void RotateWindmill(float deltaTime)
        {
            currentAngle += rotationSpeed * deltaTime / 1000;
            currentAngle %= 360;

            // Update the rotation of the Windmill sprite itself
            rotation += rotationSpeed * deltaTime / 1000;
            rotation %= 360;

            Vec2 centerPoint = new Vec2(x, y);
            float radius = width / 2;

            for (int i = 0; i < windmillBlades.Length; i++)
            {
                float bladeAngle = currentAngle + i * 90; // 90 degrees apart for each blade
                bladeAngle %= 360; // Ensure angle is within 0-360 range
                Vec2 endPoint = centerPoint + Vec2.GetUnitVectorDeg(bladeAngle) * radius;
                windmillBlades[i].CallCollider(centerPoint, endPoint);
            }
        }

        private void CheckCollisionWithPlayer()
        {
            Player player = SceneManager.CurrentScene.FindObjectOfType<Player>();
            bool isCollision = false;
            foreach (var blade in windmillBlades)
            {
                if (player != null && IsCollidingWithPlayer(blade, player))
                {
                    isCollision = true;
                    ApplyForceToPlayer(player);
                }
            }
        }

        private bool IsCollidingWithPlayer(Line blade, Player player)
        {
            bool collision = LineIntersectsCircle(blade.StartPosition, blade.EndPosition, player.Collider.Position, 32);
            return collision;
        }

        private void ApplyForceToPlayer(Player player)
        {
            Vec2 forceDirection = Vec2.GetUnitVectorDeg(currentAngle + 90);
            float forceMagnitude = GameData.windmillForceMagnitude;
            player.Collider.AddVelocity(forceDirection * forceMagnitude);
        }

        private bool LineIntersectsCircle(Vec2 lineStart, Vec2 lineEnd, Vec2 circleCenter, float circleRadius)
        {
            Vec2 d = lineEnd - lineStart;
            Vec2 f = lineStart - circleCenter;

            float a = d.Dot(d);
            float b = 2 * f.Dot(d);
            float c = f.Dot(f) - circleRadius * circleRadius;

            float discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                // No intersection
                return false;
            }
            else
            {
                // Ray-line segment intersection
                discriminant = (float)Math.Sqrt(discriminant);
                float t1 = (-b - discriminant) / (2 * a);
                float t2 = (-b + discriminant) / (2 * a);

                if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
