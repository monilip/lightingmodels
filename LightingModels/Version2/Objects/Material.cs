using System;
using OpenGL;
using System.Collections.Generic;
using System.Globalization; // for parsing float from string with ".", not ","

// 11.07.2015
namespace Version2
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
        private Texture diffuseTexture;

        //
        public Material()
        {

        }

        //
        public Texture GetDiffuseTexture()
        {
            return diffuseTexture;
        }

        //
        public string GetMaterialName()
        {
            return name;
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
                    if (line.Length == 0)
                        continue;

                    if (line.StartsWith("newmtl")) 
                    {
                        String temp = line.Substring(7);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');

                        name = lineParts[0];

                        //Useful.Log("MaterialName: " + name);
                    }

                    // Diffuse color texture map
                    else if (line.StartsWith("map_Kd"))
                    {
                        String temp = line.Substring(7);
                        temp = temp.Trim('\r', '\t');

                        String[] lineParts = temp.Split(' ');

                        if (lineParts[0].Contains("Shader_"))
                        {
                            // todo -> material shader??
                        }
                        else
                        {
                            try
                            {
                                diffuseTexture = new Texture(Useful.GetModelsPath() + lineParts[0]);
                            }
                            catch (Exception e)
                            {
                               Useful.Log("Error!" + e.ToString());
                            }
                        }

                    }

                  
                }
            }
        }

        // todo -> different texture by paramater?
        public Texture GetTexture()
        {
            return diffuseTexture;
        }
    }
}
