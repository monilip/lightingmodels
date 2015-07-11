using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

// 15.05.2015
namespace LightingModels
{
    // Abstract class for all volumes on scene
    public abstract class Volume
    {
        public string Name;
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public virtual int VertCount { get; set; }
        public virtual int NormalsCount { get; set; }
        public virtual int IndiceCount { get; set; }
        public virtual int ColorDataCount { get; set; }
        public virtual int FacesCount { get; set; }

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public abstract Vector3[] GetVerts();
        public abstract Vector3[] GetNormals();
        public abstract int[] GetIndices(int offset = 0);
        public abstract Vector3[] GetColorData();
        public abstract void CalculateModelMatrix();

        public bool IsTextured = false;
        public int TextureID;
        public int TextureCoordsCount;
        public abstract Vector2[] GetTextureCoords();

        public void RotateObject(float x, float y, float z)
        {
            Rotation = new Vector3(Rotation.X + x, Rotation.Y + y, Rotation.Z + z);
        }

        public void MoveObject(float x, float y, float z)
        {
            Position = new Vector3(Position.X + x, Position.Y + y, Position.Z + z);
        }

        // Surface normals
        public static Vector3 CalculateFaceNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 A, B;

            // A
            A.X = a.X - b.X;
            A.Y = a.Y - b.Y;
            A.Z = a.Z - b.Z;

            // B
            B.X = b.X - c.X;
            B.Y = b.Y - c.Y;
            B.Z = b.Z - c.Z;

            // calculate the cross product and place the resulting vector
            // into the normal
            Vector3 faceNormal = new Vector3();
            faceNormal.X = (A.Y * B.Z) - (A.Z * B.Y);
            faceNormal.Y = (A.Z * B.X) - (A.X * B.Z);
            faceNormal.Z = (A.X * B.Y) - (A.Y * B.X);

            // normalize
            return UsefulMethods.Normalize(faceNormal);
        }

        public virtual void Render(ShaderProgram shader, int indiceat)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.UniformMatrix4(shader.GetUniform("modelView"), false, ref ModelViewProjectionMatrix);
            GL.UniformMatrix4(shader.GetUniform("modelViewMatrix"), false, ref ModelMatrix);
            GL.UniformMatrix4(shader.GetUniform("projectionMatrix"), false, ref ViewProjectionMatrix);

            if (shader.GetAttribute("maintexture") != -1)
            {
                GL.Uniform1(shader.GetAttribute("maintexture"), TextureID);
            }

            GL.DrawElements(BeginMode.Triangles, IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
        }
    }
}
