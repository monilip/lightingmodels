using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// 07.05.15
// Rendering scenes
namespace LightingModels
{
    public class Scene1
    {
        // Array of our vertex positions
        private Vector3[] vertdata;
        // Array of our vertex colors
        private Vector3[] coldata;
        // Array of our modelview matrices
        private Matrix4[] mviewdata;

        //
        public void LoadVertex()
        {
            vertdata = new Vector3[] { new Vector3(-0.8f, -0.8f, 0f),
                new Vector3( 0.8f, -0.8f, 0f), 
                new Vector3( 0f,  0.8f, 0f)};

            coldata = new Vector3[] { new Vector3(1f, 0f, 0f),
                new Vector3( 0f, 0f, 1f), 
                new Vector3( 0f,  1f, 0f)};

            mviewdata = new Matrix4[]{
                Matrix4.Identity
            };
        }

        // Set shaders and its paramenters
        public void UpdateScene()
        {
            LoadVertex();

            GL.BindBuffer(BufferTarget.ArrayBuffer, Singleton.ShaderProgram.VBOPosition);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(Singleton.ShaderProgram.AttributeVPos, 3, VertexAttribPointerType.Float, false, 0, 0);

            GL.BindBuffer(BufferTarget.ArrayBuffer, Singleton.ShaderProgram.VBOColor);
            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(Singleton.ShaderProgram.AttributeVCol, 3, VertexAttribPointerType.Float, true, 0, 0);

            GL.UniformMatrix4(Singleton.ShaderProgram.UniformModelView, false, ref mviewdata[0]);

            GL.UseProgram(Singleton.ShaderProgram.Program);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        // Render scene
        public void RenderScene()
        {
            
            GL.EnableVertexAttribArray(Singleton.ShaderProgram.AttributeVPos);
            GL.EnableVertexAttribArray(Singleton.ShaderProgram.AttributeVCol);

            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            GL.DisableVertexAttribArray(Singleton.ShaderProgram.AttributeVPos);
            GL.DisableVertexAttribArray(Singleton.ShaderProgram.AttributeVCol);

            GL.Flush();
        }
    }
}
