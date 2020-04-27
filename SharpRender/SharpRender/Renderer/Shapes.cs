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

        public Object generateObject(Shape objShape, int precision = DEFAULT_PRECISION_LEVEL)
        {
            return null;
        }

        // returns a cube
        private Object Cube()
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

            return new Object(vertices, colors, texCoords, indices);
        }

        private Object Pyramid()
        {
            return null;
        }

        private Object Octahedron()
        {
            return null;
        }

        private Object Tetrahedron()
        {
            return null;
        }


    }
}
