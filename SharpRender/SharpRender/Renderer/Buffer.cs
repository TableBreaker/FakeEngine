using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Mathematics;

namespace SharpRender.Renderer
{
    class RenderBuffer
    {
        public RenderBuffer(int width_, int height_)
        {
            id = ++_counter;
            width = width_;
            height = height_;

            _colorBuffer = new Vector4[width, height];
            _depthBuffer = new float[width, height];
            _stencilBuffer = new float[width, height];
        }

        public void SetColor(int x, int y, Vector3 v)
        {
            if (CoordInScreen(x, y))
            {
                _colorBuffer[x, y] = v;
            }
        }

        public Vector3 GetColor(int x, int y)
        {
            if (CoordInScreen(x, y))
            {
                return _colorBuffer[x, y];
            }
            return default;
        }

        public void SetDepth(int x, int y, float v)
        {
            if (CoordInScreen(x, y))
            {
                _depthBuffer[x, y] = v;
            }
        }

        public float GetDepth(int x, int y)
        {
            if (CoordInScreen(x, y))
            {
                return _depthBuffer[x, y];
            }
            return default;
        }

        public void SetStencil(int x, int y, float v)
        {
            if (CoordInScreen(x, y))
            {
                _stencilBuffer[x, y] = v;
            }
        }

        public float GetStencil(int x, int y)
        {
            if (CoordInScreen(x, y))
            {
                return _stencilBuffer[x, y];
            }
            return default;
        }

        private bool CoordInScreen(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }

        private static int _counter = 0;

        public int id { get; private set; }
        public int width { get; private set; }
        public int height { get; private set; }

        private readonly Vector4[,] _colorBuffer;
        private readonly float[,] _depthBuffer;
        private readonly float[,] _stencilBuffer;
    }
}
