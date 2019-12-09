using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Math;

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

        public Vector3 front;
        public float near;
        public float far;
        public float fieldOfView;
    }
}
