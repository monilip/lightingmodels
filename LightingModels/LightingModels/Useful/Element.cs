using System;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.Collections.Generic;

// 11.07.2015
namespace LightingModels.Useful
{
    class Element
    {
        public string Name;
        public Vector3 Position;
        public Vector3 Rotation;

        public Element(string name)
        {
            Name = name;
        }
    }
}
