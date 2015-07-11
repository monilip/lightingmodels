using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Collections.Generic;
using System.Globalization; // for parsing float from string with ".", not ","
using LightingModels.Useful;

// 11.07.2015
namespace LightingModels
{

    /*
     * Ns - Phong SpecularTex 
     * Kd - Diffuse color
     * Ka - Ambient color 
     * Ks - Specular color
     * Ni - Optical Density
     * d -nDissolve factor
     * illum 2 = Diffuse and SpecularTex shading model
     */
    class Material
    {
        private string name;

        List<Material> materials = new List<Material>();
        static string currentMaterial = "";

        
        public string ShaderName = "";
       
        public float PhongSpec = 5; 
        public Vector4 DiffuseColor = new Vector4(1.0f, 1.0f, 1.0f, 1); // Diffuse color
        public Vector4 AmbientColor = new Vector4(0.1f, 0.1f, 0.1f, 1); // Ambient color
        public Vector4 SpecularColor = new Vector4(0.5f, 0.5f, 0.5f, 1); // Specular color
        public Vector4 EmissionColor = new Vector4(0.1f, 0.1f, 0.1f, 1);
        public float Dissolve = 1; // transparency
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

                Material material = null;

                // Read file line by line
                foreach (String line in lines)
                {
                    if (line.StartsWith("newmtl")) 
                    {
                        String temp = line.Substring(7);

                        if (material != null) 
                            materials.Add(material);

                        material = new Material();

                        String[] lineParts = temp.Split(' ');

                        material.name = lineParts[0];
                        
                        UsefulMethods.Log("MaterialName: " + material.name);
                    }

                    #region texturesStuff
                    //// Diffuse color texture map
                    //if (ln[0] == "map_Kd")
                    //{
                    //    mat.DiffuseTex = new Texture();
                    //    if (loadTextures == true) mat.DiffuseTex = Texture.Load(ln[1].ToLower());
                    //    continue;
                    //}
                    //// Specular color texture map
                    //if (ln[0] == "map_Ks")
                    //{
                    //    mat.SpecularTex = new Texture();
                    //    if (loadTextures == true) mat.SpecularTex = Texture.Load(ln[1].ToLower());
                    //    continue;
                    //}
                    //// Ambient color texture map TAI shaderin nimi
                    //if (ln[0] == "map_Ka")
                    //{
                    //    // esim: Shader_cartoon  niin ladataan objektille cartoon.vert ja cartoon.frag shaderit.
                    //    if (ln[1].Contains("Shader_"))
                    //    {
                    //        // ota shaderin nimi
                    //        mat.ShaderName = ln[1].Substring(7);
                    //    }
                    //    else //Ambient color texture
                    //    {
                    //        mat.AmbientTex = new Texture();
                    //        if (loadTextures == true) mat.AmbientTex = Texture.Load(ln[1].ToLower());
                    //    }
                    //    continue;
                    //}
                    //// Bump color texture map
                    //if (ln[0] == "map_Bump")
                    //{
                    //    mat.BumpTex = new Texture();
                    //    if (loadTextures == true) mat.BumpTex = Texture.Load(ln[1].ToLower());
                    //    continue;
                    //}
                    //// Opacity color texture map
                    //if (ln[0] == "map_d")
                    //{
                    //    mat.OpacityTex = new Texture();
                    //    if (loadTextures == true) mat.OpacityTex = Texture.Load(ln[1].ToLower());
                    //    continue;
                    //}
#endregion

                    // Phong SpecularTex component
                    else if (line.StartsWith("Ns")) 
                    {
                        String temp = line.Substring(3);
                        String[] lineParts = temp.Split(' ');
                        material.PhongSpec = UsefulMethods.GetFloat(lineParts[0]);
                        continue;
                    }

                    // Ambient color
                    else  if (line.StartsWith("Ns")) 
                    {
                        String temp = line.Substring(3);
                        String[] lineParts = temp.Split(' ');
                        material.AmbientColor = new Vector4(UsefulMethods.GetFloat(lineParts[0]), UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2]), 1);
                        continue;
                    }

                    // Diffuse color
                    else if (line.StartsWith("Ns"))
                    {
                        String temp = line.Substring(3);
                        String[] lineParts = temp.Split(' ');
                        material.DiffuseColor = new Vector4(UsefulMethods.GetFloat(lineParts[0]), UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2]), 1);
                        continue;
                    }

                    // Specular color
                    else if (line.StartsWith("Ns"))
                    {
                        String temp = line.Substring(3);
                        String[] lineParts = temp.Split(' ');
                        material.SpecularColor = new Vector4(UsefulMethods.GetFloat(lineParts[0]), UsefulMethods.GetFloat(lineParts[1]), UsefulMethods.GetFloat(lineParts[2]), 1);
                    }

                    // Dissolve factor (pistetty alphaks)
                    else if (line.StartsWith("Ns"))
                    {
                        String temp = line.Substring(3);
                        String[] lineParts = temp.Split(' ');

                        material.Dissolve = UsefulMethods.GetFloat(lineParts[1]);

                        material.DiffuseColor.W = material.Dissolve;
                        material.AmbientColor.W = material.Dissolve;
                        material.SpecularColor.W = material.Dissolve;
                    }
                }

                if (material != null) 
                    materials.Add(material);
            }
        }

        //
        public Material GetMaterial(string name)
        {
            if (this.name == name) 
                return this;

            for (int i = 0; i < materials.Count; i++)
            {
                if (materials[i].name == name) 
                    return materials[i];
            }

            return null;
        }

        //
        public void SetMaterial()
        {
            if (currentMaterial == name) 
                return;

            currentMaterial = name;

            //if (DiffuseTex != null) 
            //    DiffuseTex.Bind();

            GL.Material(MaterialFace.Front, MaterialParameter.Ambient, AmbientColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Diffuse, DiffuseColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Specular, SpecularColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Emission, EmissionColor);
            GL.Material(MaterialFace.Front, MaterialParameter.Shininess, PhongSpec);
        }

        //
        public void SetMaterial(string name)
        {
            Material mat = GetMaterial(name);
            mat.SetMaterial();
        }

    }
}
