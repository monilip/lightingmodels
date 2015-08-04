using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

// 17.05.2015
// Class for all Phong lighting properties
namespace LightingModels
{
    class PhongProperty : ShadersProperty
    {
        //
        public void Activate()
        {
            ShaderName = "phongLight";

            // default light properties
            Vector3Properties.Add("lightPos",new Vector3(1.0f,1.0f,1.0f));
            Vector3Properties.Add("ambientColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3Properties.Add("diffuseColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3Properties.Add("specularColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3PropertiesCount = 4;

            FloatProperties.Add("n", 4.0f);
            FloatPropertiesCount = 1;
        }

        //
        public void AddLight(Light light)
        {
            // light properties

            // delete if light exist
            if (Vector3Properties.ContainsKey("lightPos"))
                Vector3Properties.Remove("lightPos");
            if (Vector3Properties.ContainsKey("ambientColor"))
                Vector3Properties.Remove("ambientColor");
            if (Vector3Properties.ContainsKey("diffuseColor"))
                Vector3Properties.Remove("diffuseColor");
            if (Vector3Properties.ContainsKey("specularColor"))
                Vector3Properties.Remove("specularColor");

            Vector3Properties.Add("lightPos", light.Position);
            Vector3Properties.Add("ambientColor", light.Ambient);
            Vector3Properties.Add("diffuseColor", light.Diffuse);
            Vector3Properties.Add("specularColor", light.Specular);
        }

        //
        public void ChangeN(float n)
        {
            if (FloatProperties.ContainsKey("n"))
                FloatProperties.Remove("n");
            FloatProperties.Add("n", n);
        }
    }
}
