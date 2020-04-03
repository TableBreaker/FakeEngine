﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace SharpRender.Mathematics
{
    static class MathUtility
    {
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
    }
}
