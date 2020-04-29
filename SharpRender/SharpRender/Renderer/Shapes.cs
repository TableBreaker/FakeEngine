using System;
using System.Collections.Generic;
using SharpRender.Mathematics;

namespace SharpRender.Render
{
    class Shapes
    {
        public const float DEFAULT_RADIUS = 2f;
        public const int DEFAULT_PRECISION_LEVEL = 3;

        public enum Shape { CUBE, PYRAMID, SPHERE, OCTAHEDRON, TETRAHEDRON };

        public RenderObject GenerateObject(Shape objShape, int precision = DEFAULT_PRECISION_LEVEL)
        {
            switch(objShape)
            {
                case Shape.CUBE:
                    return Cube();
                case Shape.PYRAMID:
                    return Pyramid();
                case Shape.TETRAHEDRON:
                    return Tetrahedron();
                case Shape.OCTAHEDRON:
                    return Octahedron();
                case Shape.SPHERE:
                    return Sphere(precision, DEFAULT_RADIUS);
                default:
                    return Cube();
            }
        }

        // returns a cube
        private RenderObject Cube()
        {
            var vertices = new Vector3[]
            {
                new Vector3(1, 1, 1),
                new Vector3(1, 1, -1),
                new Vector3(1, -1, 1),
                new Vector3(1, -1, -1),
                new Vector3(-1, 1, 1),
                new Vector3(-1, 1, -1),
                new Vector3(-1, -1, 1),
                new Vector3(-1, -1, -1),
            };

            var indices = new Vector3[]
            {
                new Vector3(2, 0, 4), new Vector3(4, 6, 2), // front
                new Vector3(2, 3, 1), new Vector3(1, 0, 2), // right
                new Vector3(1, 3, 7), new Vector3(7, 5, 1), // back
                new Vector3(6, 5, 7), new Vector3(6, 4, 5), // left
                new Vector3(0, 1, 5), new Vector3(5, 4, 0), // top
                new Vector3(7, 3, 2), new Vector3(2, 6, 7), // bottom
            };

            var colors = new Vector4[]
            {
                new Vector4(0, 0, 0, 1),
                new Vector4(0, 0, 1, 1),
                new Vector4(0, 1, 0, 1),
                new Vector4(0, 1, 1, 1),
                new Vector4(1, 0, 0, 1),
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 1, 0, 1),
                new Vector4(1, 1, 1, 1),
            };

            var texCoords = new Vector3[]
            {
                // front
                new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0),

