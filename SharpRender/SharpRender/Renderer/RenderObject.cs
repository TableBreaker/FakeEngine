using System;
using System.Collections.Generic;
using System.Linq;
using SharpRender.Mathematics;

namespace SharpRender.Render
{
    class RenderObject
    {
        public RenderObject(Triangle[] _triangles)
        {

        }

        public RenderObject(Vector3[] vertices_, Vector4[] colors_, Vector3[] texCoords_, Vector3[] indices_)
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

        public RenderObject(Vector3[] vertices_, Vector4[] colors_, Vector3[] indices_)
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

        public void SetReflection(bool xy, bool xz, bool yz)
        {
            _reflectionXY = xy;
            _reflectionXZ = xz;
            _reflectionYZ = yz;
        }

        public Vector3 GetReflection()
        {
            return new Vector3(_reflectionXY ? 1f : 0f, _reflectionYZ ? 1f : 0f, _reflectionXZ ? 1f : 0f);
        }

        public void SetMaterialParameters(float ambience, float diffuse, float specular, int shininess)
        {
            Material.SetAmbient(ambience);
            Material.SetDiffuse(diffuse);
            Material.SetSpecular(specular);
            Material.SetShininess(shininess);
        }

        public Vector4 GetMaterialParameters()
        {
            return new Vector4(Material.GetAmbient(), Material.GetDiffuse(), Material.GetSpecular(), Material.GetShininess());
        }

        public void SetMaterialColor(Vector4 col)
        {
            Material.SetColor(col);
        }

        public Vector4 GetMaterialColor()
        {
            return Material.GetColor();
        }

        public void ResetMaterial()
        {
            Material.Reset();
        }

        public void SetTextureIndex(int iTex)
        {
            _textureIndex = iTex;
        }

        public void RemoveTexture()
        {
            _textureIndex = -1;
        }

        public int GetTextureIndex()
        {
            return _textureIndex;
        }

        // set the colors of the object's triangles, if vertices = true then is assumes the vector defines the color in each vertex (3 per triangle)
        public void SetTriangleColors(Vector4[] cols, bool vertices)
        {
            for (var i = 0; i < Triangles.Count; i++)
            {
                if (vertices)
                {
                    // color per vertex
                    if (cols.Length != Triangles.Count * 3)
                    {
                        throw new ArgumentException("The color should be specified for each vertex.");
                    }
                    Triangles[i].SetColors(cols[3 * i], cols[3 * i + 1], cols[3 * i + 2]);
                }
                else
                {
                    // color per triangle
                    if (cols.Length != Triangles.Count)
                    {
                        throw new ArgumentException("The color should be specified for each triangle");
                    }
                    Triangles[i].SetColor(cols[i]);
                }
            }
        }

        public Matrix4x4 GetModelMatrix()
        {
            var model = new Matrix4x4();
            model = Utils.Scale(model, Scale);
            model = Utils.RotateX(model, Rotation.x);
            model = Utils.RotateY(model, Rotation.y);
            model = Utils.RotateZ(model, Rotation.z);
            model = Utils.Translate(model, Position);

            return model;
        }

        public void Reset()
        {
            Position = new Vector3();
            Scale = new Vector3(1f, 1f, 1f);
            Rotation = new Vector3();
        }

        private void AddTriangle(Triangle tri)
        {
            Triangles.Add(tri);
        }

        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
        public Vector3 Scale { get; set; }
        public Material Material { get; set; }

        public List<Triangle> Triangles = new List<Triangle>();

        private bool _reflectionXY;
        private bool _reflectionXZ;
        private bool _reflectionYZ;


        // the number of the currently used texture (-1 if no texture)
        private int _textureIndex;
    }
}
