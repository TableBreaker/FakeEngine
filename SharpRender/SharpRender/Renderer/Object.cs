using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Mathematics;

namespace SharpRender.Renderer
{
    class Object
    {
        public Object(Vector3[] vectices_, Vector4[] colors_, Vector3[] indices_)
        {
            vertices = vectices_;
            colors = colors_;
            indices = indices_;
        }

        public Vector3[] vertices;
        public Vector4[] colors;
        public Vector3[] indices;
    }
}
