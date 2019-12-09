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
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap myBitmap = new Bitmap(700, 700);
            for (var x = 0; x < 100; x++)
                for (var y = 0; y < 100; y++)
                {
                    myBitmap.SetPixel(x, y, Color.Red);
                }
            e.Graphics.DrawImage(myBitmap, 0, 0);
        }
    }
}
