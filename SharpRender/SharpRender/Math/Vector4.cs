using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Mathematics
{
    struct Vector4
    {
        public Vector4(float x_, float y_, float z_, float w_)
        {
            x = x_; y = y_; z = z_; w = w_;
        }

        public float Dot(Vector4 v)
        {
            return Dot(this, v);
        }

        public static float Dot(Vector4 a, Vector4 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
        }

        public static Vector4 operator +(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
        }

        public static Vector4 operator -(Vector4 a, Vector4 b)
        {
            return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
        }

        public static Vector4 operator *(Vector4 v, float c)
        {
            return new Vector4(v.x * c, v.y * c, v.z * c, v.w * c);
        }

        public static Vector4 operator *(float c, Vector4 v)
        {
            return v * c;
        }

        public static Vector4 operator /(Vector4 v, float c)
        {
            return new Vector4(v.x / c, v.y / c, v.z / c, v.w / c);
        }

        public static implicit operator Vector4(Vector3 v)
        {
            return new Vector4(v.x, v.y, v.z, 1f);
        }

        public static bool operator ==(Vector4 a, Vector4 b)
        {
            return a.x == b.x && a.y == b.y && a.z == b.z && a.w == b.w;
        }

        public static bool operator !=(Vector4 a, Vector4 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Vector4))
                return false;

            return this == (Vector4)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(x, y, z, w);
        }

        public float Magnitude
        {
            get
            {
                return MathUtility.Magnitude(x, y, z, w);
            }
        }

        public Vector4 normalized
        {
            get
            {
                var mag = Magnitude;
                return this / (mag == 0f ? 1f : mag);
            }
        }

        public Vector3 fromHomogeneous
        {
            get
            {
                return new Vector3(x / w, y / w, z / w);
            }
        }

        public Vector4 fromHomogeneous4
        {
            get
            {
                return new Vector4(x / w, y / w, z / w, w);
            }
        }

        public static readonly Vector4 One = new Vector4(1f, 1f, 1f, 1f);
        public static readonly Vector4 Zero = new Vector4(0f, 0f, 0f, 0f);

        public float x, y, z, w;
    }
}
