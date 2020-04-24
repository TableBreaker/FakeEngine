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
        public Camera()
        {
            position = new Vector3(0f, 0f, 10f);
            worldUp = new Vector3(0f, 1f, 0f);
            rotation = new Vector3(0f, 180f, 0f);
            UpdateVectors();
        }

        public Camera(Vector3 position_, Vector3 worldUp_, Vector3 rotation_)
        {
            position = position_;
            worldUp = worldUp_;
            rotation = rotation_;
            UpdateVectors();
        }

        public Matrix4x4 GetViewMatrix()
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
    }
}