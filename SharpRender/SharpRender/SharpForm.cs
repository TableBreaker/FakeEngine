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
            InitGraphic();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            RenderScene();
        }

        private void InitGraphic()
        {
            _graphic = new Graphic();
            _graphic.Initialize();
        }

        private void RenderScene()
        {
            if (_graphic == null)
                return;

            if (_graphic.RenderToBuffer())
            {

            }
        }

        private Graphic _graphic;
        private Bitmap _texture;
    }
}
