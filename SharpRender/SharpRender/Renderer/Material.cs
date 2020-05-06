using SharpRender.Mathematics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SharpRender.Render
{
    class Material
    {
        const float DEFAULT_MATERIAL_AMBIENCE = 1f;
        const float DEFAULT_MATERIAL_DIFFUSE = 0.9f;
        const float DEFAULT_MATERIAL_SPECULAR = 0.5f;
        const int DEFAULT_MATERIAL_SHININESS = 16;
        static readonly Vector4 DEFAULT_MATERIAL_COLOR = new Vector4(1f, 1f, 1f, 1f);

        public Material()
            : this(DEFAULT_MATERIAL_AMBIENCE, DEFAULT_MATERIAL_DIFFUSE, DEFAULT_MATERIAL_SPECULAR, DEFAULT_MATERIAL_SHININESS)
        { }

        public Material(float ambient_, float diffuse_, float specular_, int shine_)
            : this(ambient_, diffuse_, specular_, shine_, DEFAULT_MATERIAL_COLOR)
        { }

        public Material(float ambient_, float diffuse_, float specular_, int shine_, Vector4 color_)
        {
            _ambient = ambient_;
            _diffuse = diffuse_;
            _specular = specular_;
            _shininess = shine_;
            _color = color_;
        }

        public Material(Material mat)
        {
            _color = mat._color;
            _ambient = mat._ambient;
            _diffuse = mat._diffuse;
            _specular = mat._specular;
            _shininess = mat._shininess;
        }

        public float GetAmbient()
        {
            return _ambient;
        }

        public float GetDiffuse()
        {
            return _diffuse;
        }

        public float GetSpecular()
        {
            return _specular;
        }

        public Vector4 GetAmbientColor()
        {
            return _color * _ambient;
        }

        public Vector4 GetDiffuseColor()
        {
            return _color * _diffuse;
        }

        public Vector4 GetSpecularColor()
        {
            return _color * _specular;
        }

        public int GetShininess()
        {
            return _shininess;
        }

        public Vector4 GetColor()
        {
            return _color;
        }

        public void SetAmbient(float ambient)
        {
            _ambient = Utils.Clamp(ambient, 0f, 1f);
        }

        public void SetDiffuse(float diffuse)
        {
            _diffuse = Utils.Clamp(diffuse, 0f, 1f);
        }

        public void SetSpecular(float specular)
        {
            _specular = Utils.Clamp(specular, 0f, 1f);
        }

        public void SetShininess(int shininess)
        {
            _shininess = (int)Utils.Clamp(shininess, 2f, 256f);
        }

        public void SetColor(Vector4 color)
        {
            _color = Utils.Clamp(color, 0f, 255f);
        }

        public void Reset()
        {
            _color = DEFAULT_MATERIAL_COLOR;
            _ambient = DEFAULT_MATERIAL_AMBIENCE;
            _diffuse = DEFAULT_MATERIAL_DIFFUSE;
            _specular = DEFAULT_MATERIAL_SPECULAR;
            _shininess = DEFAULT_MATERIAL_SHININESS;
        }

        private Vector4 _color;
        private float _ambient;
        private float _diffuse;
        private float _specular;
        private int _shininess;
    }
}
