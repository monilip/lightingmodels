using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Collections.Generic;
using System.Globalization; // for parsing float from string with ".", not ","

// 11.07.2015
namespace LightingModels
{
    /*
     * Ns - Phong SpecularTex 
     * Kd - Diffuse color
     * Ka - Ambient color 
     * Ks - Specular color
     * Ni - Optical Density
     * d - Dissolve factor
     * illum 2 = Diffuse and SpecularTex shading model
     * map_Kd - Diffuse color texture map
     * map_Ks - Specular color texture map
     * map_Ka - Ambient color texture map
     * map_d - Opacity texture map
     */

    public class Material
    {
        private string name;

        static string currentMaterial = "";
        
        public string ShaderName = "";
       
        public float PhongSpec = 5; 
        public Vector4 DiffuseColor = new Vector4(1.0f, 1.0f, 1.0f, 1); // Diffuse color
        public Vector4 AmbientColor = new Vector4(0.1f, 0.1f, 0.1f, 1); // Ambient color
        public Vector4 SpecularColor = new Vector4(0.5f, 0.5f, 0.5f, 1); // Specular color
        public Vector4 EmissionColor = new Vector4(0.1f, 0.1f, 0.1f, 1);
        public float Dissolve = 1; // transparency

        // texture
        public int DiffuseTexId = 0;
        public int SpecularTexId = 0;
        public int AmbientTexId = 0;
        public int OpacityTexId = 0;


        //
        public Material()
        {

        }
              
        //
        public Material(string materialName)
        {
            name = materialName;
        }

        //
        public void Load(string fileName)
        {
            using (System.IO.StreamReader file = new System.IO.StreamReader(fileName))
            {
                string obj = file.ReadToEnd();

                List<String> lines = new List<string>(obj.Split('\n'));
                
                // Read file line by line
                foreach (String line in lines)
                {
                    if (line.StartsWith("newmtl")) 
                    {
                        String temp = line.Substring(7);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');

                        name = lineParts[0];
                        
                        UsefulMethods.Log("MaterialName: " + name);
                    }

                    // Diffuse color texture map
                    else if (line.StartsWith("map_Kd"))
                    {
                        String temp = line.Substring(7);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');

                        if (lineParts[0].Contains("Shader_"))
                        {
                            // todo -> material shader
                        }
                        else
                        { 
                            if (Texture.LoadTextures == true)
                                DiffuseTexId = Texture.LoadImage(DataPath.TexturesPath + lineParts[0], lineParts[0]);
                        }

                    }

                    // Ambient color texture m
                    else if (line.StartsWith("map_Ka"))
                    {
                        String temp = line.Substring(7);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');

                        if (lineParts[0].Contains("Shader_"))
                        {
                            // todo -> material shader
                        }
                        else
                        {
                            if (Texture.LoadTextures == true)
                                AmbientTexId = Texture.LoadImage(DataPath.TexturesPath + lineParts[0], lineParts[0]);
                        }
                    }
                    //// Opacity color texture map
                    //if (ln[0] == "map_d")
                    //{
                    //    mat.OpacityTex = new Texture();
                    //    
                    //if (Texture.LoadTextures == true)
                    //    OpacityTex = Texture.LoadImage(DataPath.TexturesPath + lineParts[0], lineParts[0]);
                    //}

                   // Phong SpecularTex component
                    else if (line.StartsWith("Ns"))
                    {
                        String temp = line.Substring(3);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');
                        PhongSpec = UsefulMethods.GetFloat(lineParts[0]);
                        continue;
                    }

                    // Ambient color
                    else if (line.StartsWith("Ka"))
                    {
                        String temp = line.Substring(3);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');
                        AmbientColor = new Vector4(UsefulMethods.GetFloat(lineParts[0]), UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2]), 1);
                        continue;
                    }

                    // Diffuse color
                    else if (line.StartsWith("Kd"))
                    {
                        String temp = line.Substring(3);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');
                        DiffuseColor = new Vector4(UsefulMethods.GetFloat(lineParts[0]), UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2]), 1);
                        continue;
                    }

                    // Specular color
                    else if (line.StartsWith("Ks"))
                    {
                        String temp = line.Substring(3);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');
                        SpecularColor = new Vector4(UsefulMethods.GetFloat(lineParts[0]), UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2]), 1);
                    }

                    // Dissolve factor
                    else if (line.StartsWith("d"))
                    {
                        String temp = line.Substring(2);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');

                        Dissolve = UsefulMethods.GetFloat(lineParts[0]);

                        DiffuseColor.W = Dissolve;
                        AmbientColor.W = Dissolve;
                        SpecularColor.W = Dissolve;
                    }
                }
            }
        }

        //
        public void SetMaterial()
        {
            if (currentMaterial == name) 
                return;

            currentMaterial = name;

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, AmbientColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, DiffuseColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, SpecularColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, EmissionColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, PhongSpec);
        }
    }
}
