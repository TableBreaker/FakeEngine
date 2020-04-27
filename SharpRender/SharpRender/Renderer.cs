using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Render;
using SharpRender.Mathematics;
using System.Drawing;

namespace SharpRender
{
	class Renderer
	{
		public static readonly Color DEFAULT_BG_COLOR = Color.Black;
		public static readonly Color DEFAULT_WF_COLOR = Color.White;
		public static readonly Color DEFAULT_SELECTED_COLOR = Color.Blue;

		public Renderer(Graphics graphics, int viewportWidth, int viewportHeight)
		{
			// initialize colors
			_graphics = graphics;
			_bgColor = DEFAULT_BG_COLOR;
			_wfBrush = new SolidBrush(DEFAULT_WF_COLOR);
			_selectedBrush = new SolidBrush(DEFAULT_SELECTED_COLOR);
			_surfaceBrush = new SolidBrush(Color.Black);

			// initialize z-buffer
			SetViewPort(viewportWidth, viewportHeight);
		}

		public void SetViewPort(int width, int height)
		{
			_viewportX = width;
			_viewportY = height;
			_zbuffer = new float[_viewportX, _viewportY];
		}

		public float GetViewportAspect()
		{
			return _viewportX / (float)_viewportY;
		}

		public void ClearScreen()
		{

		}

		public void ClearZBuffer()
		{

		}

		public void RenderObject(Object obj, Matrix4x4 model, Matrix4x4 view, Matrix4x4 proj,)

		private Graphics _graphics;
		private Color _bgColor;
		private SolidBrush _wfBrush;
		private SolidBrush _selectedBrush;
		private SolidBrush _surfaceBrush;
		private bool _perspective;
		private bool _cullFace;
		private int _viewportX;
		private int _viewportY;
		private float[,] _zbuffer;
    }
}
