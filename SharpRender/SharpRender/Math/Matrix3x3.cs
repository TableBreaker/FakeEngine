using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Mathematics
{
    struct Matrix3x3
    {
        public Matrix3x3(float a00, float a01, float a02,
                         float a10, float a11, float a12,
                         float a20, float a21, float a22)
        {
            m00 = a00; m01 = a01; m02 = a02;
            m10 = a10; m11 = a11; m12 = a12;
            m20 = a20; m21 = a21; m22 = a22;
        }

        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3(
                a.m00 + b.m00, a.m01 + b.m01, a.m02 + b.m02,
                a.m10 + b.m10, a.m11 + b.m11, a.m12 + b.m12,
                a.m20 + b.m20, a.m21 + b.m21, a.m22 + b.m22);
        }

        public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3(
                a.m00 - b.m00, a.m01 - b.m01, a.m02 - b.m02,
                a.m10 - b.m10, a.m11 - b.m11, a.m12 - b.m12,
                a.m20 - b.m20, a.m21 - b.m21, a.m22 - b.m22);
        }

        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
        {
            return new Matrix3x3(
                a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20, 
                a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21, 
                a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22,

                a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20,
                a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21,
                a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22,

                a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20,
                a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21,
                a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22);
        }

        public static Matrix3x3 operator *(Matrix3x3 m, float c)
        {
            return new Matrix3x3(
                m.m00 * c, m.m01 * c, m.m02 * c,
                m.m10 * c, m.m11 * c, m.m12 * c,
                m.m20 * c, m.m21 * c, m.m22 * c);
        }

        public static Matrix3x3 operator *(float c, Matrix3x3 m)
        {
            return m * c;
        }

        public static Vector3 operator *(Matrix3x3 m, Vector3 v)
        {
            return new Vector3(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z);
        }

        public static Matrix3x3 operator /(Matrix3x3 m, float c)
        {
            return new Matrix3x3(
                m.m00 / c, m.m01 / c, m.m02 / c,
                m.m10 / c, m.m11 / c, m.m12 / c,
                m.m20 / c, m.m21 / c, m.m22 / c);
        }

        public static implicit operator Matrix3x3(Matrix4x4 m)
        {
            return new Matrix3x3(
                m.m00, m.m01, m.m02,
                m.m10, m.m11, m.m12,
                m.m20, m.m21, m.m22);
        }

        public static bool operator == (Matrix3x3 a, Matrix3x3 b)
        {
            return a.m00 == b.m00 && a.m01 == b.m01 && a.m02 == b.m02 &&
                    a.m10 == b.m10 && a.m11 == b.m11 && a.m12 == b.m12 &&
                    a.m20 == b.m20 && a.m21 == b.m21 && a.m22 == b.m22;
        }

        public static bool operator != (Matrix3x3 a, Matrix3x3 b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Matrix3x3))
                return false;

            return this == (Matrix3x3)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(HashCode.Combine(m00, m01, m02),
                                    HashCode.Combine(m10, m11, m12),
                                    HashCode.Combine(m20, m21, m22));
        }

        public float m00, m01, m02,
                     m10, m11, m12,
                     m20, m21, m22;
    }
}
