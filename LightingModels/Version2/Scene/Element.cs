using System;
using OpenGL;
using System.Collections.Generic;

// 11.07.2015
namespace Version2
{
    public class Element
    {
        public string Name;
        public Vector3 Position;
        public Vector3 Rotation;

        //
        public Element(string name)
        {
            Name = name;
        }

        //
        public void SetPosition(Vector3 position)
        {
            Position = position;
        }
    }
}
