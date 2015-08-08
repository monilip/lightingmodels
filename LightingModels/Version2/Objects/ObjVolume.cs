using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenGL;

namespace Version2
{
    public class ObjVolume : Volume
    {
        #region loadingFromFile
        //
        public void LoadFromFileFromBlenderObj(string filename)
        {
            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
                {
                    LoadFromStringFromBlenderObj(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException e)
            {
                Useful.Log("File not found: " + filename);
            }
            catch (Exception e)
            {
                Useful.Log("Error loading file:" + filename);
            }
        }
        
        //
        // Examples:
        // v 1.000000 -1.000000 -1.000000 -> vertex.xyz = (1, -1, -1)
        // vn 0.000000 -1.000000 0.000000 -> vertexNormal.xyz = (0, -1, 0)
        // vt 0.1 0.1 0.0 -> textureCord = (0.1, 0.1)
        // f 4/3/2 2/2/1 3/1/1 
        // -> face.vertext1 = vertexts[4-1]     // counting starts with 1, not 0!
        // -> face.textCord1 = textureCord[3-1] // counting starts with 1, not 0!
        // -> face.normal1 = normals[2-1]       // counting starts with 1, not 0!
        public void LoadFromStringFromBlenderObj(string obj)
        {
            // Seperate lines from the file
            List<String> lines = new List<string>(obj.Split('\n'));


            int facesNumber = 0;
            String temp;
            float x, y, z;
            String[] lineParts;
            Face face;
            string lineStart;

            // Read file line by line
            foreach (String line in lines)
            {
                if (line.Length == 0)
                    continue;

                lineStart = line.Substring(0, 2);

                switch (lineStart)
                {
                    // verticles
                    case "v ":
                        // Cut off beginning of line
                        temp = line.Substring(2);
                        temp = temp.Trim('\r', '\t');
        
                        lineParts = temp.Split(' ');

                        x = Useful.GetFloat(lineParts[0]);
                        y = Useful.GetFloat(lineParts[1]);
                        z = Useful.GetFloat(lineParts[2]);

                        // temporary
                        ColorList.Add(new Vector3(1f, 0f, 1f));

                        VertexsList.Add(new Vector3(x, y, z));
                        
                        break;

                    // normals
                    case "vn":
                        // Cut off beginning of line
                        temp = line.Substring(3);
                        temp = temp.Trim('\r', '\t');

                        lineParts = temp.Split(' ');

                        x = Useful.GetFloat(lineParts[0]);
                        y = Useful.GetFloat(lineParts[1]);
                        z = Useful.GetFloat(lineParts[2]);

                        NormalsList.Add(new Vector3(x, y, z));

                        break;

                    // uvs
                    case "vt":
                        // Cut off beginning of line
                        temp = line.Substring(3);
                        temp = temp.Trim('\r', '\t');

                        lineParts = temp.Split(' ');

                        x = Useful.GetFloat(lineParts[0]);
                        y = Useful.GetFloat(lineParts[1]);

                        UVsList.Add(new Vector2(x, y));

                        break;

                    // faces: creating VBOs
                    // three parts create a face
                    // example: f 5/3/6 1/4/6 8/2/6              
                    case "f ":
                        // Cut off beginning of line
                        temp = line.Substring(2);
                        temp = temp.Trim('\r', '\t');

                        lineParts = temp.Split(' ');

                        int[] faceVectors = new int[3];
                        int[] faceNormals = new int[3];
                        int[] faceUVs = new int[3];
                        for (int i = 0; i < 3; i++)
                        {
                            // example: part = 4/3/2
                            // 4 - vertext's vector
                            // 3 - vertext's textCord
                            // 2 - vertext's normal

                            String[] indices = lineParts[i].Split('/');

                            faceVectors[i] = Useful.GetInt(indices[0]) -1;
                            faceUVs[i] = Useful.GetInt(indices[1]) - 1;
                            faceNormals[i] = Useful.GetInt(indices[2]) -1;
                        }

                        face = new Face();
                        face.VertexsIndices = Tuple.Create<int, int, int>(faceVectors[0], faceVectors[1], faceVectors[2]);
                        face.UVsIndices = Tuple.Create<int, int, int>(faceUVs[0], faceUVs[1], faceUVs[2]);
                        face.NormalsIndices = Tuple.Create<int, int, int>(faceNormals[0], faceNormals[1], faceNormals[2]);
                        face.Number = facesNumber;
                        facesNumber++;

                        FacesList.Add(face);
                        
                        break;

                    case "mt":
                        if (line.Substring(0, 6).Equals("mtllib"))
                        {
                            // Cut off beginning of line
                            temp = line.Substring(6);
                            temp = temp.Trim('\r', '\t');

                            String[] vertParts = temp.Split(' ');
                            this.Material = null;
                            try
                            {
                                this.Material = new Material();

                                Material.Load(Useful.GetModelsPath() + vertParts[1]);
                            }
                            catch (Exception e)
                            {
                                Useful.Log(e.ToString());
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

            // creating VBO from faces:
            Vector3[] dataVertexs = new Vector3[FacesList.Count*3];
            Vector3[] dataColors = new Vector3[FacesList.Count*3];
            Vector3[] dataNormals = new Vector3[FacesList.Count * 3];
            Vector2[] dataUVs = new Vector2[FacesList.Count * 3];
            int[] triangles = new int[FacesList.Count * 3];
            for (int i = 0; i < FacesList.Count; i++)
            {
                dataVertexs[i*3] = VertexsList[FacesList[i].VertexsIndices.Item1];
                dataVertexs[i * 3 + 1] = VertexsList[FacesList[i].VertexsIndices.Item2];
                dataVertexs[i * 3 + 2] = VertexsList[FacesList[i].VertexsIndices.Item3];

                dataColors[i * 3] = ColorList[FacesList[i].VertexsIndices.Item1];
                dataColors[i * 3 + 1] = ColorList[FacesList[i].VertexsIndices.Item2];
                dataColors[i * 3 + 2] = ColorList[FacesList[i].VertexsIndices.Item3];

                dataNormals[i * 3] = NormalsList[FacesList[i].NormalsIndices.Item1];
                dataNormals[i * 3 + 1] = NormalsList[FacesList[i].NormalsIndices.Item2];
                dataNormals[i * 3 + 2] = NormalsList[FacesList[i].NormalsIndices.Item3];

                dataUVs[i * 3] = UVsList[FacesList[i].UVsIndices.Item1];
                dataUVs[i * 3 + 1] = UVsList[FacesList[i].UVsIndices.Item2];
                dataUVs[i * 3 + 2] = UVsList[FacesList[i].UVsIndices.Item3];

                triangles[i * 3] = i * 3;
                triangles[i * 3 + 1] = i * 3 + 1;
                triangles[i * 3 + 2] = i * 3 + 2;
            }
            
        //    Useful.LogVec3Array(dataVertexs,"vertex");
         //   Useful.LogVec3Array(dataNormals,"normals");
         //   Useful.LogVec2Array(dataUVs, "uv");
         //   Useful.LogIntArray(triangles, "triangles");

            this.VertexsVBO = new VBO<Vector3>(dataVertexs);
            this.ColorsVBO = new VBO<Vector3>(dataColors);
            this.NormalsVBO = new VBO<Vector3>(dataNormals);
            this.UVsVBO = new VBO<Vector2>(dataUVs);
            this.TrianglesVBO = new VBO<int>(triangles, BufferTarget.ElementArrayBuffer);

        }

        #endregion


    }
}
