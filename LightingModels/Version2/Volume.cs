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
        public Vector3 Scale = new Vector3(1,1,1);

        public List<Vector3> VertexsList = new List<Vector3>();
        public List<Vector3> NormalsList = new List<Vector3>();
        public List<Vector2> UVsList = new List<Vector2>();
        public List<Face> FacesList = new List<Face>();

        public VBO<Vector3> VertexsVBO;
        public VBO<Vector3> NormalsVBO;
        public VBO<Vector2> UVsVBO;
    }
}
