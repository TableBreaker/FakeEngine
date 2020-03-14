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
            _graphic = new Graphic(width, height);
        }

        private void Render(Graphics graphics)
        {
            if (_graphic == null)
                return;

            if (_graphic.RenderToBuffer())
            {
                var buffer = _graphic.GetBuffer();
                for (var y = 0; y < buffer.height; y++)
                    for (var x = 0; x < buffer.width; x++)
                    {
                        //var color = buffer.GetColor(x, y);
                        //_brush.Color = Color.FromArgb((int)(color.x * 255), (int)(color.y * 255), (int)(color.z * 255));
                        graphics.FillRectangle(_brush, x, y, 1, 1);
                    }
            }
        }

        private SolidBrush _brush;
        private Graphic _graphic;
        private Bitmap _texture;
    }
}
