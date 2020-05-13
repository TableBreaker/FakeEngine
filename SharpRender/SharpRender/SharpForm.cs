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
            _bm = new Bitmap(800, 450);
            _renderer = new Renderer(Graphics.FromImage(_bm), 800, 450);
            this.Shown += OnShown;
        }

        private void OnShown(object sender, EventArgs e)
        {
            SetScene();
        }

        private void SetScene()
        {
            var shape = new Shapes();
            var obj = shape.GenerateObject(Shapes.Shape.CUBE);
            _mainScene.AddObject(obj);
            _mainScene.SetLightPosition(50, 100, -50);
            _mainScene.SetLightColor(Color.Red);
            obj.SetMaterialColor(Vector4.One);
            obj.SetMaterialParameters(1f, 1f, 1f, 10);
            
            RenderScene();
        }

        private void RenderScene()
        {
            _mainScene.RenderScene(_renderer);
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
        private Bitmap _bm;
        private Bitmap _noTexture;

        private PictureBox _pictureBox;
    }
}
