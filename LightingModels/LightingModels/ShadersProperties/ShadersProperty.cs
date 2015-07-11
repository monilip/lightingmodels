using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;

// 17.05.2015
namespace LightingModels
{
    public class ShadersProperty
    {
        public string ShaderName { get; protected set; }

        public int Vector3PropertiesCount { get; protected set; }
        public Dictionary<string,Vector3> Vector3Properties { get; set; }

        public int FloatPropertiesCount { get; protected set; }
        public Dictionary<string,float> FloatProperties { get; set; }

        public ShadersProperty()
        {
            Vector3PropertiesCount = 0;
            Vector3Properties = new Dictionary<string,Vector3>();

            FloatPropertiesCount = 0;
            FloatProperties = new Dictionary<string,float>();
        }
    }
}