                // right
                new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0),
                new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 0),

                // back
                new Vector3(0, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0),
                new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),

                // left
                new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 0),
                new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),

                // top
                new Vector3(1, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0, 1, 0), new Vector3(0, 0, 0), new Vector3(1, 0, 0), 

				// bottom
				new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 1, 0),
                new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 0),
            };

            return new RenderObject(vertices, colors, texCoords, indices);
        }

        // return a pyramid.
        private RenderObject Pyramid()
        {
            var vertices = new Vector3[]
            {
                new Vector3(0, 1, 0),
                new Vector3(1, -1, 1),
                new Vector3(1, -1, -1),
                new Vector3(-1, -1, 1), 
                new Vector3(-1, -1, -1)
            };

            var indices = new Vector3[]
            {
                new Vector3(1, 0, 3), // front
				new Vector3(2, 0, 1), // right
				new Vector3(4, 0, 2), // back
				new Vector3(3, 0, 4), // left
				new Vector3(1, 3, 4), new Vector3(1, 4, 2), // bottom
            };

            var colors = new Vector4[]
            {
                new Vector4(1, 1, 1, 1),
                new Vector4(0.5f, 0.5f, 1, 1), 
                new Vector4(0.5f, 0.5f, 1, 1),
                new Vector4(0.5f, 0.5f, 1, 1), 
                new Vector4(0.5f, 0.5f, 1, 1)
            };

            var texCoords = new Vector3[]
            {
                // front
				new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0), 
				// right
				new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0), 
				// back
				new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0), 
				// left
				new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0), 
				// bottom
				new Vector3(1, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 0, 0),
                new Vector3(1, 1, 0), new Vector3(0, 0, 0), new Vector3(0, 1, 0),

            };

            return new RenderObject(vertices, colors, texCoords, indices);
        }

        // return an octahedron.
        private RenderObject Octahedron()
        {
            var vertices = new Vector3[]
            {
                new Vector3(0, -2, 0),
                new Vector3(1, 0, 1),
                new Vector3(-1, 0, 1),
                new Vector3(1, 0, -1),
                new Vector3(-1, 0, -1),
                new Vector3(0, 2, 0),
            };

            var indices = new Vector3[]
            {
                new Vector3(0, 1, 2), 
                new Vector3(0, 3, 1),
                new Vector3(0, 4, 3), 
                new Vector3(0, 2, 4),
                new Vector3(2, 1, 5), 
                new Vector3(1, 3, 5),
                new Vector3(3, 4, 5), 
                new Vector3(2, 5, 4),
            };

            var colors = new Vector4[]
            {
                new Vector4(1, 1, 1, 1), 
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 1, 1, 1)
            };

            var texCoords = new Vector3[]
            {
                new Vector3(0.5f, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0.5f, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0.5f, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0.5f, 0, 0), new Vector3(1, 1, 0), new Vector3(0, 1, 0),
                new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0),
                new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0),
                new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0),
                new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0),

            };

            return new RenderObject(vertices, colors, texCoords, indices);
        }

        // return a tetrahedron
        private RenderObject Tetrahedron()
        {
            var vertices = new Vector3[]
            {
                new Vector3(0, 1, 0),
                new Vector3(MathF.Sqrt(8f / 9f), -1f / 3f, 0),
                new Vector3(-MathF.Sqrt(2f / 9f), -1f / 3f, MathF.Sqrt(2f / 3f)),
                new Vector3(-MathF.Sqrt(2f / 9f), -1f / 3f, -MathF.Sqrt(2f / 3f)),
            };

            var indices = new Vector3[]
            {
                new Vector3(1, 0, 2),
                new Vector3(3, 0, 1),
                new Vector3(2, 0, 3),
                new Vector3(1, 2, 3), // bottom
            };

            var colors = new Vector4[]
            {
                new Vector4(1, 1, 1, 1),
                new Vector4(1, 0.5f, 1, 1),
                new Vector4(1, 0.5f, 1, 1),
                new Vector4(1, 0.5f, 1, 1),
            };

            var texCoords = new Vector3[]
            {
                new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0),
                new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0),
                new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0), 
				// bottom
				new Vector3(1, 0, 0), new Vector3(0.5f, 1, 0), new Vector3(0, 0, 0),
            };

            return new RenderObject(vertices, colors, texCoords, indices);
        }

        private Vector4 Middle(Vector4 a, Vector4 b)
        {
            var m = (a + b) * .5f;
            m.w = 1f;
            return m;
        }

        private void Spherize(Triangle tri, float radius)
        {
            for (var i = 0; i < tri.vertices.Length; i++)
            {
                tri.vertices[i] = ((Vector3)tri.vertices[i]).normalized; // (xyz.normalized, 1)
                tri.normals[i] = tri.vertices[i];
            }
        }

        // slice one triangle to four triangle (for spherizing a Tetrahedron)
        private Triangle[] RefineTriangle(Triangle tri)
        {
            var refined = new Triangle[4];
            // middle points and middle values
            var mid01 = Middle(tri.vertices[0], tri.vertices[1]);
            var mid12 = Middle(tri.vertices[1], tri.vertices[2]);
            var mid20 = Middle(tri.vertices[2], tri.vertices[0]);

            var colorMid01 = Middle(tri.colors[0], tri.colors[1]);
            var colorMid12 = Middle(tri.colors[1], tri.colors[2]);
            var colorMid20 = Middle(tri.colors[2], tri.colors[0]);

            var normalMid01 = ((tri.normals[0] + tri.normals[1]) * .5f).normalized;
            var normalMid12 = ((tri.normals[1] + tri.normals[2]) * .5f).normalized;
            var normalMid20 = ((tri.normals[2] + tri.normals[0]) * .5f).normalized;

            // construct 4 new triangles
            refined[0] = new Triangle(tri.vertices[0], mid01, mid20);
            refined[1] = new Triangle(tri.vertices[1], mid12, mid01);
            refined[2] = new Triangle(tri.vertices[2], mid20, mid12);
            refined[3] = new Triangle(mid01, mid12, mid20);

            refined[0].SetColors(tri.colors[0], colorMid01, colorMid20);
            refined[1].SetColors(tri.colors[1], colorMid12, colorMid01);
            refined[2].SetColors(tri.colors[2], colorMid20, colorMid12);
            refined[3].SetColors(colorMid01, colorMid12, colorMid20);

            refined[0].SetNormals(tri.normals[0], normalMid01, normalMid20);
            refined[1].SetNormals(tri.normals[1], normalMid12, normalMid01);
            refined[2].SetNormals(tri.normals[2], normalMid20, normalMid12);
            refined[3].SetNormals(normalMid01, normalMid12, normalMid20);

            return refined;
        }

        // generate a sphere
        private RenderObject Sphere(int precisionLevel, float radius)
        {
            // generate icosahedron first
            var t = (1f + MathF.Sqrt(5f)) / 2f;

            var vertices = new Vector3[]
            {
                new Vector3(-1, t, 0), 
                new Vector3(1, t, 0),
                new Vector3(-1, -t, 0),
                new Vector3(1, -t, 0),
                new Vector3(0, -1, t),
                new Vector3(0, 1, t), 
                new Vector3(0, -1, -t),
                new Vector3(0, 1, -t),
                new Vector3(t, 0, -1), 
                new Vector3(t, 0, 1), 
                new Vector3(-t, 0, -1),
                new Vector3(-t, 0, 1),
            };

            var indices = new Vector3[]
            {
                new Vector3(0, 11, 5),
                new Vector3(0, 5, 1),
                new Vector3(0, 1, 7),
                new Vector3(0, 7, 10),
                new Vector3(0, 10, 11),

                new Vector3(1, 5, 9),
                new Vector3(5, 11, 4),
                new Vector3(11, 10, 2),
                new Vector3(10, 7, 6),
                new Vector3(7, 1, 8),

                new Vector3(3, 9, 4),
                new Vector3(3, 4, 2),
                new Vector3(3, 2, 6),
                new Vector3(3, 6, 8),
                new Vector3(3, 8, 9),

                new Vector3(4, 9, 5),
                new Vector3(2, 4, 11),
                new Vector3(6, 2, 10),
                new Vector3(8, 6, 7),
                new Vector3(9, 8, 1)
            };

            var colors = new Vector4[]
            {
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1),
                new Vector4(1, 0, 1, 1), 
                new Vector4(1, 0, 1, 1),
            };

            var iso = new RenderObject(vertices, colors, indices);

            // refine each triangle N times
            for (var i = 0; i < precisionLevel; i++)
            {
                var refined = new List<Triangle>();
                foreach (var tri in iso.Triangles)
                {
                    var tris = RefineTriangle(tri);
                    // append triangles
                    refined.AddRange(tris);
                }
                for(var j = 0; j < refined.Count; j++)
                {
                    Spherize(refined[j], radius);
                }
                Utils.Swap(ref iso.Triangles, ref refined);
            }
            return iso;
        }
    }
}
