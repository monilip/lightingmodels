using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace LightingModels
{
    public class ObjVolume : Volume
    {
        Vector3[] vertices;
        Vector3[] normals;
        Vector3[] colors;
        Vector2[] textureCoords;
        Material material;
        List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();

        public override int VertCount { get { return vertices.Length; } }
        public override int NormalsCount { get { return normals.Length; } }
        public override int IndiceCount { get { return faces.Count * 3; } }
        public override int ColorDataCount { get { return colors.Length; } }

      
        //
        public override Vector3[] GetVerts()
        {
            return vertices;
        }

        // 
        public override Vector3[] GetNormals()
        {
            return normals;
        }

        //
        public override int[] GetIndices(int offset = 0)
        {
            List<int> indices = new List<int>();

            foreach (var face in faces)
            {
                indices.Add(face.Item1 + offset);
                indices.Add(face.Item2 + offset);
                indices.Add(face.Item3 + offset);
            }

            return indices.ToArray();
        }

        //
        public override Vector3[] GetColorData()
        {
            return colors;
        }

        //
        public override Vector2[] GetTextureCoords()
        {
            return textureCoords;
        }


        //public Material GetMaterial()
        //{
        //    return material;
        //}

        ////
        //public Material GetMaterial(string name)
        //{
        //    return material.GetMaterial(name);
        //}

        //
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.Scale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
        }
                  
        # region static loading Blender obj methods
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
                UsefulMethods.Log("File not found: "+ filename);
            }
            catch (Exception e)
            {
                UsefulMethods.Log("Error loading file:" + filename);
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

            // Lists to hold model data
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> textureCords = new List<Vector2>();
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();


            // Read file line by line
            foreach (String line in lines)
            {
                if (line.StartsWith("mtllib"))// material file
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);
                    temp = temp.Trim('\r', '\t');

                    String[] vertParts = temp.Split(' ');
                    try
                    {
                        material = new Material();

                        material.Load(DataPath.MaterialsPath + vertParts[1]);
                    }
                    catch (Exception e)
                    {
                        UsefulMethods.Log(e.ToString());
                    }
                }

                else if (line.StartsWith("v ")) // Vertex definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);
                    temp = temp.Trim('\r', '\t');

                    Vector3 vec = new Vector3();
                                        
                    String[] vertParts = temp.Split(' ');

                    vec.X = UsefulMethods.GetFloat(vertParts[0]);
                    vec.Y = UsefulMethods.GetFloat(vertParts[1]);
                    vec.Z = UsefulMethods.GetFloat(vertParts[2]);

                    // todo
                    // temporaty color/texture coordinates for now
                    colors.Add(new Vector3(1f,0f,1f));
                    
                    vertices.Add(vec);
                }
                
                else if (line.StartsWith("vn ")) // Normals definition
                {
                    // todo
                }

                else if (line.StartsWith("vt ")) // Texture coordinates
                {
                    String temp = line.Substring(2);
                    temp = temp.Trim('\r', '\t');

                    String[] lineParts = temp.Split(' ');

                    textureCords.Add(new Vector2(UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2])));
                }

                else if (line.StartsWith("f ")) // Face definition
                {
                    // Example: f 4/3/2 2/2/1 3/1/1 

                    // Cut off beginning of line
                    String temp = line.Substring(2);
                    temp = temp.Trim('\r', '\t');

                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);

                    String[] facesIndices = temp.Split(' ');

                    // add face's vectors's index: int, int, int
                    // add face's normals's vextor: Vector3 // todo -> change to list and keep here only indexes!
                    // add face's textCords: TODO
                    // counting starts with 1, not 0!
                    int[] faceVectors = new int[3];
                    int[] faceNormals = new int[3];
                    for (int i = 0; i < 3; i++)
                    {
                        // example: indice = 4/3/2
                        // 4 - vertext's vector
                        // 3 - vertext's textCord
                        // 2 - vertext's normal
 
                        String[] indices = facesIndices[i].Split('/');

                        faceVectors[i] = UsefulMethods.GetInt(indices[0]) - 1;
                        // faceNormals[i] = int.Parse(indices[2], CultureInfo.InvariantCulture.NumberFormat) - 1;
                    }

                    face = new Tuple<int, int, int>(faceVectors[0], faceVectors[1], faceVectors[2]);
                    faces.Add(face);

                    normals.Add(new Vector3(faceNormals[0],faceNormals[1],faceNormals[2]));
                }
            }

            // Create the ObjVolume
            this.vertices = vertices.ToArray();
            this.normals = normals.ToArray();
            this.faces = new List<Tuple<int, int, int>>(faces);
            this.colors = colors.ToArray();
            this.textureCoords = textureCords.ToArray();
        }
        # endregion

        public override void Render(ShaderProgram shader, int indiceat)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureID);
            GL.UniformMatrix4(shader.GetUniform("modelView"), false, ref ModelViewProjectionMatrix);
            GL.UniformMatrix4(shader.GetUniform("modelViewMatrix"), false, ref ModelMatrix);
            GL.UniformMatrix4(shader.GetUniform("projectionMatrix"), false, ref ViewProjectionMatrix);

            if (shader.GetAttribute("maintexture") != -1)
            {
                GL.Uniform1(shader.GetAttribute("maintexture"), TextureID);
            }

            material.SetMaterial();

            GL.DrawElements(BeginMode.Triangles, IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
        }
    }


 
}
