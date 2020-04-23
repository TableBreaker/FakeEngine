using System;

namespace SharpRender.Mathematics
{
    static class MathUtility
    {
        public const float PI = 3.141592653589793238462643383279502884f;

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

        public static Vector3 Normal(Vector3 a, Vector3 b, Vector3 c)
        {
            var t1 = b - a;
            var t2 = c - a;
            return t1.Cross(t2).normalized;
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
    }
}
