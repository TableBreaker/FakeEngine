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

        public static implicit operator Vector4(Vector3 v)
        {
            return new Vector4(v.x, v.y, v.z, 1f);
        }

        public float x, y, z, w;
    }
}
