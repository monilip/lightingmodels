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


            PropertiesList = new List<Tuple<string, ShadersProperty.Type, string>>()
            {
                Tuple.Create("Position of light", ShadersProperty.Type.VECTOR3, "lightPos"),
                Tuple.Create("Diffuse color", ShadersProperty.Type.VECTOR3,"diffuseColor"),
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
