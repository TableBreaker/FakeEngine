using System;
using System.Net.WebSockets;

namespace FakeEngine.Mathematics
{
    sealed class Matrix3x3
    {
        public Matrix3x3()
        {
            values = new float[]
            {
                1, 0, 0,
                0, 1, 0,
                0, 0, 1,
            };
        }

        public Matrix3x3(float[] values_)
        {
            if (values_.Length != dimension * dimension)
                throw new ArgumentException("Matrix3x3 must have 9 elements");

            values = values_;
        }

        public float Get(int row, int col)
        {
            if (row < 0 || row >= dimension || col < 0 || col >= dimension)
                throw new ArgumentException("Coordinates ar invalid");

            return values[row * dimension + col];
        }

        public void Set(int row, int col, float val)
        {
            if (row < 0 || row >= dimension || col < 0 || col >= dimension)
                throw new ArgumentException("Corrdinates are invalid");

            values[row * dimension + col] = val;
        }

        public Vector3 GetRow(int row)
        {
            if (row < 0 || row >= dimension)
                throw new ArgumentException("Row number is invalid");

            return new Vector3(Get(row, 0), Get(row, 1), Get(row, 2));
        }

        public Vector3 GetColumn(int col)
        {
            if (col < 0 || col >= dimension)
                throw new ArgumentException("Col number is invalid");

            return new Vector3(Get(0, col), Get(1, col), Get(2, col));
        }

        public float Determinant()
        {
            var minor1 = Get(1, 1) * Get(2, 2) - Get(1, 2) * Get(2, 1);
            var minor2 = Get(1, 0) * Get(2, 2) - Get(1, 2) * Get(2, 0);
            var minor3 = Get(1, 0) * Get(2, 1) - Get(1, 1) * Get(2, 0);

            return Get(0, 0) * minor1 - Get(0, 1) * minor2 + Get(0, 2) * minor3;
        }

        public Matrix3x3 Transposed()
        {
            var newValues = new float[dimension * dimension];
            for (var i = 0; i < newValues.Length; i++)
            {
                newValues[i] = Get(i % dimension, i / dimension);
            }
            return new Matrix3x3(newValues);
        }

        public static Matrix3x3 operator +(Matrix3x3 a, Matrix3x3 b)
        {
            var m = new Matrix3x3();
            for (var i = 0; i < dimension; i++)
                m.values[i] = a.values[i] + b.values[i];

            return m;
        }

        public static Matrix3x3 operator -(Matrix3x3 a, Matrix3x3 b)
        {
            var m = new Matrix3x3();
            for (var i = 0; i < m.values.Length; i++)
                m.values[i] = a.values[i] - b.values[i];
            
            return m;
        }

        public static Matrix3x3 operator *(Matrix3x3 a, Matrix3x3 b)
        {
            var newValues = new float[dimension * dimension];
            for (var i = 0; i < dimension; i++)
                for (var j = 0; j < dimension; j++)
                {
                    newValues[i * dimension + j] = a.GetRow(i).Dot(b.GetColumn(j));
                }

            return new Matrix3x3(newValues);
        }

        public static Matrix3x3 operator *(Matrix3x3 m, float c)
        {
            var newValues = new float[dimension * dimension];
            for (var i = 0; i < newValues.Length; i++)
                newValues[i] = m.values[i] * c;

            return new Matrix3x3(newValues);
        }

        public static Matrix3x3 operator *(float c, Matrix3x3 m)
        {
            return m * c;
        }

        public static Vector3 operator *(Matrix3x3 m, Vector3 v)
        {
            var newVec = new float[dimension];
            for (var i = 0; i < dimension; i++)
                newVec[i] = m.GetRow(i).Dot(v);

            return new Vector3(newVec);
        }

        public static bool operator == (Matrix3x3 a, Matrix3x3 b)
        {
            var equal = true;
            for (var i = 0; i < dimension; i++)
                for (var j = 0; j < dimension; j++)
                    equal &= a.Get(i, j) == b.Get(i, j);

            return equal;
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
            return HashCode.Combine(values);
        }

        public float[] values;
        public const int dimension = 3;
    }
}
