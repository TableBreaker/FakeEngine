using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Mathematics;

namespace SharpRender.Render
{
    class Object
    {
        public Object(Vector3[] vectices_, Vector4[] colors_, Vector3[] indices_)
        {
            for (var i = 0; i < indices_.Length; i++)
            {

            }
        }
        
        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public Vector3 scale { get; set; }

        public List<Triangle> _triangles = new List<Triangle>();
    }
}
