using System;
using System.Collections.Generic;
using System.Drawing;
using SharpRender.Mathematics;

namespace SharpRender.Render
{
    class Scene
    {
        public const float POSITION_MULTIPLIER = .1f;
        public const float ROTATION_MULTIPLIER = 1f;
        public const float SCALE_MULTIPLIER = .1f;
        public const float CAMERA_POSITION_MULTIPLIER = .1f;
        public const float CAMERA_ROTATION_MULTIPLIER = 1f;
        public const float LIGHT_POSITION_MULTIPLIER = .1f;
        public const float LIGHT_AMBIENT_MULTIPLIER = .01f;
        public const float LIGHT_DIFFUSE_MULTIPLIER = .01f;
        public const float LIGHT_SPECULAR_MULTIPLIER = .01f;
        public const float MATERIAL_AMBIENT_MULTIPLIER = .01f;
        public const float MATERIAL_DIFFUSE_MULTIPLIER = .01f;
        public const float MATERIAL_SPECULAR_MULTIPLIER = .01f;

        public Scene()
        {
            _camera = new Camera();
            _selectedObject = 0;
            _lightSource = new Light(_camera.Position);
            _faceCull = true;
            _wrapMode = Renderer.DEFAULT_WRAP_MODE;
            _perspective = true;
            _drawWireFrame = false;
            _drawSolid = true;
        }

        public void RenderScene(Renderer renderer)
        {
            renderer.ClearScreen();
            renderer.ClearZBuffer();
            renderer.SetFaceCulling(_faceCull);
            renderer.SetWrapMode(_wrapMode);

            // calculate matrices
            var view = _camera.GetViewMatrix();
            Matrix4x4 projection;
            if (isPerspective())
            {
                projection = Utils.Perspective(60f, renderer.GetViewportAspect(), .1f, 100f);
                renderer.SetProjection(true);
            }
            else
            {
                projection = Utils.Orthographic(5, 5, .1f, 100f);
                renderer.SetProjection(false);
            }

            if (!IsEmpty())
            {
                foreach (var obj in _sceneObjects)
                {
                    // get transformation matrix
                    var model = obj.GetModelMatrix();
                    // notify the renderer if the current object is selected
                    renderer.IsSelectedObject = (_sceneObjects[_selectedObject] == obj);
                    renderer.SetTextureIndex(obj.GetTextureIndex());
                    // pass the current object and transformation matrices in the renderer;
                    renderer.RenderObject(obj, model, view, projection, _lightSource, _drawWireFrame, _drawSolid);
                    renderer.IsSelectedObject = false;
                }
            }
        }

        #region methods to manipulate objects;

        public void SetObjectPosition(int x_coord, int y_coord, int z_coord)
        {
            _sceneObjects[_selectedObject].Position = new Vector3(x_coord, y_coord, z_coord) * POSITION_MULTIPLIER;
        }

        public void SetObjectRotation(float x_angle, float y_angle, float z_angle)
        {
            _sceneObjects[_selectedObject].Rotation = new Vector3(x_angle, y_angle, z_angle) * ROTATION_MULTIPLIER;
        }

        public void SetObjectReflection(bool xy, bool xz, bool yz)
        {
            _sceneObjects[_selectedObject].SetReflection(xy, xz, yz);
        }

        public void SetObjectScale(int x_scale, int y_scale, int z_scale)
        {
            _sceneObjects[_selectedObject].Scale = new Vector3(x_scale, y_scale, z_scale) * SCALE_MULTIPLIER;
        }

        public Vector3 GetObjectPosition(bool worldCoords)
        {
            return _sceneObjects[_selectedObject].Position / (worldCoords ? 1f : POSITION_MULTIPLIER);
        }

        public Vector3 GetObjectRotation(bool worldCoords)
        {
            return _sceneObjects[_selectedObject].Rotation / (worldCoords ? 1f : ROTATION_MULTIPLIER);
        }

        public Vector3 GetObjectReflection()
        {
            return _sceneObjects[_selectedObject].GetReflection();
        }
        
