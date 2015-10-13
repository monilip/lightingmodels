using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

// 17.05.2015
// Class for all Lambert lighting properties
namespace Version2
{
    class LambertProperty : ShadersProperty
    {
        //
        public void Activate(string shaderName = null)
        {
            ShaderName = "Lambert";

            // default light properties
            Vector3Properties.Add("lightPos",new Vector3(1.0f,1.0f,1.0f));
            Vector3Properties.Add("diffuseColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3Properties.Add("ambientColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3Properties.Add("Kd", Vector3.Zero);
            Vector3Properties.Add("Ka", Vector3.Zero);


            PropertiesList = new List<Tuple<string, ShadersProperty.Type, string>>()
            {
                Tuple.Create("Position of light", ShadersProperty.Type.VECTOR3, "lightPos"),
                Tuple.Create("Ambient color", ShadersProperty.Type.VECTOR3,"ambientColor"),
                Tuple.Create("Ka", ShadersProperty.Type.VECTOR3,"Ka"),
                Tuple.Create("Diffuse color", ShadersProperty.Type.VECTOR3,"diffuseColor"),
                Tuple.Create("Kd", ShadersProperty.Type.VECTOR3,"Kd"),
                
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

            Vector3Properties.Add("lightPos", light.Position);
            Vector3Properties.Add("diffuseColor", light.Diffuse);
        }

        

    }
}
