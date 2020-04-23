using SharpRender.Mathematics;

namespace SharpRender.Render
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

        public void Reset()
        {

        }

        private void UpdateVectors()
        {

        }

        public Vector3 position { get; set; }
        public Vector3 rotation { get; set; }
        public Vector3 front { get; set; }
        public Vector3 up { get; set; }
        public Vector3 right { get; set; }
        public Vector3 worldUp { get; set; }

        public float Near { get; private set; }
        public float Far { get; private set; }
        public float FieldOfView { get; private set; }
        public ECameraProjection Mode { get; private set; }
    }
}