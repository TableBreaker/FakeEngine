using FakeEngine.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FakeEngine.Render
{
    enum LightMode { FLAT, PHONG, GOURAUD };

    class Light
    {
        public static readonly Vector3 DEFAULT_LIGHT_COLOR = new Vector3(1f, 1f, 1f);
        public const float DEFAULT_LIGHT_AMBIENCE = 0.1f;
        public const float DEFAULT_LIGHT_DIFFUSE = 0.8f;
        public const float DEFAULT_LIGHT_SPECULAR = 1f;
        public const LightMode DEFAULT_LIGHT_MODE = LightMode.PHONG;

        public Light() : this(Vector3.Zero)
        {

        }

        public Light(Vector3 pos) 
            : this(pos, DEFAULT_LIGHT_COLOR, DEFAULT_LIGHT_AMBIENCE, DEFAULT_LIGHT_DIFFUSE, DEFAULT_LIGHT_SPECULAR)
        {
            
        }

        public Light(Vector3 pos, Vector3 col, float amb, float diff, float spec)
        {
            Position = pos;
            InitPosition = pos;
            Color = col;
            On = true;
            Mode = DEFAULT_LIGHT_MODE;
            SetAmbient(amb);
            SetDiffuse(diff);
            SetSpecular(spec);
        }
        
        public Light(Light light)
        {
            On = light.On;
            Mode = light.Mode;
            InitPosition = light.InitPosition;
            Position = light.Position;
            Color = light.Color;
            _ambient = light.GetAmbient();
            _diffuse = light.GetDiffuse();
            _specular = light.GetSpecular();
        }

        // returns the ambient light intensity of the light source
        public float GetAmbient()
        {
            return _ambient;
        }

        // returns the diffused light intensity of the light source
        public float GetDiffuse()
        {
            return _diffuse;
        }

        // returns the specular light intensity of the light source
        public float GetSpecular()
        {
            return _specular;
        }

        // returns the color of the ambient light of the light source
        public Vector4 GetAmbientColor()
        {
            return Color * _ambient;
        }

        // returns the color of the diffuse light of the light source
        public Vector4 GetDiffuseColor()
        {
            return Color * _diffuse;
        }

        // returns the color of the specular light of the light source
        public Vector4 GetSpecularColor()
        {
            return Color * _specular;
        }

        // set the ambient light intensity of the light source
        public void SetAmbient(float ambient)
        {
            _ambient = Utils.Clamp(ambient, 0f, 1f);
        }

        // set the diffuse light intensity of the light source
        public void SetDiffuse(float diffuse)
        {
            _diffuse = Utils.Clamp(diffuse, 0f, 1f);
        }

        // set the specular light intensity of the light source
        public void SetSpecular(float specular)
        {
            _specular = Utils.Clamp(specular, 0f, 1f);
        }

        // reset the parameters of the light source to the default ones
        public void Reset()
        {
            On = true;
            Position = InitPosition;
            Mode = DEFAULT_LIGHT_MODE;
            Color = DEFAULT_LIGHT_COLOR;
            _ambient = DEFAULT_LIGHT_AMBIENCE;
            _diffuse = DEFAULT_LIGHT_DIFFUSE;
            _specular = DEFAULT_LIGHT_SPECULAR;
        }

        public Vector3 InitPosition { get; set; }
        public Vector3 Position { get; set; }
        public Vector4 Color { get; set; }
        public bool On { get; set; }
        public LightMode Mode { get; set; }

        private float _ambient;
        private float _diffuse;
        private float _specular;
    }
}
