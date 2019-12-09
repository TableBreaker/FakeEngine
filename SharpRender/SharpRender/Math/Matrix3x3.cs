using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Math
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



        public float m00, m01, m02,
                     m10, m11, m12,
                     m20, m21, m22;
    }
}
