using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Math
{
    struct Vector3
    {
        public Vector3(float x_, float y_, float z_)
        {
            x = x_; y = y_; z = z_;
        }

        public static implicit operator Vector3(Vector4 v)
        {
            return new Vector3(v.x, v.y, v.z);
        }

        public float x, y, z;
    }
}