        public Vector3 GetObjectScale(bool worldCoords)
        {
            return _sceneObjects[_selectedObject].Scale / (worldCoords ? 1f : SCALE_MULTIPLIER);
        }

        public void ResetObject()
        {
            if (!IsEmpty())
            {
                _sceneObjects[_selectedObject].Reset();
            }
        }

        public void AddObject(RenderObject obj)
        {
            _sceneObjects.Add(obj);
            // select the added object
            _selectedObject = _sceneObjects.Count - 1;
        }

        public void DeleteObject()
        {
            if (!IsEmpty())
            {
                _sceneObjects.RemoveAt(_selectedObject);
                if (_selectedObject > 0)
                {
                    _selectedObject--;
                }
            }
        }

        public void SelectNextObject()
        {
            if (!IsEmpty() && _selectedObject < _sceneObjects.Count - 1)
            {
                _selectedObject++;
            }
        }

        public void SelectPreviousObject()
        {
            if (!IsEmpty() && _selectedObject > 0)
            {
                _selectedObject--;
            }
        }

        #endregion

        #region methods to manupulate camera
        
        public void SetCameraPosition(int x_coord, int y_coord, int z_coord)
        {
            _camera.Position = new Vector3(x_coord, y_coord, z_coord) * CAMERA_POSITION_MULTIPLIER;
        }

        public void SetCameraRotation(float pitch, float yaw, float roll)
        {
            _camera.Rotation = new Vector3(pitch, yaw, roll) * CAMERA_ROTATION_MULTIPLIER;
        }

        public Vector3 GetCameraPosition(bool worldCoords)
        {
            return _camera.Position / (worldCoords ? 1f : CAMERA_POSITION_MULTIPLIER);
        }

        public Vector3 GetCameraRotation(bool worldCoords)
        {
            return _camera.Rotation / (worldCoords ? 1f : CAMERA_ROTATION_MULTIPLIER);
        }

        public void ResetCamera()
        {
            _camera.Reset();
        }

        #endregion

        #region methods to manipulate lighting

        public Vector3 GetLightPosition(bool worldCoords)
        {
            return _lightSource.Position / (worldCoords ? 1f : LIGHT_POSITION_MULTIPLIER);
        }

        public void SetLightPosition(int x_coord, int y_coord, int z_coord)
        {
            _lightSource.Position = new Vector3(x_coord, y_coord, z_coord) * LIGHT_POSITION_MULTIPLIER;
        }

        public void SetLightColor(Color lightColor)
        {
            _lightSource.Color = Utils.ColorToVec(lightColor);
        }

        public void SetLightParams(float ambient, float diffuse, float specular)
        {
            _lightSource.SetAmbient(ambient * LIGHT_AMBIENT_MULTIPLIER);
            _lightSource.SetDiffuse(diffuse * LIGHT_DIFFUSE_MULTIPLIER);
            _lightSource.SetSpecular(specular * LIGHT_SPECULAR_MULTIPLIER);
        }

        public Vector3 GetLightParams(bool worldParams)
        {
            var amb = _lightSource.GetAmbient() / (worldParams ? 1f : LIGHT_AMBIENT_MULTIPLIER);
            var diff = _lightSource.GetDiffuse() / (worldParams ? 1f : LIGHT_DIFFUSE_MULTIPLIER);
            var spec = _lightSource.GetSpecular() / (worldParams ? 1f : LIGHT_SPECULAR_MULTIPLIER);
            return new Vector3(amb, diff, spec);
        }

        public void SetLightOn(bool isOn)
        {
            _lightSource.On = isOn;
        }

        public void SetLightMode(LightMode mode)
        {
            _lightSource.Mode = mode;
        }

        public Color GetLightColor()
        {
            return Utils.VecToColor(_lightSource.Color);
        }

        public bool IsLightOn()
        {
            return _lightSource.On;
        }

        public LightMode GetLightMode()
        {
            return _lightSource.Mode;
        }

        public void ResetLighting()
        {
            _lightSource.Reset();
        }

        #endregion

        #region material parameters manipulation

