using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Mathematics
{
    struct Vector3
    {
        public Vector3(float x_, float y_, float z_)
        {
            x = x_; y = y_; z = z_;
        }

        public float Dot(Vector3 v)
        {
            return Dot(this, v);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public Vector3 Cross(Vector3 v)
        {
            return Cross(this, v);
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
        }

        public static Vector3 operator *(Vector3 v, float c)
        {
            return new Vector3(v.x * c, v.y * c, v.z * c);
        }

        public static Vector3 operator *(float c, Vector3 v)
        {
            return v * c;
        }

        public static Vector3 operator /(Vector3 v, float c)
        {
            return new Vector3(v.x / c, v.y / c, v.z / c);
        }

        public static implicit operator Vector3(Vector4 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public static bool operator ==(Vector3 a, Vector3 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z;
        }

        public static bool operator !=(Vector3 a, Vector3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector3))
                return false;

            return this == (Vector3)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z);
        }

        public float Magnitude
        {
            get
            {
                return MathUtility.Magnitude(x, y, z);
            }
        }

        public Vector3 normalized
        {
            get
            {
                var mag = Magnitude;
                return this / (mag == 0f ? 1f : mag);
            }
        }

        public static readonly Vector3 One = new Vector3(1f, 1f, 1f);
        public static readonly Vector3 Zero = new Vector3(0f, 0f, 0f);

        public float x, y, z;
    }
}
