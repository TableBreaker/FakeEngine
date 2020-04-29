using Microsoft.VisualBasic.CompilerServices;
using SharpRender.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Render
{
    enum LightMode { FLAT, PHONG, GOURAUD };

    class Light
    {
        public static readonly Vector3 DEFAULT_LIGHT_COLOR = new Vector3(1f, 1f, 1f);
        public const float DEFAULT_LIGHT_AMBIENCE = 0.1f;
        public const float DEFAULT_LIGHT_DIFFUSE = 0.8f;
        public const float DEFAULT_LIGHT_SPECULAR = 1f;
        public const LightMode DEFAULT_LIGHT_MODE = LightMode.PHONG;

        public Light()
        {

        }

        public Light(Vector3 pos, Vector3 col, float amb, float dif, float spec)
        {

        }

        public Light(Vector3 position)
        {

        }
        
        public Light(Light light)
        {

        }


    }
}
