using System;
using System.Windows.Forms;

namespace SharpRender.Mathematics
{
    sealed class Matrix4x4
    {
        public Matrix4x4()
        {
            values = new float[]
            {
                1, 0, 0, 0,
                0, 1, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1,
            };
        }

        public Matrix4x4(float[] values_)
        {
            if (values_.Length != dimension * dimension)
                throw new ArgumentException("Matrix4x4 must have 16 elements");

            values = values_;
        }

        public float Get(int row, int col)
        {
            if (row < 0 || row >= dimension || col < 0 || col >= dimension)
                throw new ArgumentException("Coordinates are invalid");

            return values[row * dimension + col];
        }

        public void Set(int row, int col, float val)
        {
            if (row < 0 || row >= dimension || col < 0 || col >= dimension)
                throw new ArgumentException("Coordinates are invalid");

            values[row * dimension + col] = val;
        }

        public Vector4 GetRow(int row)
        {
            if (row < 0 || row >= dimension)
                throw new ArgumentException("Row number is invalid");

            return new Vector4(Get(row, 0), Get(row, 1), Get(row, 2), Get(row, 3));
        }

        public Vector4 GetColumn(int col)
        {
            if (col < 0 || col >= dimension)
                throw new ArgumentException("Col number is invalid");

            return new Vector4(Get(0, col), Get(1, col), Get(2, col), Get(3, col));
        }

        private float Minor(int row, int col)
        {
            Matrix3x3 sub = new Matrix3x3();
            for (var i = 0; i < dimension; i++)
                for (var j = 0; j < dimension; j++)
                {
                    if (i != row && j != col)
                    {
                        int r = i < row ? i : i - 1;
                        int c = j < col ? j : j - 1;
                        sub.Set(r, c, Get(i, j));
                    }
                }

            return sub.Determinant();
        }

        public float Determinant()
        {
            return Get(0, 0) * Minor(0, 0) - Get(0, 1) * Minor(0, 1) + Get(0, 2) * Minor(0, 2) - Get(0, 3) * Minor(0, 3);
        }

        public Matrix4x4 Transposed()
        {
            var newValues = new float[dimension * dimension];
            for (var i = 0; i < newValues.Length; i++)
            {
                newValues[i] = Get(i % dimension, i / dimension);
            }
            return new Matrix4x4(newValues);
        }

        public Matrix4x4 Inverted()
        {
            var det = Determinant();
            if (det == 0f)
                throw new ArgumentException("The determinant is zero");

            Matrix4x4 inv = new Matrix4x4();
            for (var i = 0; i < dimension; i++)
                for (var j = 0; j < dimension; j++)
                {
                    int sign = (i + j) % 2 == 0 ? 1 : -1;
                    inv.Set(j, i, sign * Minor(i, j));
                }

            return inv * (1f / det);
        }

        public static Matrix4x4 operator + (Matrix4x4 a, Matrix4x4 b)
        {
            var m = new Matrix4x4();
            for (var i = 0; i < dimension; i++)               
                m.values[i] = a.values[i] + b.values[i];
            
            return m;
        }

        public static Matrix4x4 operator - (Matrix4x4 a, Matrix4x4 b)
        {
            var m = new Matrix4x4();
            for (var i = 0; i < m.values.Length; i++)
                m.values[i] = a.values[i] - b.values[i];

            return m;
        }

        public static Matrix4x4 operator * (Matrix4x4 a, Matrix4x4 b)
        {
            var newValues = new float[dimension * dimension];
            for (var i = 0; i < dimension; i++)
                for (var j = 0; j < dimension; j++)
                {
                    newValues[i * dimension + j] = a.GetRow(i).Dot(b.GetColumn(j));
                }

            return new Matrix4x4(newValues);
        }

        public static Matrix4x4 operator * (Matrix4x4 m, float c)
        {
            var newValues = new float[dimension * dimension];
            for (var i = 0; i < newValues.Length; i++)
                newValues[i] = m.values[i] * c;

            return new Matrix4x4(newValues);
        }

        public static Matrix4x4 operator *(float c, Matrix4x4 m)
        {
            return m * c;
        }

        public static Vector4 operator * (Matrix4x4 m, Vector4 v)
        {
            var newVec = new float[dimension];
            for (var i = 0; i < dimension; i++)
                newVec[i] = m.GetRow(i).Dot(v);

            return new Vector4(newVec);
        }

        public static bool operator == (Matrix4x4 a, Matrix4x4 b)
        {
            var equal = true;
            for (var i = 0; i < dimension; i++)
                for (var j = 0; j < dimension; j++)
                    equal &= a.Get(i, j) == b.Get(i, j);

            return equal;
        }

        public static bool operator != (Matrix4x4 a, Matrix4x4 b)
        {
            return !(a == b);
        }

        public static implicit operator Matrix3x3(Matrix4x4 m)
        {
            return new Matrix3x3(new float[]
            {
                m.Get(0, 0), m.Get(0, 1), m.Get(0, 2),
                m.Get(1, 0), m.Get(1, 1), m.Get(1, 2),
                m.Get(2, 0), m.Get(2, 1), m.Get(2, 2),
            });
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Matrix4x4))
                return false;

            return this == (Matrix4x4)obj;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(values);
        }

        public float[] values;
        public const int dimension = 4;
    }
}
