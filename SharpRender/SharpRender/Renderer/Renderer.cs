using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Mathematics;

namespace SharpRender.Renderer
{
    class Renderer
    {
        #region Drawer
        // Draws a line using the Bresenham's algorithm with Z-testing.
        private void DrawLine(Vector3 from, Vector3 to)
        {
			int x = (int)from.x, y = (int)from.y;

			int dx = (int)MathF.Abs(to.x - x);
			int dy = (int)MathF.Abs(to.y - y);
			int sx = MathF.Sign(to.x - x);
			int sy = MathF.Sign(to.y - y);

			// swap the deltas if 2, 3, 6 or 7th octant
			bool isSwap = dy > dx;
			if (isSwap)
				MathUtility.Swap(ref dx, ref dy);

			int e = 2 * dy - dx;

			// start drawing
			for (int i = 1; i <= dx; i++, e += 2 * dy)
			{
				// calculate z-value in pixel
				float t = (new Vector3(x, y, 0f) - new Vector3(from.x, from.y, 0f)).Magnitude / (new Vector3(to.x, to.y, 0f) - new Vector3(from.x, from.y, 0f)).Magnitude;
				float z = (1 - t) * from.z + t * to.z;
				DrawPoint(x, y, z, isSelectedObject ? selectedBrush : wfBrush);

				// determine if need to change the direction
				while (e >= 0)
				{
					if (isSwap) 
					{
						x += sx; 
					}
					else 
					{ 
						y += sy; 
					}
					e -= 2 * dx;
				}

				// increment y or x each step, depending on the octant
				if (isSwap) 
				{
					y += sy; 
				}
				else 
				{ 
					x += sx; 
				}
			}
		}

		public void DrawPoint(int x, int y, float z, Vector4 color)
		{
			if (x >= 0 && x < viewportX && y >= 0 && y < viewportY && z > (perspective ? 0 : -1) && z < 1)
			{
				// z test
				if (z < zbuffer[x, y])
				{
					zbuffer[x, y] = z;
					graphics->FillRectangle(br, x, y, 1, 1);
				}
			}
		}
		#endregion


		private Camera mainCamera;
		private RenderBuffer buffer;
	}
}
