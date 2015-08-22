using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

// 21.08.2015
// Class for all Cook-Torrence lighting properties
namespace Version2
{
    class CookTorrenceProperty : ShadersProperty
    {
        //
        public void Activate()
        {
            ShaderName = "Cook-Torrence";

            // default light properties
            Vector3Properties.Add("lightPos",new Vector3(1.0f,1.0f,1.0f));
            Vector3Properties.Add("diffuseColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3Properties.Add("specularColor", new Vector3(1.0f, 1.0f, 1.0f));

            FloatProperties.Add("m", 0.25f);
            FloatProperties.Add("F0", 0.6f);

            PropertiesList = new List<Tuple<string, ShadersProperty.Type, string>>()
            {
                Tuple.Create("Position of light", ShadersProperty.Type.VECTOR3, "lightPos"),
                Tuple.Create("Diffuse color", ShadersProperty.Type.VECTOR3,"diffuseColor"),
                Tuple.Create("Specular color", ShadersProperty.Type.VECTOR3,"specularColor"),
                Tuple.Create("Roughtness Value", ShadersProperty.Type.FLOAT,"m"),
                Tuple.Create("Fresnel Value", ShadersProperty.Type.FLOAT,"F0"),
            };
        }

        //
        public override void ChangeLight(Light light)
        {
            // light properties

            // delete if light exist
            if (Vector3Properties.ContainsKey("lightPos"))
                Vector3Properties.Remove("lightPos");
            if (Vector3Properties.ContainsKey("diffuseColor"))
                Vector3Properties.Remove("diffuseColor");
            if (Vector3Properties.ContainsKey("specularColor"))
                Vector3Properties.Remove("specularColor");

            Vector3Properties.Add("lightPos", light.Position);
            Vector3Properties.Add("diffuseColor", light.Diffuse);
            Vector3Properties.Add("specularColor", light.Specular);
        }

        

    }
}
