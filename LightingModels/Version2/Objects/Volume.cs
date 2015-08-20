using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;

namespace Version2
{
    public abstract class Volume
    {
        public enum UpdateType
        {
            POSITION,
            ROTATION,
        }

        public event UpdateHandler UpdateVolume;
        public delegate void UpdateHandler(UpdateType type);

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
            ModelMatrix = Matrix4.CreateScaling(Scale) * Matrix4.CreateRotationY(Rotation.y) * Matrix4.CreateRotationZ(Rotation.z) * Matrix4.CreateRotationX(Rotation.x) * Matrix4.CreateTranslation(Position);
            return ModelMatrix;
        }

        public void RotateObjectAxis(float xx, float yy, float zz)
        {
            float x = Rotation.x + xx;
            if (x >= 2 * Math.PI || x < 0)
                x = 0;

            float y = Rotation.y + yy;
            if (y >= 2 * Math.PI || y < 0)
                y = 0;

            float z = Rotation.z + zz;
            if (z >= 2 * Math.PI || z < 0)
                z = 0;
            Rotation = new Vector3(x, y, z);

            UpdateVolume(UpdateType.ROTATION);
        }

        public void MoveObject(float xx, float yy, float zz)
        {
            Position = new Vector3(Position.x + xx, Position.y + yy, Position.z + zz);

            UpdateVolume(UpdateType.POSITION);
        }
    }
}