        public void SetMaterialParams(int ambi, int diff, int spec, int shine)
        {
            if (!IsEmpty())
            {
                _sceneObjects[_selectedObject].SetMaterialParameters(
                    ambi * MATERIAL_AMBIENT_MULTIPLIER,
                    diff * MATERIAL_DIFFUSE_MULTIPLIER,
                    spec * MATERIAL_SPECULAR_MULTIPLIER,
                    (int)MathF.Pow(2f, shine));
            }
        }

        public void SetMaterialColor(Color col)
        {
            if (!IsEmpty())
            {
                _sceneObjects[_selectedObject].SetMaterialColor(Utils.ColorToVec(col));
            }
        }

        public Vector4 GetMaterialParams()
        {
            if (!IsEmpty())
            {
                var paras = _sceneObjects[_selectedObject].GetMaterialParameters();
                var amb = paras.x / MATERIAL_AMBIENT_MULTIPLIER;
                var diff = paras.y / MATERIAL_DIFFUSE_MULTIPLIER;
                var spec = paras.z / MATERIAL_SPECULAR_MULTIPLIER;
                var shine = (int)MathF.Log2(paras.w);
                return new Vector4(amb, diff, spec, shine);
            }
            return default;
        }

        public Color GetMaterialColor()
        {
            if (!IsEmpty())
            {
                return Utils.VecToColor(_sceneObjects[_selectedObject].GetMaterialColor());
            }
            return default;
        }

        public void ResetMaterial()
        {
            if (!IsEmpty())
            {
                _sceneObjects[_selectedObject].ResetMaterial();
            }
        }

        #endregion

        #region textures manipulation

        public void SetTexture(int iTex)
        {
            if (!IsEmpty())
            {
                _sceneObjects[_selectedObject].SetTextureIndex(iTex);
            }
        }

        public void RemoveTexture()
        {
            if (!IsEmpty())
            {
                _sceneObjects[_selectedObject].RemoveTexture();
            }
        }

        public int GetTexture()
        {
            if (!IsEmpty())
            {
                return _sceneObjects[_selectedObject].GetTextureIndex();
            }
            return default;
        }

        #endregion

        // sets projection mode: perspective or orthographics
        public void SetProjectionMode(bool perspective)
        {
            _perspective = perspective;
        }

        // set drawing mode flags: wireframe and solid
        public void SetDrawingMode(bool wireframe, bool solid)
        {
            _drawWireFrame = wireframe;
            _drawSolid = solid;
        }

        public void SetCulling(bool cull)
        {
            _faceCull = cull;
        }

        public void SetWrapMode(TextureWrapMode mode)
        {
            _wrapMode = mode;
        }

        public bool IsEmpty()
        {
            return _sceneObjects.Count == 0;
        }

        public bool IsSelectedFirst()
        {
            return _selectedObject == 0 && !IsEmpty();
        }

        public bool isSelectedLast()
        {
            return _selectedObject == _sceneObjects.Count - 1 && !IsEmpty();
        }

        public bool isPerspective()
        {
            return _perspective;
        }

        public bool IsWireframeMode()
        {
            return _drawWireFrame;
        }

        public bool IsSolidMode()
        {
            return _drawSolid;
        }

        public bool IsCulling()
        {
            return _faceCull;
        }

        public TextureWrapMode GetWrapMode()
        {
            return _wrapMode;
        }

        public int ObjectCount()
        {
            return _sceneObjects.Count;
        }

        public int GetSelected()
        {
            return _selectedObject;
        }

        public bool FromFile(string file)
        {
            var delimStr = "\r\n";
            var delimiter = delimStr.ToCharArray();
            var lines = file.Split(delimiter);

            // parse each line
            var colors = new List<Vector4>();


        }


        private Light _lightSource;
        private Camera _camera;
        private List<RenderObject> _sceneObjects = new List<RenderObject>();
        private int _selectedObject;
        private bool _perspective;
        private bool _faceCull;
        private TextureWrapMode _wrapMode;
        private bool _drawWireFrame;
        private bool _drawSolid;
    }
}
