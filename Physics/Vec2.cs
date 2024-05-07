namespace GXPEngine
{
    public struct Vec2
    {
        public float x;
        public float y;

        public readonly static Vec2 zero = new Vec2(0, 0);

        public Vec2(float pX = 0, float pY = 0)
        {
            x = pX;
            y = pY;
        }

        //* Instance methods *//

        public void SetXY(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetXY(Vec2 vec)
        {
            x = vec.x;
            y = vec.y;
        }

        /// <summary>Normalizes the vector</summary>
        public void Normalize()
        {
            float length = Length();
            x /= length;
            y /= length;
        }

        /// <summary>Return a new normalized version of the vector</summary>
        public Vec2 Normalized()
        {
            float length = Length();
            if (float.IsNaN(length) || length == 0) return new Vec2(0, 0);
            else return new Vec2(x / length, y / length);
        }

        /// <summary>Sets the angle of the vector in degrees</summary>
        public void SetAngleDegrees(float degrees) => SetAngleRadians(DegToRad(degrees));

        /// <summary>Sets the angle of the vector in radians</summary>
        public void SetAngleRadians(float radians)
        {
            float length = Length();
            x = Mathf.Cos(radians) * length;
            y = Mathf.Sin(radians) * length;
        }

        /// <summary>Returns the angle of the vector in degrees</summary>
        public float GetAngleDegrees() => RadToDeg(GetAngleRadians());

        /// <summary>Returns the angle of the vector in radians</summary>
        public float GetAngleRadians() => Mathf.Atan2(y, x);

        /// <summary>Rotates the vector by a given amount of degrees</summary>
        public void RotateDegrees(float degrees) => RotateRadians(DegToRad(degrees));

        /// <summary>Rotates the vector by a given amount of radians</summary>
        public void RotateRadians(float radians)
        {
            float cos = Mathf.Cos(radians);
            float sin = Mathf.Sin(radians);
            float tempx = x * cos - y * sin;
            float tempy = x * sin + y * cos;
            SetXY(tempx, tempy);
        }

        /// <summary>Rotates the vector around a given point by a given amount of degrees</summary>
        public void RotateAroundDegrees(Vec2 point, float degrees) => RotateAroundRadians(point, DegToRad(degrees));

        /// <summary>Rotates the vector around a given point by a given amount of radians</summary>
        public void RotateAroundRadians(Vec2 point, float radians)
        {
            Translate(-point.x, -point.y);
            RotateRadians(radians);
            Translate(point.x, point.y);
        }

        /// <summary>Returns the dot product of the vector and another vector</summary>
        public float Dot(Vec2 other) => x * other.x + y * other.y;

        /// <summary>Returns a new vector that is the normal of the vector</summary>
        public Vec2 Normal() => new Vec2(-y, x).Normalized();

        /// <summary>Reflects the vector based on the given normal vector and bounciness coefficient</summary>
        /// <param name="normal">The normal vector to which the vector will be reflected to</param>
        /// <param name="coefficient">The coefficient of bouncyness</param>
        public void Reflect(Vec2 normal, float coefficient = 1f)
        {
            this -= (1 + coefficient) * Dot(normal) * normal;
        }

        /// <summary>Translates the vector by a given vector</summary>
        public void Translate(Vec2 vector)
        {
            x += vector.x;
            y += vector.y;
        }

        /// <summary>Translates the vector by a given amount of x and y</summary>
        public void Translate(float dx, float dy)
        {
            x += dx;
            y += dy;
        }

        /// <summary>Returns the length of the vector</summary>
        public float Length() => Mathf.Sqrt(x * x + y * y);

        /// <summary>Returns the squared length of the vector</summary>
        public float SquareLength() => x * x + y * y;

        /// <summary>Returns the distance to another vector</summary>
        public float DistanceTo(Vec2 target) => new Vec2(target.x - x, target.y - y).Length();

        //* Static methods *//

        /// <summary>Returns a unit vector with random x and y values</summary>
        public static Vec2 GetRandomUnitVector() => GetUnitVectorRad(Utils.Random(0, Mathf.PI * 2));

        /// <summary>Returns a unit vector based on given degrees</summary>
        /// <param name="degrees">The degrees which the unit vector will be calculated from</param>
        public static Vec2 GetUnitVectorDeg(float degrees) => GetUnitVectorRad(DegToRad(degrees));

        /// <summary>Returns a unit vector based on given radians</summary>
        /// <param name="radians">The radians which the unit vector will be calculated from</param>
        public static Vec2 GetUnitVectorRad(float radians) => new Vec2(Mathf.Cos(radians), Mathf.Sin(radians));

        /// <summary>Converts degrees to radians
        /// <param name="degrees">Angle in degrees to be converted to radians</param>
        public static float DegToRad(float degrees) => degrees * Mathf.PI / 180f;

        /// <summary>Converts radians to degrees</summary>
        /// <param name="radians">Angle in radians to be converted to degrees</param>
        public static float RadToDeg(float radians) => radians / Mathf.PI * 180f;

        /// <summary>Creates a new cartesian vector from a polar vector</summary>
        /// <param name="length">The length of the polar vector</param>
        /// <param name="angle">The angle of rotation of the polar vector</param>
        public static Vec2 PolarToCartesian(float length, float angle) => new Vec2(Mathf.Cos(angle) * length, Mathf.Sin(angle) * length);

        /// <summary>Returns the unit normal of a given vector</summary>
        public static Vec2 UnitNormal(Vec2 vector) => new Vec2(-vector.y, vector.x).Normalized();

        /// <summary>Returns the normal of a given vector/// </summary>
        public static Vec2 Normal(Vec2 vector) => new Vec2(-vector.y, vector.x);

        /// <summary>Returns true if the vector is approximately zero</summary>
        public static bool IsZero(Vec2 vector, float tolerance = 0.0001f) => IsApporximately(vector.Length(), 0, tolerance);

        /// <summary>Returns true if the two floats are approximately equal, within a given tolerance</summary>
        private static bool IsApporximately(float a, float b, float tolerance = 0.0001f) => Mathf.Abs(a - b) < tolerance;

        //* Operators *//

        public static Vec2 operator +(Vec2 left, Vec2 right) => new Vec2(left.x + right.x, left.y + right.y);
        public static Vec2 operator -(Vec2 left, Vec2 right) => new Vec2(left.x - right.x, left.y - right.y);

        public static Vec2 operator -(Vec2 vector) => new Vec2(-vector.x, -vector.y);

        public static Vec2 operator *(Vec2 left, float right) => new Vec2(left.x * right, left.y * right);
        public static Vec2 operator *(float left, Vec2 right) => new Vec2(right.x * left, right.y * left);
        public static Vec2 operator *(Vec2 left, Vec2 right) => new Vec2(left.x * right.x, left.y * right.y);

        public static Vec2 operator /(Vec2 left, float right) => new Vec2(left.x / right, left.y / right);

        public static bool operator ==(Vec2 left, Vec2 right) => IsApporximately(left.x, right.x) && IsApporximately(left.y, right.y);
        public static bool operator !=(Vec2 left, Vec2 right) => IsApporximately(left.x, right.x) && IsApporximately(left.y, right.y);

        //* Overrides *//

        public override string ToString() => $"[Vec2] x: {x}, y: {y})";

        public override bool Equals(object obj)
        {
            return obj is Vec2 vec &&
                   x == vec.x &&
                   y == vec.y;
        }

        public override int GetHashCode()
        {
            int hashCode = 1502939027;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            return hashCode;
        }
    }
}