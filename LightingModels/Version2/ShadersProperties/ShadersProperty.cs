using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

// 17.05.2015
namespace Version2
{
    public class ShadersProperty
    {
        public string ShaderName { get; protected set; }

        public Dictionary<string,Vector3> Vector3Properties { get; set; }
        public Dictionary<string,float> FloatProperties { get; set; }

        public List<Tuple<string, ShadersProperty.Type, string>> PropertiesList = new List<Tuple<string, ShadersProperty.Type, string>>();

        public enum Type
        {
            VECTOR3,
            FLOAT,
        }

        public ShadersProperty()
        {
            Vector3Properties = new Dictionary<string,Vector3>();
            FloatProperties = new Dictionary<string,float>();
        }

        public virtual void ChangeLight(Light light)
        {
        }
    }
}
