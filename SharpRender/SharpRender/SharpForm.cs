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
            InitGraphic(ClientSize.Width, ClientSize.Height);
            _brush = new SolidBrush(Color.White);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Render(e.Graphics);
        }

        private void InitGraphic(int width, int height)
        {
            _graphic = new Renderer(width, height);
        }

        private void Render(Graphics graphics)
        {
            if (_graphic == null)
                return;

            if (_graphic.Render())
            {
                
            }
        }

        private SolidBrush _brush;
        private Renderer _graphic;
        private Bitmap _texture;
    }
}
