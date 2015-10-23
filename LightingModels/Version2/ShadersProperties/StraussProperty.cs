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

            Vector3Properties.Add("surfColor", new Vector3(0.2f,0.3f,0.2f));            

            FloatProperties.Add("m", 0.3f);
            FloatProperties.Add("s", 0.7f);
            FloatProperties.Add("t", 0.1f);         


            PropertiesList = new List<Tuple<string, ShadersProperty.Type, string>>()
            {
                Tuple.Create("Position of light", ShadersProperty.Type.VECTOR3, "lightPos"),
                Tuple.Create("Color of surface", ShadersProperty.Type.VECTOR3,"surfColor"),
                Tuple.Create("Metalicity", ShadersProperty.Type.FLOAT,"m"),
                Tuple.Create("Smoothness", ShadersProperty.Type.FLOAT,"s"),
                Tuple.Create("Transparency", ShadersProperty.Type.FLOAT,"t"),
            };
        }

        //
        public override void ChangeLight(Light light)
        {
            // light properties

            // delete if light exist
            if (Vector3Properties.ContainsKey("lightPos"))
                Vector3Properties.Remove("lightPos"); 

            Vector3Properties.Add("lightPos", light.Position);
        }

        

    }
}
