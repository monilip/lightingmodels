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

        public void SetVector3Property(string propertyName, Vector3 vec3)
        {
            if (Vector3Properties.ContainsKey(propertyName))
                Vector3Properties.Remove(propertyName);

            Vector3Properties.Add(propertyName, vec3);
        }

        public Vector3 GetVector3Property(string vec3Name)
        {
            if (Vector3Properties.ContainsKey(vec3Name))
                return Vector3Properties[vec3Name];
            else
                return Vector3.Zero;
        }

        public void SetFloatProperty(string propertyName, float f)
        {
            if (FloatProperties.ContainsKey(propertyName))
                FloatProperties.Remove(propertyName);

            FloatProperties.Add(propertyName, f);
        }

        public float GetFloatProperty(string floatName)
        {
            if (FloatProperties.ContainsKey(floatName))
                return FloatProperties[floatName];
            else 
                return 0.0f;
        }
    }
}
