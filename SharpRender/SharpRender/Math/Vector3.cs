using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Math
{
    struct Vector3
    {
        public Vector3(float x_, float y_, float z_)
        {
            x = x_; y = y_; z = z_;
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vector3 Cross(Vector3 a, Vector3 b)
        {
            return default;
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

        public float x, y, z;
    }
}
