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
        Vector2[] texturecoords;

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

        // todo
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
            return texturecoords;
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
            objVolume.texturecoords = textures.ToArray();
  
            return objVolume;
        }


        # endregion
    }
 
}
