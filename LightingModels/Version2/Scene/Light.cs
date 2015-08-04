using System;
using OpenGL;
using System.Collections.Generic;

// 11.07.2015
namespace Version2
{
    public class Light : Element
    {
        private int lightNum = 0;

        public static List<Light> Lights = new List<Light>();
        public Vector3 Diffuse = new Vector3(0.8f, 0.8f, 0.8f);
        public Vector3 Specular = new Vector3(0.5f, 0.5f, 0.5f);
        public Vector3 Ambient = new Vector3(0.1f, 0.1f, 0.1f);
        public bool Enabled = true;

        //
        public Light(string name, Vector3 diffuse, Vector3 specular, Vector3 ambient)
            : base(name)
        {
            Diffuse = diffuse;
            Specular = specular;
            Ambient = ambient;
        }

        ////
        public Light(string name)
            : base(name)
        {

        }

    }
}
