using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

// 03.10.2015
// Class for all Strauss lighting properties
namespace Version2
{
    class StraussProperty : ShadersProperty
    {
        //
        public void Activate()
        {
            ShaderName = "Strauss";

            // default light properties
            Vector3Properties.Add("lightPos",new Vector3(1.0f,1.0f,1.0f));
            Vector3Properties.Add("diffuseColor", new Vector3(1.0f, 1.0f, 1.0f));
           // Vector3Properties.Add("specularColor", new Vector3(1.0f, 1.0f, 1.0f));

            FloatProperties.Add("m", 0.1f);
            FloatProperties.Add("s", 0.1f);
            FloatProperties.Add("t", 0.3f);
            PropertiesList = new List<Tuple<string, ShadersProperty.Type, string>>()
            {
                Tuple.Create("Position of light", ShadersProperty.Type.VECTOR3, "lightPos"),
                Tuple.Create("c", ShadersProperty.Type.VECTOR3,"diffuseColor"),
             //   Tuple.Create("Specular color", ShadersProperty.Type.VECTOR3,"specularColor"),
                Tuple.Create("m", ShadersProperty.Type.FLOAT,"m"),
                Tuple.Create("s", ShadersProperty.Type.FLOAT,"s"),
                Tuple.Create("t", ShadersProperty.Type.FLOAT,"t"),
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
          //  if (Vector3Properties.ContainsKey("specularColor"))
          //      Vector3Properties.Remove("specularColor");

            Vector3Properties.Add("lightPos", light.Position);
            Vector3Properties.Add("diffuseColor", light.Diffuse);
        //    Vector3Properties.Add("specularColor", light.Specular);
        }

        

    }
}
