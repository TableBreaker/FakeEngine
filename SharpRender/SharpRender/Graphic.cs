using System;
using System.Collections.Generic;
using System.Text;
using SharpRender.Renderer;

namespace SharpRender
{
    class Graphic
    {
        public bool Initialize()
        {

            return true;
        }

        public bool RenderToBuffer()
        {

            return true;
        }

        public RenderBuffer GetBuffer()
        {
            return _buffer;
        }

        private RenderBuffer _buffer;
    }
}
