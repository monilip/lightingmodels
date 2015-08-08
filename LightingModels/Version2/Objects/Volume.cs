using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;

namespace Version2
{
    public abstract class Volume
    {
        public string Name;
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = new Vector3(1, 1, 1);

        public List<Vector3> VertexsList = new List<Vector3>();
        public List<Vector3> ColorList = new List<Vector3>();
        public List<Vector3> NormalsList = new List<Vector3>();
        public List<Vector2> UVsList = new List<Vector2>();
        public List<Face> FacesList = new List<Face>();

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        public VBO<Vector3> VertexsVBO;
        public VBO<Vector3> ColorsVBO;
        public VBO<Vector3> NormalsVBO;
        public VBO<Vector2> UVsVBO;
        public VBO<int> TrianglesVBO;
        public VBO<int> QuadsVBO;

        public string TextureName;
        public Texture Texture;

        public Material Material;

        public OpenGL.Texture GetTexture()
        {
            if (Material != null)
                return Material.GetTexture();
            else
                return Texture;
        }

        public Matrix4 CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.CreateScaling(Scale) * Matrix4.CreateRotationX(Rotation.x) * Matrix4.CreateRotationY(Rotation.y) * Matrix4.CreateRotationZ(Rotation.z) * Matrix4.CreateTranslation(Position);
            return ModelMatrix;
        }

        public void RotateObject(float x, float y, float z)
        {
            Rotation = new Vector3(Rotation.x + x, Rotation.y + y, Rotation.z + z);
        }
    }
}
