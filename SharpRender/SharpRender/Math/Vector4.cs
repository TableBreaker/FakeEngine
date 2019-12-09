using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Math
{
    struct Vector4
    {
        public Vector4(float x_, float y_, float z_, float w_)
        {
            x = x_; y = y_; z = z_; w = w_;
        }

        public float x, y, z, w;
    }
}
