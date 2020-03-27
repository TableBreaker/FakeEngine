using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Render;
using SharpRender.Mathematics;

namespace SharpRender
{
    class Graphic
    {
		public Graphic(int width, int height)
		{

		}

		public bool Render()
		{
			return scene.RenderScene(renderer);
		}

		private Scene scene;
		private Renderer renderer;
    }
}
