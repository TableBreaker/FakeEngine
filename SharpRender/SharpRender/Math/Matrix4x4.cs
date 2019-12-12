using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Math
{
    struct Matrix4x4
    {
        public Matrix4x4(float a00, float a01, float a02, float a03,
                         float a10, float a11, float a12, float a13,
                         float a20, float a21, float a22, float a23,
                         float a30, float a31, float a32, float a33)
        {
            m00 = a00; m01 = a01; m02 = a02; m03 = a03;
            m10 = a10; m11 = a11; m12 = a12; m13 = a13;
            m20 = a20; m21 = a21; m22 = a22; m23 = a23;
            m30 = a30; m31 = a31; m32 = a32; m33 = a33;
        }

        public static Matrix4x4 operator * (Matrix4x4 a, Matrix4x4 b)
        {
            return new Matrix4x4(
                a.m00 * b.m00 + a.m01 * b.m10 + a.m02 * b.m20 + a.m03 * b.m30,
                a.m00 * b.m01 + a.m01 * b.m11 + a.m02 * b.m21 + a.m03 * b.m31,
                a.m00 * b.m02 + a.m01 * b.m12 + a.m02 * b.m22 + a.m03 * b.m32,
                a.m00 * b.m03 + a.m01 * b.m13 + a.m02 * b.m23 + a.m03 * b.m33,

                a.m10 * b.m00 + a.m11 * b.m10 + a.m12 * b.m20 + a.m13 * b.m30,
                a.m10 * b.m01 + a.m11 * b.m11 + a.m12 * b.m21 + a.m13 * b.m31,
                a.m10 * b.m02 + a.m11 * b.m12 + a.m12 * b.m22 + a.m13 * b.m32,
                a.m10 * b.m03 + a.m11 * b.m13 + a.m12 * b.m23 + a.m13 * b.m33,

                a.m20 * b.m00 + a.m21 * b.m10 + a.m22 * b.m20 + a.m23 * b.m30,
                a.m20 * b.m01 + a.m21 * b.m11 + a.m22 * b.m21 + a.m23 * b.m31,
                a.m20 * b.m02 + a.m21 * b.m12 + a.m22 * b.m22 + a.m23 * b.m32,
                a.m20 * b.m03 + a.m21 * b.m13 + a.m22 * b.m23 + a.m23 * b.m33,
                
                a.m30 * b.m00 + a.m31 * b.m10 + a.m32 * b.m20 + a.m33 * b.m30,
                a.m30 * b.m01 + a.m31 * b.m11 + a.m32 * b.m21 + a.m33 * b.m31,
                a.m30 * b.m02 + a.m31 * b.m12 + a.m32 * b.m22 + a.m33 * b.m32,
                a.m30 * b.m03 + a.m31 * b.m13 + a.m32 * b.m23 + a.m33 * b.m33);
        }

        public static Vector4 operator * (Matrix4x4 m, Vector4 v)
        {
            return new Vector4(
                m.m00 * v.x + m.m01 * v.y + m.m02 * v.z + m.m03 * v.w,
                m.m10 * v.x + m.m11 * v.y + m.m12 * v.z + m.m13 * v.w,
                m.m20 * v.x + m.m21 * v.y + m.m22 * v.z + m.m23 * v.w,
                m.m30 * v.x + m.m31 * v.y + m.m32 * v.z + m.m33 * v.w);
        }

        public static implicit operator Matrix4x4(Matrix3x3 m)
        {
            return new Matrix4x4(
                m.m00, m.m01, m.m02, 1f,
                m.m10, m.m11, m.m12, 1f,
                m.m20, m.m21, m.m22, 1f,
                1f, 1f, 1f, 1f);
        }

        public float m00, m01, m02, m03,
                     m10, m11, m12, m13,
                     m20, m21, m22, m23,
                     m30, m31, m32, m33;
    }
}
