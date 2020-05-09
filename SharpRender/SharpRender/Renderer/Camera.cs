using SharpRender.Mathematics;
using System;

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
            Position = new Vector3(0f, 0f, 10f);
            worldUp = new Vector3(0f, 1f, 0f);
            Rotation = new Vector3(0f, 180f, 0f);
            UpdateVectors();
        }

        public Camera(Vector3 position_, Vector3 worldUp_, Vector3 rotation_)
        {
            Position = position_;
            worldUp = worldUp_;
            Rotation = rotation_;
            UpdateVectors();
        }

        public Matrix4x4 GetViewMatrix()
        {
            return Utils.LookAt(Position, Position + front, up);
        }

        public void Reset()
        {
            Position = new Vector3(0f, 0f, 0f);
            front = new Vector3(0f, 0f, -1f);
            worldUp = new Vector3(0f, 1f, 0f);
            Rotation = new Vector3(0f, 180f, 0f);
            UpdateVectors();
        }

        private void UpdateVectors()
        {
            var front_ = new Vector3();
            var pitch = Utils.DegToRad(Rotation.x);
            var yaw = Utils.DegToRad(Rotation.y);
            front_.x = MathF.Sin(yaw) * MathF.Cos(pitch);
            front_.y = MathF.Sin(pitch);
            front_.z = MathF.Cos(yaw) * MathF.Cos(pitch);
            front = front_.normalized;
            right = front.Cross(worldUp).normalized;
            up = right.Cross(front).normalized;
        }

        public Vector3 Position { get; set; }

        private Vector3 _rotation;
        public Vector3 Rotation { get { return _rotation; } set { _rotation = value; UpdateVectors(); } }
        public Vector3 front { get; set; }
        public Vector3 up { get; set; }
        public Vector3 right { get; set; }
        public Vector3 worldUp { get; set; }
    }
}