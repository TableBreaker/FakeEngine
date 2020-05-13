using SharpRender.Mathematics;
using SharpRender.Render;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpRender
{
    public partial class SharpForm : Form
    {
        public SharpForm()
        {
            InitializeComponent();
            _mainScene = new Scene();
            _bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            _pictureBox.Image = _bitmap;
            _renderer = new Renderer(Graphics.FromImage(_bitmap), 800, 450);
            InitScene();
            SetScene();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Console.WriteLine("move");
            base.OnMouseMove(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            Vector3 translate = Vector3.Zero;
            Vector3 rotate = Vector3.Zero;
            if (e.KeyChar == 'w')
            {
                translate.z -= 10f;
            }
            if (e.KeyChar == 's')
            {
                translate.z += 10f;
            }
            if (e.KeyChar == 'a')
            {
                translate.x -= 10f;
            }
            if (e.KeyChar == 'd')
            {
                translate.x += 10f;
            }
            if (e.KeyChar == 'q')
            {
                translate.y -= 10f;
            }
            if (e.KeyChar == 'e')
            {
                translate.y += 10f;
            }

            if (e.KeyChar == 'u')
            {
                rotate.x += 10f;
            }
            if (e.KeyChar == 'i')
            {
                rotate.y += 10f;
            }
            if (e.KeyChar == 'o')
            {
                rotate.z += 10f;
            }

            MoveObject(translate);
            RotateObject(rotate);
        }

        private void MoveObject(Vector3 translate)
        {
            var pos = _mainScene.GetObjectPosition(false);
            pos += translate;
            _mainScene.SetObjectPosition((int)pos.x, (int)pos.y, (int)pos.z);
            SetScene();
        }

        private void RotateObject(Vector3 rotate)
        {
            var rot = _mainScene.GetObjectRotation(false);
            rot += rotate;
            _mainScene.SetObjectRotation((int)rot.x, (int)rot.y, (int)rot.z);
        }

        private void InitScene()
        {
            var shape = new Shapes();
            var obj = shape.GenerateObject(Shapes.Shape.CUBE);
            _mainScene.AddObject(obj);
            _mainScene.SetLightPosition(50, 100, -50);
            _mainScene.SetLightColor(Color.White);
            obj.SetMaterialColor(Vector4.One);
            obj.SetMaterialParameters(1f, 1f, 1f, 10);
        }

        private void SetScene()
        {
            RenderScene();
        }

        private void RenderScene()
        {
            var gr = Graphics.FromImage(_bitmap);
            _renderer.SetGraphics(gr);
            _mainScene.RenderScene(_renderer);
            _pictureBox.Refresh();
        }

        //private void UpdateObjectsParams()
        //{
        //    if (!_mainScene.IsEmpty())
        //    {
        //        var pos = _mainScene.GetObjectPosition(false);
        //    }
        //}

        //private void UpdateMaterialParams()
        //{

        //}

        //private void UpdateCameraParams()
        //{

        //}

        //private void UpdateOtherParams()
        //{

        //}

        //private void UpdateLightParams()
        //{

        //}

        private Scene _mainScene;
        private Renderer _renderer;
        private PictureBox _pictureBox;
        private Bitmap _bitmap;
    }
}
