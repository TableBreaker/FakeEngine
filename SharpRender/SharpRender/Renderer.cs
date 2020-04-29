using SharpRender.Render;
using SharpRender.Mathematics;
using System.Drawing;

namespace SharpRender
{
	enum TextureWrapMode { REPEAT, MIRRORED_REPEAT, CLAMP_TO_EDGE };

	class Renderer
	{
		public static readonly Color DEFAULT_BG_COLOR = Color.Black;
		public static readonly Color DEFAULT_WF_COLOR = Color.White;
		public static readonly Color DEFAULT_SELECTED_COLOR = Color.Blue;

		public const TextureWrapMode DEFAULT_WRAP_MODE = TextureWrapMode.REPEAT;

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


		// returns a width / height ratio
		public float GetViewportAspect()
		{
			return _viewportX / (float)_viewportY;
		}

		// clears a screen with a current background color
		public void ClearScreen()
		{

		}

		// clears a depth buffer
		public void ClearZBuffer()
		{

		}

		// transforms renders and shades a object
		public void RenderObject(RenderObject obj, Matrix4x4 model, Matrix4x4 view, Matrix4x4 proj, Light light, bool wireframe, bool solid)
		{
			
		}

		public void SetGraphics(Graphics g)
		{

		}
		
		// loads a texture to the renderer
		public void AddTexture(Bitmap tex)
		{

		}

		// returns the number of the currently loaded textures
		public ulong GetTextureNumber()
		{
			return default;
		}

		// returns a selected texture
		public Bitmap GetTexture(int iTex)
		{
			return default;
		}

		// sets a current texture
		public void SetTextureIndex(int iTex)
		{

		}

		// sets a texture edge sampling mode
		public void SetWrapMode(TextureWrapMode mode)
		{

		}

		// returns the background color
		public Color GetBGColor()
		{
			return default;
		}

		// returns the wireframe color
		public Color GetWFColor()
		{
			return default;
		}

		// returns the wireframe color of the selected object
		public Color GetSelectedColor()
		{
			return default;
		}

		// sets the background color
		public void SetBGColor(Color col)
		{

		}

		// sets the wireframe color
		public void SetWFColor(Color col)
		{

		}

		// sets the wireframe color of the selected object
		public void SetSelectedColor(Color col)
		{

		}

		// switches a current projection mode
		public void SetProjection(bool perspective)
		{

		}

		// switches back-face culling
		public void SetFaceCulling(bool cullFace)
		{

		}

		// if true, the renderer uses the brush for selected object, otherwise the wireframe brush
		public bool IsSelectedObject;

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
