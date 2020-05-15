using FakeEngine.Mathematics;
using FakeEngine.Render;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FakeEngine
{
    public partial class FakeForm : Form
    {
        public FakeForm()
        {
            InitializeComponent();
            _mainScene = new Scene();
            _bitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            _pictureBox.Image = _bitmap;
            _renderer = new Renderer(Graphics.FromImage(_bitmap), 800, 450);
            InitScene();
            SetScene();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            Vector3 translate = Vector3.Zero;
            Vector3 rotate = Vector3.Zero;

            if (e.KeyCode == Keys.W)
            {
                translate.z -= 10f;
            }
            if (e.KeyCode == Keys.S)
            {
                translate.z += 10f;
            }
            if (e.KeyCode == Keys.A)
            {
                translate.x -= 10f;
            }
            if (e.KeyCode == Keys.D)
            {
                translate.x += 10f;
            }
            if (e.KeyCode == Keys.Q)
            {
                translate.y -= 10f;
            }
            if (e.KeyCode == Keys.E)
            {
                translate.y += 10f;
            }

            if (e.KeyCode == Keys.U)
            {
                rotate.x += 10f;
            }
            if (e.KeyCode == Keys.I)
            {
                rotate.y += 10f;
            }
            if (e.KeyCode == Keys.O)
            {
                rotate.z += 10f;
            }

            if (e.KeyCode == Keys.R)
            {
                ResetObject();
            }

            if (e.KeyCode == Keys.N)
            {
                _mainScene.SetDrawingMode(true, false);
            }

            if (e.KeyCode == Keys.M)
            {
                _mainScene.SetDrawingMode(false, true);
            }

            if (e.KeyCode == Keys.B)
            {
                _mainScene.SetDrawingMode(true, true);
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

        private void ResetObject()
        {
            _mainScene.SetObjectPosition(0, 0, 0);
            _mainScene.SetObjectRotation(0, 0, 0);
        }

        private void InitScene()
        {
            var shape = new Shapes();
            var obj = shape.GenerateObject(Shapes.Shape.CUBE);
            _mainScene.AddObject(obj);
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
