using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// 07.05.15
// OpenGL functions independent from both Shader and Scene
namespace LightingModels
{
    public class OpenTKManager
    {
        //
        public void SetupViewport(int width, int height)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(0, width, 0, height, -1, 1);
            GL.Viewport(0, 0, width, height);
            GL.Enable(EnableCap.DepthTest);
        }

        //
        public void Clear()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
        }
    }
}
