using SharpRender.Render;
using SharpRender.Mathematics;
using System.Drawing;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

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
			_graphics.Clear(_bgColor);
		}

		// clears a depth buffer
		public void ClearZBuffer()
		{
			for (var x = 0; x < _zbuffer.GetLength(0); x++)
				for (var y = 0; y < _zbuffer.GetLength(1); y++)
				{
					_zbuffer[x, y] = float.PositiveInfinity;
				}
		}

		// *********************************************************************************************************
		// transforms renders and shades a object
		public void RenderObject(RenderObject obj, Matrix4x4 model, Matrix4x4 view, Matrix4x4 proj, Light light, bool wireframe, bool solid)
		{
			var modelView = view * model;
			Matrix3x3 normalTransform = modelView.Inverted().Transposed();
			foreach (var tri in obj.Triangles)
			{
				// get the triangle transformed (world and camera coords)
				var cameraTransformed = tri.GetTransformed(modelView, normalTransform);
				// check visibility
				if (_cullFace && !IsVisible(cameraTransformed))
					continue;

				// get the triangle transformed (clip space) if ortho, dont forget to apply the view matrix
				var transformed = cameraTransformed.GetTransformed(_perspective ? proj : proj * view, new Matrix3x3());
				ViewportTransform(transformed);
				if (!ToClip(transformed))
				{
					if (wireframe)
					{
						DrawTriangle(transformed);
					}
					if (solid)
					{
						// another one for lighting calculations
						var worldTransformed = tri.GetTransformed(model, normalTransform);
						var gouraudColors = new List<Vector4> { Vector4.Zero, Vector4.Zero, Vector4.Zero, Vector4.Zero };
						if (light.Mode == LightMode.GOURAUD)
						{
							// doing gouraud lighting calculation here in a 'vertex shader'
							gouraudColors = GetGouraudColors(worldTransformed, light, obj.Material);
						}
						// essentially, entering fragment shader
						FillTriangle(transformed, worldTransformed, light, obj.Material, gouraudColors);
					}
				}
			}
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

		// returns a (x, y) pixel of the selected texture
		private Vector4 SampleTexture(float x, float y)
		{
			return default;
		}

		// True, if a polygon is entirely off the screen.
		private bool ToClip(Triangle tri)
		{
			if (Utils.Area(tri) < .01)
				return true;
			
			foreach (var vert in tri.vertices)
			{
				// draw a triangle if at least one vertex is inside ?
				if (vert.x >= 0 && vert.x < _viewportX && vert.y >= 0 && vert.y < _viewportY)
				{
					return false;
				}
				if (vert.z < 1 && vert.z > 0)
				{
					return false;
				}
			}

			return true;
		}

		// True, if a polygon is back-faced.
		private bool IsVisible(Triangle tri)
		{
			// a face is visible is a dot-product of its normal vector
			// and the first vertex of the triangle is less than zero 
			var norm = Utils.Normal(tri.vertices[0], tri.vertices[1], tri.vertices[2]);
			Vector3 v0 = tri.vertices[0];
			var camPos = _perspective ? Vector3.Zero : new Vector3(v0.x, v0.y, 1f);
			var visible = (camPos - v0).Dot(norm) > .5;
			return visible;
		}

		// Calculates the colors in the vertices of a polygon using the Gouraud shading.  
		// about Gouraud shading: https://en.wikipedia.org/wiki/Gouraud_shading  vertex light?
		private List<Vector4> GetGouraudColors(Triangle tri, Light lightSource, Material material)
		{
			// gouraud model lighting calculations:
			var colors = new List<Vector4>();
			for (var i = 0; i < 3; i++)
			{
				// diffuse
				var lightDir = (lightSource.Position - (Vector3)tri.vertices[i]).normalized;
				var diff = MathF.Max(tri.normals[i].Dot(lightDir), 0f);
				var diffuse = lightSource.GetDiffuseColor() * material.GetDiffuseColor() * diff;

				// specular
				var viewDir = (-(Vector3)tri.vertices[i]).normalized;
				var reflectDir = Utils.Reflect(-lightDir, tri.normals[i]);
				var spec = MathF.Pow(MathF.Max(viewDir.Dot(reflectDir), 0f), material.GetShininess());
				var specular = lightSource.GetSpecularColor() * material.GetSpecularColor() * spec;

				colors.Add(diffuse + specular);
			}

			return colors;
		}

		#region Draw Functions

		// Draws a line using the Bresenham's algorithm.
		// about Bresenham's algorithm: https://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm
		private void DrawLine(Vector3 from, Vector3 to)
		{

		}

		// Draw a single pixel with a Z-test.
		private void DrawPoint(int x, int y, float z, SolidBrush b)
		{

		}

		// Draws a wireframe of a polygon(triangle).
		private void DrawTriangle(Triangle tri)
		{

		}

		// Fills and shades a polygon.
		private void FillTriangle(Triangle tri, Triangle worldTri, Light lightSource, Material material, List<Vector4> gouraudColors)
		{

		}

		// Translate from homonegenous coordinates of the vector (w-division) and map to the viewport
		private void ViewportTransform(Triangle tri)
		{
			tri.vertices[0] = NDCtoViewport(tri.vertices[0].fromHomogeneous4);
			tri.vertices[1] = NDCtoViewport(tri.vertices[1].fromHomogeneous4);
			tri.vertices[2] = NDCtoViewport(tri.vertices[2].fromHomogeneous4);
		}

		// Remaps coordinates from [-1, 1] to the [0, viewportX(Y)] space.
		private Vector4 NDCtoViewport(Vector4 vertex)
		{
			return new Vector4((int)((1f + vertex.x) * _viewportX / 2f), (int)((1f - vertex.y) * _viewportY / 2f), vertex.z, vertex.w);
		}

        #endregion

        // if true, the renderer uses the brush for selected object, otherwise the wireframe brush
        public bool IsSelectedObject;

		// The graphics objects to render on.
		private Graphics _graphics;
		// The background color.
		private Color _bgColor;
		// The brush for wireframe drawing.
		private SolidBrush _wfBrush;
		// The brush for wireframe drawing of the currently selected object.
		private SolidBrush _selectedBrush;
		// The brush for the surface drawing.
		private SolidBrush _surfaceBrush;
		// True, if a perspective projection is set, otherwise orthographics.
		private bool _perspective;
		// True, is back-face culling is set.
		private bool _cullFace;
		// The width of the viewport (screen).
		private int _viewportX;
		// The height of the viewport (screen).
		private int _viewportY;
		// Depth buffer (width X height).
		private float[,] _zbuffer;
		// Loaded textures.
		private List<Bitmap> _textures;
		// Selected texture.
		private int _iTexture;
		// Specifies the edge sampling behavior.
		private TextureWrapMode _wrapMode;

    }
}
