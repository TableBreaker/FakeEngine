using SharpRender.Render;
using System;
using System.Drawing;
using System.Collections.Generic;

namespace SharpRender.Mathematics
{
    static class Utils
    {
        public const float PI = 3.141592653589793238462643383279502884f;

        public static float Clamp(float value, float min, float max)
        {
            if (value > max) return max;
            else if (value < min) return min;
            else return value;
        }

        public static Vector4 Clamp(Vector4 value, float min, float max)
        {
            if (value.x < min) value.x = min;
            if (value.y < min) value.y = min;
            if (value.z < min) value.z = min;
            if (value.w < min) value.w = min;

            if (value.x > max) value.x = max;
            if (value.y > max) value.y = max;
            if (value.z > max) value.z = max;
            if (value.w > max) value.w = max;

            return value;
        }

        public static float Lerp(float a, float b, float t)
        {
            return (1 - t) * a + t * b;
        }

        public static float Magnitude(params float[] elements)
        {
            if (elements == null)
                return 0;

            var sqrm = 0f;
            foreach (var e in elements)
            {
                sqrm += e * e;
            }
            return MathF.Sqrt(sqrm);
        }

        public static void Swap<T>(ref T left, ref T right)
        {
            var temp = left;
            left = right;
            right = temp;
        }

        public static void Swap<T>(this T[] array, int leftIdx, int rightIdx)
        {
            if (leftIdx < 0 || leftIdx >= array.Length || rightIdx < 0 || rightIdx >= array.Length)
                return;

            var temp = array[leftIdx];
            array[leftIdx] = array[rightIdx];
            array[rightIdx] = temp;
        }

        public static void Swap<T>(this List<T> list, int leftIdx, int rightIdx)
        {
            if (leftIdx < 0 || leftIdx >= list.Count || rightIdx < 0 || rightIdx >= list.Count)
                return;

            var temp = list[leftIdx];
            list[leftIdx] = list[rightIdx];
            list[rightIdx] = temp;
        }

        public static Vector3 Normal(Vector3 a, Vector3 b, Vector3 c)
        {
            var t1 = b - a;
            var t2 = c - a;
            return t1.Cross(t2).normalized;
        }

        public static Vector3 Reflect(Vector3 i, Vector3 normal)
        {
            return i - normal * (2f * normal.Dot(i));
        }

        public static float DegToRad(float degrees)
        {
            return degrees * PI / 180f;
        }

        public static Matrix4x4 Translate(Matrix4x4 mat, Vector3 move)
        {
            var translationMatrix = new Matrix4x4();
            translationMatrix.Set(0, 3, move.x);
            translationMatrix.Set(1, 3, move.y);
            translationMatrix.Set(2, 3, move.z);

            return translationMatrix * mat;
        }

        public static Matrix4x4 RotateX(Matrix4x4 mat, float degrees)
        {
            var angle = DegToRad(degrees);
            var rotationMatrix = new Matrix4x4();
            rotationMatrix.Set(1, 1, MathF.Cos(angle));
            rotationMatrix.Set(2, 1, -MathF.Sin(angle));
            rotationMatrix.Set(1, 2, MathF.Sin(angle));
            rotationMatrix.Set(2, 2, MathF.Cos(angle));

            return rotationMatrix * mat;
        }

        public static Matrix4x4 RotateY(Matrix4x4 mat, float degrees)
        {
            var angle = DegToRad(degrees);
            var rotationMatrix = new Matrix4x4();
            rotationMatrix.Set(0, 0, MathF.Cos(angle));
            rotationMatrix.Set(0, 2, MathF.Sin(angle));
            rotationMatrix.Set(2, 0, -MathF.Sin(angle));
            rotationMatrix.Set(2, 2, MathF.Cos(angle));

            return rotationMatrix * mat;
        }

        public static Matrix4x4 RotateZ(Matrix4x4 mat, float degrees)
        {
            var angle = DegToRad(degrees);
            var rotationMatrix = new Matrix4x4();
            rotationMatrix.Set(0, 0, MathF.Cos(angle));
            rotationMatrix.Set(0, 1, -MathF.Sin(angle));
            rotationMatrix.Set(1, 0, MathF.Sin(angle));
            rotationMatrix.Set(1, 1, MathF.Cos(angle));

            return rotationMatrix * mat;
        }

        public static Matrix4x4 Scale(Matrix4x4 mat, Vector3 scale)
        {
            var scaleMatrix = new Matrix4x4();
            scaleMatrix.Set(0, 0, scale.x);
            scaleMatrix.Set(1, 1, scale.y);
            scaleMatrix.Set(2, 2, scale.z);
            return scaleMatrix * mat;
        }

        // Returns a LookAt matrix.
        public static Matrix4x4 LookAt(Vector3 position, Vector3 target, Vector3 up)
        {
            // calculate the (reverse) direction vector
            var direction = (position - target).normalized;
            // calculate the basis vector that points to the right
            var cameraRight = up.normalized.Cross(direction).normalized;
            // calculate the up basis vector
            var cameraUp = direction.normalized.Cross(cameraRight);
            // calculate matrices
            var rotational = new Matrix4x4(new float[]
            {
                cameraRight.x, cameraRight.y, cameraRight.z, 0f,
                cameraUp.x, cameraUp.y, cameraUp.z, 0f,
                direction.x, direction.y, direction.z, 0f,
                0f, 0f, 0f, 0f,
            });
            // translate world coord to view coord
            var positional = new Matrix4x4(new float[]
            {
                1f, 0f, 0f, -position.x,
                0f, 1f, 0f, -position.y,
                0f, 0f, 1f, -position.z,
                0f, 0f, 0f, 1f,
            });

            return rotational * positional;
        }

        // returns an area of the given triangle
        public static float Area(Triangle tri)
        {
            var left = (tri.vertices[1].x - tri.vertices[0].x) * (tri.vertices[2].y - tri.vertices[0].y);
            var right = (tri.vertices[2].x - tri.vertices[0].x) * (tri.vertices[1].y - tri.vertices[0].y);
            return MathF.Abs(.5f * (left - right));
        }

        public static Color VecToColor(Vector4 col)
        {
            return Color.FromArgb((int)(col.w * 255), (int)(col.x * 255), (int)(col.y * 255), (int)(col.z * 255));
        }

        public static Vector4 ColorToVec(Color col)
        {
            return new Vector4(col.R / 255f, col.G / 255f, col.B / 255f, col.A / 255f);
        }

        // return a signum of x
        public static int Sign(int x)
        {
            return x == 0 ? 0 : (x > 0 ? 1 : -1);
        }
    }
}
