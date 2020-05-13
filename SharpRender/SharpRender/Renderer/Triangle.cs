using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Mathematics;

namespace SharpRender.Render
{
    class Triangle
    {
        public Triangle(Vector4 first, Vector4 second, Vector4 third)
        {
            vertices = new Vector4[] { first, second, third };
            colors = new Vector4[] { Vector4.One, Vector4.One, Vector4.One };
            texCoords = new Vector4[] { Vector3.Zero, Vector3.Zero, Vector3.Zero };
            CalculateNormals();
        }

        public void SetColor(Vector4 color)
        {
            colors = new Vector4[] { color, color, color };
        }

        public void SetColors(Vector4 first, Vector4 second, Vector4 third)
        {
            colors = new Vector4[] { first, second, third };
        }

        public void SetNormals(Vector3 first, Vector3 second, Vector3 third)
        {
            normals = new Vector3[] { first, second, third };
        }

        public void SetTexCoords(Vector4 first, Vector4 second, Vector4 third)
        {
            texCoords = new Vector4[] { first, second, third };
        }

        public void CalculateNormals()
        {
            var normal = Utils.Normal(vertices[0].fromHomogeneous, vertices[1].fromHomogeneous, vertices[2].fromHomogeneous);
            normals = new Vector3[] { normal, normal, normal };
        }

        public Triangle GetTransformed(Matrix4x4 mat, Matrix3x3 normalMat)
        {
            var transformed = new Triangle(vertices[0], vertices[1], vertices[2]);
            for (var i = 0; i < vertices.Length; i++)
            {
                transformed.vertices[i] = mat * vertices[i];
                transformed.colors[i] = colors[i];
                transformed.texCoords[i] = texCoords[i];

                // we need a different matrix for the normal transformation (inversed-transposed).
                // 为了保证normal仍然垂直
                transformed.normals[i] = (normalMat * normals[i]).normalized;
            }
            return transformed;
        }

        public Vector4[] vertices;
        public Vector4[] colors;
        public Vector4[] texCoords;
        public Vector3[] normals;
    }
}
