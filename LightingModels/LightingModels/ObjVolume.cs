using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization; // for parsing float from string with ".", not ","
using OpenTK;
namespace LightingModels
{
    class ObjVolume : Volume
    {
        Vector3[] vertices;
        Vector3[] normals;
        Vector3[] colors;
        Vector2[] textureCoords;

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

        //
        public override void CalculateModelMatrix()
        {
            ModelMatrix = Matrix4.Scale(Scale) * Matrix4.CreateRotationX(Rotation.X) * Matrix4.CreateRotationY(Rotation.Y) * Matrix4.CreateRotationZ(Rotation.Z) * Matrix4.CreateTranslation(Position);
        }

        # region static loading methods
        //
        public static ObjVolume LoadFromFile(string filename)
        {
            ObjVolume obj = new ObjVolume();
            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
                {
                    obj = LoadFromString(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found: ", filename);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading file:", filename);
            }

            return obj;
        }

        public static ObjVolume LoadFromString(string obj)
        {
            // Seperate lines from the file
            List<String> lines = new List<string>(obj.Split('\n'));

            // Lists to hold model data
            List<Vector3> vertices = new List<Vector3>();
            List<Vector3> normals= new List<Vector3>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> textures = new List<Vector2>();
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();

            // Read file line by line
            foreach (String line in lines)
            {
                if (line.StartsWith("v ")) // Vertex definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Vector3 vec = new Vector3();
                                        
                    String[] vertParts = temp.Split(' ');

                    try
                    {
                        vec.X = float.Parse(vertParts[0], CultureInfo.InvariantCulture.NumberFormat);
                        vec.Y = float.Parse(vertParts[1], CultureInfo.InvariantCulture.NumberFormat);
                        vec.Z = float.Parse(vertParts[2], CultureInfo.InvariantCulture.NumberFormat);

                        // Temporaty color/texture coordinates for now
                        colors.Add(new Vector3((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));
                        textures.Add(new Vector2((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));

                        vertices.Add(vec);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error parsing face: {0}", line);
                    }                        
                }
                else if (line.StartsWith("f ")) // Face definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);

                    String[] faceparts = temp.Split(' ');

                    int i1, i2, i3;

                    try
                    {
                        i1 = int.Parse(faceparts[0], CultureInfo.InvariantCulture.NumberFormat);
                        i2 = int.Parse(faceparts[1], CultureInfo.InvariantCulture.NumberFormat);
                        i3 = int.Parse(faceparts[2], CultureInfo.InvariantCulture.NumberFormat);
                        face = new Tuple<int, int, int>(i1 - 1, i2 - 1, i3 - 1);
                        faces.Add(face);                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error parsing face: {0}", line);
                    }
                }
            }

            // create normals
            foreach ( Tuple<int, int, int> face in faces)
            {
                 // add normal to this face
                normals.Add(Scene.CalculateFaceNormal(vertices[face.Item1], vertices[face.Item2], vertices[face.Item3]));
             }
                       

            // Create the ObjVolume
            ObjVolume objVolume = new ObjVolume();
            objVolume.vertices = vertices.ToArray();
            objVolume.normals = normals.ToArray();
            objVolume.faces = new List<Tuple<int, int, int>>(faces);
            objVolume.colors = colors.ToArray();
            objVolume.textureCoords = textures.ToArray();
  
            return objVolume;
        }
        
        # region static loading Blender obj methods
        //
        public static ObjVolume LoadFromFileFromBlenderObj(string filename)
        {
            ObjVolume obj = new ObjVolume();
            try
            {
                using (StreamReader reader = new StreamReader(new FileStream(filename, FileMode.Open, FileAccess.Read)))
                {
                    obj = LoadFromStringFromBlenderObj(reader.ReadToEnd());
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File not found: ", filename);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading file:", filename);
            }

            return obj;
        }

        //
        // Examples:
        // v 1.000000 -1.000000 -1.000000 -> vertex.xyz = (1, -1, -1)
        // vn 0.000000 -1.000000 0.000000 -> vertexNormal.xyz = (0, -1, 0)
        // vt 0.1 0.1 -> textureCord = (0.1, 0.1)
        // f 4/3/2 2/2/1 3/1/1 
        // -> face.vertext1 = vertexts[4-1]     // counting starts with 1, not 0!
        // -> face.textCord1 = textureCord[3-1] // counting starts with 1, not 0!
        // -> face.normal1 = normals[2-1]       // counting starts with 1, not 0!
        public static ObjVolume LoadFromStringFromBlenderObj(string obj)
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
                if (line.StartsWith("v ")) // Vertex definition
                {
                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Vector3 vec = new Vector3();

                    String[] vertParts = temp.Split(' ');

                    try
                    {
                        vec.X = float.Parse(vertParts[0], CultureInfo.InvariantCulture.NumberFormat);
                        vec.Y = float.Parse(vertParts[1], CultureInfo.InvariantCulture.NumberFormat);
                        vec.Z = float.Parse(vertParts[2], CultureInfo.InvariantCulture.NumberFormat);

                        // todo
                        // temporaty color/texture coordinates for now
                        colors.Add(new Vector3((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));
                        textureCords.Add(new Vector2((float)Math.Sin(vec.Z), (float)Math.Sin(vec.Z)));

                        vertices.Add(vec);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error parsing face: {0}", line);
                    }
                }
                
                else if (line.StartsWith("vn ")) // Normals definition
                {
                    // todo
                }

                else if (line.StartsWith("vt ")) // Texture coordinates
                {
                    // todo
                }

                else if (line.StartsWith("f ")) // Face definition
                {
                    // Example: f 4/3/2 2/2/1 3/1/1 

                    // Cut off beginning of line
                    String temp = line.Substring(2);

                    Tuple<int, int, int> face = new Tuple<int, int, int>(0, 0, 0);

                    String[] facesIndices = temp.Split(' ');

                    try
                    {
                        // add face's vectors's index: int, int, int
                        // add face's normals's vextor: Vector3 // todo -> change to list and keep here only indexes!
                        // add face's textCords: TODO
                        // counting starts with 1, not 0!
                        int[] faceVectors = new int[3];
                        int[] faceNormals = new int[3];
                        for (int i = 0; i < facesIndices.Length; i++)
                        {
                            // example: indice = 4/3/2
                            // 4 - vertext's vector
                            // 3 - vertext's textCord
                            // 2 - vertext's normal
 
                            String[] indices = facesIndices[i].Split('/');

                            faceVectors[i] = int.Parse(indices[0], CultureInfo.InvariantCulture.NumberFormat) - 1;
                           // faceNormals[i] = int.Parse(indices[2], CultureInfo.InvariantCulture.NumberFormat) - 1;
                        }

                        face = new Tuple<int, int, int>(faceVectors[0], faceVectors[1], faceVectors[2]);
                        faces.Add(face);

                        normals.Add(new Vector3(faceNormals[0],faceNormals[1],faceNormals[2]));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error parsing face: {0}", line);
                    }
                }
            }

            // Create the ObjVolume
            ObjVolume objVolume = new ObjVolume();
            objVolume.vertices = vertices.ToArray();
            objVolume.normals = normals.ToArray();
            objVolume.faces = new List<Tuple<int, int, int>>(faces);
            objVolume.colors = colors.ToArray();
            objVolume.textureCoords = textureCords.ToArray();

            return objVolume;
        }
        # endregion
        # endregion
    }
 
}
