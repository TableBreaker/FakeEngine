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
            _lightSource = new Light(_camera.position);
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
            //renderer
            
        }

        #region methods to manipulate objects;

        public void SetObjectPosition(int x_coord, int y_coord, int z_coord)
        {

        }

        public void SetObjectRotation(float x_angle, float y_angle, float z_angle)
        {

        }

        public void SetObjectReflection(bool xy, bool xz, bool yz)
        {

        }

        public void SetObjectScale(int x_scale, int y_scale, int z_scale)
        {

        }

        public Vector3 GetObjectPosition(bool worldCoords)
        {
            return default;
        }

        public Vector3 GetObjectRotation(bool worldCoords)
        {
            return default;
        }

        public Vector3 GetObjectReflection()
        {
            return default;
        }
        
        public Vector3 GetObjectScale(bool worldCoords)
        {
            return default;
        }

        public void ResetObject()
        {

        }

        public void DeleteObject()
        {

        }

        public void SelectNextObject()
        {

        }

        public void SelectPreviousObject()
        {

        }

        #endregion

        #region methods to manupulate camera
        
        public void SetCameraPosition(int x_coord, int y_coord, int z_coord)
        {

        }

        public void SetLightParams(float ambient_, float diffuse_, float specular_)
        {

        }

        public Vector3 GetLightParams(bool worldParams)
        {
            return default;
        }

        public void SetCameraRotation(float pitch_, float yaw, float roll)
        {

        }

        public Vector3 GetCameraPosition(bool worldCoords)
        {
            return default;
        }

        public Vector3 GetCameraRotation(bool worldCoords)
        {
            return default;
        }

        public void ResetCamera()
        {

        }

        #endregion

        #region methods to manipulate lighting

        public Vector3 GetLightPosition(bool worldCoords)
        {
            return default;
        }

        public void SetLightPosition(int x_coord, int y_coord, int z_coord)
        {

        }

        public void SetLightColor(Color lightColor)
        {

        }

        public void SetLightOn(bool isOn)
        {

        }

        public void SetLightMode(LightMode mode)
        {

        }

        public Color GetLightColor()
        {
            return default;
        }

        public bool IsLightOn()
        {
            return default;
        }

        public LightMode GetLightMode()
        {
            return default;
        }

        public void ResetLighting()
        {

        }

        #endregion

        #region material parameters manipulation

        public void SetMaterialParams(int ambi, int diff, int spec, int shine)
        {

        }

        public void SetMaterialColor(Color col)
        {

        }

        public Vector4 GetMaterialParams()
        {
            return default;
        }

        public Color GetMaterialColor()
        {
            return default;
        }

        public void ResetMaterial()
        {

        }

        #endregion

        #region textures manipulation

        public void SetTexture(int iTex)
        {

        }

        public void RemoveTexture()
        {

        }

        public int GetTexture()
        {
            return default;
        }

        #endregion

        // sets projection mode: perspective or orthographics
        public void SetProjectionMode(bool perspective)
        {

        }

        // set drawing mode flags: wireframe and solid
        public void SetDrawingMode(bool wireframe, bool solid)
        {

        }

        public void SetCulling(bool cull)
        {

        }

        public void SetWrapMode(TextureWrapMode mode)
        {

        }

        public bool IsEmpty()
        {
            return default;
        }

        public bool IsSelectedFirst()
        {
            return default;
        }

        public bool isSelectedLast()
        {
            return default;
        }

        public bool isPerspective()
        {
            return default;
        }

        public bool IsWireframeMode()
        {
            return default;
        }

        public bool IsSolidMode()
        {
            return default;
        }

        public bool IsCulling()
        {
            return default;
        }

        public TextureWrapMode GetWrapMode()
        {
            return default;
        }

        public uint ObjectCount()
        {
            return default;
        }

        public uint GetSelected()
        {
            return default;
        }

        public void AddObject(RenderObject obj)
        {

        }

        public bool FromFile(string file)
        {
            return default;
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
