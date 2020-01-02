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

        private float near;
        private float far;
        private float fieldOfView;
        private int viewportX;
        private int viewportY;
        private bool perspective;
    }
}
