using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Mathematics;

namespace SharpRender.Renderer
{
    enum ECameraProjection
    {
        Perspective,
        Orthographic
    }

    class Camera
    {
        public Matrix4x4 CalculateViewMatrix()
        {
            return default;
        }

        private void UpdateVectors()
        {

        }

        public Vector3 front;
        public Vector3 up;
        public Vector3 right;

        public float Near { get; private set; }
        public float Far { get; private set; }
        public float FieldOfView { get; private set; }
        public ECameraProjection Mode { get; private set; }
    }
}
