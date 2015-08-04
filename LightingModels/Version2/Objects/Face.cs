using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace Version2
{
    public class Face
    {
        public Tuple<int, int, int> VertexsIndices; 
        public Tuple<int, int, int> NormalsIndices; 
        public Tuple<int, int, int> UVsIndices;

        public int Number;
    }
}
