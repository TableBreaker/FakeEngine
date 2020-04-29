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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Render(e.Graphics);
        }

        private void InitGraphic(int width, int height)
        {
        }

        private void Render(Graphics graphics)
        {
        }
    }
}
