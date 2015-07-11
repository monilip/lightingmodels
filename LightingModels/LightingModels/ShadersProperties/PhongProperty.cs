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
        public void Activate()
        {
            ShaderName = "phongLight";

            // add vec3 properties
            Vector3Properties.Add("lightPos",new Vector3(1.0f,1.0f,1.0f));
            Vector3Properties.Add("ambientColor", new Vector3(0.3f, 0.0f, 0.0f));
            Vector3Properties.Add("diffuseColor", new Vector3(0.5f, 0.0f, 0.0f));
            Vector3Properties.Add("specularColor", new Vector3(1.0f, 1.0f, 1.0f));
            Vector3PropertiesCount = 4;

            FloatProperties.Add("n", 4.0f);
            FloatPropertiesCount = 1;
        }
    }
}
