using System;
using System.Collections.Generic;
using SharpRender.Mathematics;

namespace SharpRender.Render
{
    class Object
    {
        public Object(Triangle[] _triangles)
        {

        }

        public Object(Vector3[] vertices_, Vector4[] colors_, Vector3[] texCoords_, Vector3[] indices_)
        {
            if (colors_.Length != vertices_.Length)
                throw new ArgumentException("Colors mus be specified for each vertex");

            if (texCoords_.Length != indices_.Length * 3)
                throw new ArgumentException("Texture coordinates must be specified for each triangle");

            // vert indices are the same as color indices，one point one vert, but texCoord are duplicated, one point may have more than one texcoord
            for (var i = 0; i < indices_.Length; i++)
            {
                AddTriangle(new Triangle(
                    vertices_[(int)indices_[i].x],
                    vertices_[(int)indices_[i].y], 
                    vertices_[(int)indices_[i].z]));

                Triangles[^1].SetColors(
                    colors_[(int)indices_[i].x],
                    colors_[(int)indices_[i].y],
                    colors_[(int)indices_[i].z]);

                Triangles[^1].SetTexCoords(
                    texCoords_[3 * i],
                    colors_[3 * i + 1],
                    colors_[3 * i + 2]);
            }
        }

        public Object(Vector3[] vertices_, Vector4[] colors_, Vector3[] indices_)
        {
            if (colors_.Length != vertices_.Length)
                throw new ArgumentException("Colors mus be specified for each vertex");

            for (var i = 0; i < indices_.Length; i++)
            {
                AddTriangle(new Triangle(
                    vertices_[(int)indices_[i].x],
                    vertices_[(int)indices_[i].y],
                    vertices_[(int)indices_[i].z]));

                Triangles[^1].SetColors(
                    colors_[(int)indices_[i].x],
                    colors_[(int)indices_[i].y],
                    colors_[(int)indices_[i].z]);
            }
        }

        public Matrix4x4 GetModelMatrix()
        {
            var model = new Matrix4x4();
            model = Utils.Scale(model, scale);
            model = Utils.RotateX(model, rotation.x);
            model = Utils.RotateY(model, rotation.y);
            model = Utils.RotateZ(model, rotation.z);
            model = Utils.Translate(model, position);

            return model;
        }

        public void Reset()
        {
            position = new Vector3();
            scale = new Vector3(1f, 1f, 1f);
            rotation = new Vector3();
        }

        private void AddTriangle(Triangle tri)
        {
            Triangles.Add(tri);
        }

        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public Vector3 scale { get; set; }

        public List<Triangle> Triangles = new List<Triangle>();
    }
}
