using SharpRender.Render;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpRender
{
    public partial class SharpForm : Form
    {
        public SharpForm()
        {
            InitializeComponent();
            _mainScene = new Scene();
            _renderer = new Renderer(Graphics.FromImage(null), 800, 450);
        }

        private void SetScene()
        {

        }

        private void UpdateObjectsParams()
        {
            if (!_mainScene.IsEmpty())
            {
                var pos = _mainScene.GetObjectPosition(false);
            }
        }

        private Scene _mainScene;
        private Renderer _renderer;
        private Bitmap _bm;
        private Bitmap _noTexture;

        private PictureBox _pictureBox;
    }
}
