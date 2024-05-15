using System;
using GXPEngine.SceneManager;

namespace GXPEngine.Physics.PhysicsObjects
{
    internal class Windmill : Sprite
    {
        Line windmillWall;
        float rotationSpeed = -GameData.windmillSpinSpeed; // Negative for clockwise rotation
        float currentAngle = 0f;

        public Windmill() : base("colors.png")
        {
            visible = false;
            collider.isTrigger = true;
            windmillWall = new Line(Vec2.zero, Vec2.zero);
            SceneManager.SceneManager.CurrentScene.AddChild(windmillWall);
        }

        private void Update()
        {
            float deltaTime = Time.deltaTime;
            RotateWindmill(deltaTime);
            CheckCollisionWithPlayer();
            if (GameData.ShowColliders) Gizmos.DrawRectangle(x + width / 2, y + height / 2, width, height, color: GameData.ColliderColor);
        }

        private void RotateWindmill(float deltaTime)
        {
            currentAngle += rotationSpeed * deltaTime / 1000;
            currentAngle %= 360;

            Vec2 centerPoint = new Vec2(x + width / 2, y + height / 2);
            float radius = width / 2;
            Vec2 endPoint = centerPoint + Vec2.GetUnitVectorDeg(currentAngle) * radius;

            windmillWall.CallCollider(centerPoint, endPoint);
        }

        private void CheckCollisionWithPlayer()
        {
            Player player = SceneManager.SceneManager.CurrentScene.FindObjectOfType<Player>();
            if (player != null && IsCollidingWithPlayer(player))
            {
                ApplyForceToPlayer(player);
            }
        }

        private bool IsCollidingWithPlayer(Player player)
        {
            Vec2 centerPoint = new Vec2(x + width / 2, y + height / 2);
            float radius = width / 2;
            Vec2 endPoint = centerPoint + Vec2.GetUnitVectorDeg(currentAngle) * radius;

            return LineIntersectsCircle(centerPoint, endPoint, player.Collider.Position, 32);
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

        private void ApplyForceToPlayer(Player player)
        {
            Vec2 forceDirection = Vec2.GetUnitVectorDeg(currentAngle + 90);
            float forceMagnitude = GameData.windmillForceMagnitude;
            player.Collider.AddVelocity(forceDirection * forceMagnitude);
        }
    }
}
