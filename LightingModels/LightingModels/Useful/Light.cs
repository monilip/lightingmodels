using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Collections.Generic;
using LightingModels.Useful;

// 11.07.2015
namespace LightingModels
{
    class Light : Element
    {
        private int lightNum = 0;

        public static List<Light> Lights = new List<Light>();
        public Vector3 Diffuse = new Vector3(0.8f, 0.8f, 0.8f);
        public Vector3 Specular = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 Ambient = new Vector3(0.1f, 0.1f, 0.1f);
        public bool Enabled = true;

        //
        public Light(string name)
            : base(name)
        {

        }

        #region GL methods
        // 
        public static void Enable()
        {
            GL.Enable(EnableCap.Lighting);
        }

        // 
        public static void Disable()
        {
            GL.Disable(EnableCap.Lighting);
        }

        //
        public void SetLight(bool enable)
        {
            if (enable == false)
            {
                GL.Disable(EnableCap.Light0 + lightNum);
                Enabled = false;
                return;
            }
            Enabled = true;
            GL.Enable(EnableCap.Light0 + lightNum);
            UpdateColor();
        }

        //
        public void UpdateColor()
        {
            GL.Light(LightName.Light0 + lightNum, LightParameter.Ambient, new float[] { Ambient.X, Ambient.Y, Ambient.Z });
            GL.Light(LightName.Light0 + lightNum, LightParameter.Diffuse, new float[] { Diffuse.X, Diffuse.Y, Diffuse.Z });
            GL.Light(LightName.Light0 + lightNum, LightParameter.Specular, new float[] { Specular.X, Specular.Y, Specular.Z });
        }

        #endregion

        //
        public static void Add(Light light, int lightNum)
        {
            light.lightNum = lightNum;
            light.SetLight(true);
            Lights.Add(light);
        }

        public static void Remove(Light light)
        {
            light.SetLight(false);
            Lights.Remove(light);
        }
        public void Remove()
        {
            SetLight(false);
            Lights.Remove(this);
        }
    }
}
