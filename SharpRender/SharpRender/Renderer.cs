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
			_graphics = g;
		}
		
		// loads a texture to the renderer
		public void AddTexture(Bitmap tex)
		{
			_textures.Add(tex);
		}

		// returns the number of the currently loaded textures
		public int GetTextureNumber()
		{
			return _textures.Count;
		}

		// returns a selected texture
		public Bitmap GetTexture(int iTex)
		{
			if (iTex < 0 || iTex >= _textures.Count)
			{
				throw new ArgumentException("Texture index is out of bounds");
			}
			return _textures[iTex];
		}

		// sets a current texture
		public void SetTextureIndex(int iTex)
		{
			if (iTex < 0 || iTex >= _textures.Count)
			{
				iTex = -1;
			}
			_iTexture = iTex;
		}

		// sets a texture edge sampling mode
		public void SetWrapMode(TextureWrapMode mode)
		{
			_wrapMode = mode;
		}

		// returns the background color
		public Color GetBGColor()
		{
			return _bgColor;
		}

		// returns the wireframe color
		public Color GetWFColor()
		{
			return _wfBrush.Color;
		}

		// returns the wireframe color of the selected object
		public Color GetSelectedColor()
		{
			return _selectedBrush.Color;
		}

		// sets the background color
		public void SetBGColor(Color col)
		{
			_bgColor = col;
		}

		// sets the wireframe color
		public void SetWFColor(Color col)
		{
			_wfBrush.Color = col;
		}

		// sets the wireframe color of the selected object
		public void SetSelectedColor(Color col)
		{
			_selectedBrush.Color = col;
		}

		// switches a current projection mode
		public void SetProjection(bool perspective)
		{
			_perspective = perspective;
		}

		// switches back-face culling
		public void SetFaceCulling(bool cullFace)
		{
			_cullFace = cullFace;
		}

		// returns a (x, y) pixel of the selected texture
		private Vector4 SampleTexture(float x, float y)
		{
			if (_iTexture < 0 || _iTexture > GetTextureNumber())
			{
				// return white color if no texture
				return Vector4.One;
			}

			var tex = _textures[_iTexture];

			int ix = (int)(x * (tex.Width - 1));
			int iy = (int)(y * (tex.Height - 1));
			var mirrorX = MathF.Abs((int)x) % 2 != 0 ^ x <= 0;
			var mirrorY = MathF.Abs((int)y) % 2 != 0 ^ y <= 0;

			switch(_wrapMode)
			{
				case TextureWrapMode.CLAMP_TO_EDGE:
					ix = (int)Utils.Clamp(x, 0f, 1f) * (tex.Width - 1);
					iy = (int)Utils.Clamp(1 - y, 0f, 1f) * (tex.Height - 1);
					break;
				case TextureWrapMode.MIRRORED_REPEAT:
					if (mirrorX) ix = tex.Width - ix;
					if (mirrorY) iy = tex.Height - iy;
					break;
				case TextureWrapMode.REPEAT:
					ix = (tex.Width + (ix % tex.Width)) % tex.Width;
					iy = (tex.Height + (iy % tex.Height)) % tex.Height;
					break;
			}

			return Utils.ColorToVec(tex.GetPixel(ix, iy));
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
			var x = (int)from.x;
			var y = (int)from.y;

			var dx = (int)MathF.Abs(to.x - x);
			var dy = (int)MathF.Abs(to.y - y);
			var sx = Utils.Sign((int)(to.x - x));
			var sy = Utils.Sign((int)(to.y - y));

			// swap the deltas if 2, 3, 6, or 7th octant;
			var isSwap = dy > dx;
			if (isSwap) Utils.Swap(ref dx, ref dy);

			var e = 2 * dy - dx;

			// start drawing
			for (var i = 1; i < dx; i++, e += 2 * dy)
			{
				// calculate z-value in pixel
				var t = (new Vector3(x, y, 0f) - new Vector3(from.x, from.y, 0f)).Magnitude / (new Vector3(to.x, to.y, 0f) - new Vector3(from.x, from.y, 0f)).Magnitude;
				var z = Utils.Lerp(from.z, to.z, t);
				DrawPoint(x, y, z, IsSelectedObject ? _selectedBrush : _wfBrush);

				// determine if need to change the direction
				while (e >= 0)
				{
					if (isSwap)
						x += sx;
					else
						y += sy;

					e -= 2 * dy;
				}

				// increment y or x each step, depending on the octant
				if (isSwap)
					y += sy;
				else
					x += sx;
			}
		}

		// Draw a single pixel with a Z-test.
		private void DrawPoint(int x, int y, float z, SolidBrush b)
		{
			if (x >= 0 && x <_viewportX && y >= 0 && y < _viewportY && z > (_perspective ? 0 : -1f) && z < 1f)
			{
				// z test
				if (z < _zbuffer[x, y])
				{
					_zbuffer[x, y] = z;
					_graphics.FillRectangle(b, x, y, 1, 1);
				}
			}
		}

		// Draws a wireframe of a polygon(triangle).
		private void DrawTriangle(Triangle tri)
		{
			var vert = 3;
			for (var i = 0; i < vert; i++)
			{
				DrawLine(tri.vertices[i], tri.vertices[(i + 1) % vert]);
			}
		}

		// Fills and shades a polygon.
		private void FillTriangle(Triangle tri, Triangle worldTri, Light lightSource, Material material, List<Vector4> gouraudColors)
		{
			// memorize 1 / w (for perspective-correct mapping)
			var ws = new Vector3(1f / tri.vertices[0].w, 1f / tri.vertices[1].w, 1f / tri.vertices[2].w);
			// copy vectors first
			Vector3 first = tri.vertices[0];
			Vector3 second = tri.vertices[1];
			Vector3 third = tri.vertices[2];
			// in world coordinates ( for lighting calculations)
			Vector3 firstW = worldTri.vertices[0];
			Vector3 secondW = worldTri.vertices[1];
			Vector3 thirdW = worldTri.vertices[2];
			// normals (in world coordinates, for lighting calculations)
			Vector3 firstNorm = worldTri.normals[0];
			Vector3 secondNorm = worldTri.normals[1];
			Vector3 thirdNorm = worldTri.normals[2];
			// colors
			var firstColor = tri.colors[0];
			var secondColor = tri.colors[1];
			var thirdColor = tri.colors[2];
			// texture coordinates (set to zero if a texture isn't set)
			Vector3 firstTex = Vector3.Zero, secondTex = Vector3.Zero, thirdTex = Vector3.Zero;
			if (tri.texCoords.Length != 0)
			{
				firstTex = tri.texCoords[0] * ws.x;
				secondTex = tri.texCoords[1] * ws.y;
				thirdTex = tri.texCoords[2] * ws.z;
			}

			// deformed triangles not needed to be rendered
			if ((first.y == second.y && first. y == third.y) || (first.x == second.x && first.x == third.x))
			{
				return;
			}

			// sort the vertices with all the data, third -> second -> first
			if (first.y > second.y)
			{
				Utils.Swap(ref first, ref second);
				Utils.Swap(ref firstW, ref secondW);
				Utils.Swap(ref firstColor, ref secondColor);
				Utils.Swap(ref firstNorm, ref secondNorm);
				Utils.Swap(ref firstTex, ref thirdTex);
				gouraudColors.Swap(0, 1);
				Utils.Swap(ref ws.x, ref ws.y);
			}

			if (first.y > third.y)
			{
				Utils.Swap(ref first, ref third);
				Utils.Swap(ref firstW, ref thirdW);
				Utils.Swap(ref firstColor, ref thirdColor);
				Utils.Swap(ref firstNorm, ref thirdNorm);
				Utils.Swap(ref firstTex, ref thirdTex);
				gouraudColors.Swap(0, 2);
				Utils.Swap(ref ws.x, ref ws.z);
			}

			if (second.y > third.y)
			{
				Utils.Swap(ref second, ref third);
				Utils.Swap(ref secondW, ref thirdW);
				Utils.Swap(ref secondColor, ref thirdColor);
				Utils.Swap(ref secondNorm, ref thirdNorm);
				Utils.Swap(ref secondTex, ref thirdTex);
				gouraudColors.Swap(1, 2);
				Utils.Swap(ref ws.y, ref ws.z);
			}

			// memorize z-values
			var zs = new Vector3(first.z, second.z, third.z);
			// we're working in the 2d space, so we get rid of z
			first.z = second.z = third.z = 0f;
			// discard very narrow triangles
			if (first.EqualEpsilon(second, .5f) || second.EqualEpsilon(third, .5f) || third.EqualEpsilon(first, .5f))
			{
				return;
			}

			// ambient lighting
			var ambient = lightSource.GetAmbientColor() * material.GetAmbientColor();

			var totalHeight = third.y - first.y;
			// scan down to up
			for (var i = 0; i < totalHeight; i++)
			{
				// lines is off the screen, don't draw
				if (first.y + i >= _viewportY) return;
				if (first.y + i < 0) continue;
				var secondHalf = i > second.y - first.y || second.y == first.y;
				var segmentHeight = secondHalf ? third.y - second.y : second.y - first.y;
				// current height to overall height ratio
				var alpha = (float)i / totalHeight;
				// current segment height to overall segment height 
				var beta = (float)(i - (secondHalf ? second.y - first.y : 0)) / segmentHeight;
				// find current intersection point between scaline and first-to-third side
				var A = first + (third - first) * alpha;
				// find current intersection point between scanline and first-tosecond (or second-to-third) side
				var B = secondHalf ? second + (third - second) * beta : first + (second - first) * beta;
				// A should be on the left
				if (A.x > B.x) Utils.Swap(ref A, ref B);
				// the line is off the screen, don't draw
				if (A.x > _viewportX || B.x <= 0) continue;
				if (A.x < 0) A.x = 0;
				if (B.x > _viewportX) B.x = _viewportX - 1;
				// fill the line between A and B
				for (var j = (int)A.x; j <= B.x; j++)
				{
					// find barycentric coordinates for interpolation
					try
					{
						var coordinates = Utils.Barycentric2D(new Vector3(j, first.y + i, 0f), first, second, third);
						// find interpolated z-value
						var z = coordinates.Dot(zs);
						var oneToW = coordinates.Dot(ws);

						// determine a color
						var col = firstColor * coordinates.x + secondColor * coordinates.y + thirdColor * coordinates.z;
						if (_iTexture >= 0 && _iTexture < GetTextureNumber())
						{
							// find a texture pixel
							var texel = (firstTex * coordinates.x / oneToW + secondTex * coordinates.y / oneToW + thirdTex * coordinates.z / oneToW);
							// resulting color = color from vertices * texel
							col = SampleTexture(texel.x, texel.y);
						}
						else
						{
							col = firstColor * coordinates.x + secondColor * coordinates.y + thirdColor * coordinates.z;
						}

						// determine a fragment position (in world space)
						var fragPos = (firstW * coordinates.x + secondW * coordinates.y + thirdW * coordinates.z);
						// determine an interpolated normal
						var fragNormal = new Vector3();
						switch(lightSource.Mode)
						{
							case LightMode.FLAT:
								fragNormal = ((firstNorm + secondNorm + thirdNorm) / 3f).normalized;
								break;
							case LightMode.PHONG:
								// Phong model shading normal interpolation
								fragNormal = (firstNorm * coordinates.x + secondNorm * coordinates.y * thirdNorm * coordinates.z).normalized;
								break;
						}

						if (lightSource.Mode != LightMode.GOURAUD)
						{
							// diffuse lighting
							var lightDir = (lightSource.Position - fragPos).normalized;
							var diff = MathF.Max(fragNormal.Dot(lightDir), 0f);
							var diffuse = lightSource.GetDiffuseColor() * material.GetDiffuseColor() * diff;

							// specular lighting
							var viewDir = (-fragPos).normalized;
							var reflectDir = Utils.Reflect(-lightDir, fragNormal);
							var spec = MathF.Pow(MathF.Max(viewDir.Dot(reflectDir), 0f), material.GetShininess());
							var specular = lightSource.GetSpecularColor() * material.GetSpecularColor() * spec;

							// resulting
							col = Utils.Clamp((ambient + (lightSource.On ? diffuse + specular : Vector4.Zero)) * col, 0f, 1f);
						}
						else
						{
							// using pre-computed vertex color values
							var sumLight = gouraudColors[0] * coordinates.x + gouraudColors[1] * coordinates.y + gouraudColors[2] * coordinates.z;
							col = Utils.Clamp(ambient + (lightSource.On ? sumLight : Vector4.Zero) * col, 0f, 1f);
						}

						_surfaceBrush.Color = Color.FromArgb(255, (int)(col.x * 255), (int)(col.y * 255), (int)(col.z * 255));
						DrawPoint(j, (int)first.y + i, z, _surfaceBrush);
					}
					catch
					{
						// deformed triangle or something else, don't draw
					}
				}
			}
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
