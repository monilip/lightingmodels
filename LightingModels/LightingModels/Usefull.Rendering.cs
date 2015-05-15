using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// 05.05.15
namespace LightingModels
{
    // 
    // OpenGL function that can be used in different situations
    // - LoadShader(String filename, ShaderType type, int program, out int shader)
    partial class Usefull
    {
        

        public static void LoadShader(String filename, ShaderType type, int program, out int shader)
        {
            shader = GL.CreateShader(type);
            using (StreamReader sr = new StreamReader(filename))
            {
                GL.ShaderSource(shader, sr.ReadToEnd());
            }
            GL.CompileShader(shader);
            GL.AttachShader(program, shader);
            Log(GL.GetShaderInfoLog(shader));
        }


       
   

    }
}
