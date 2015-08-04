using System;
using System.Collections.Generic;
using System.Linq;
using OpenGL;

// 04.08.2015
namespace Version2
{
    public class Shader
    {
        private String name;
        private OpenGL.ShaderProgram shaderProgram;

        //
        public Shader(string name, OpenGL.ShaderProgram shaderProgram)
        {
            this.name = name;
            this.shaderProgram = shaderProgram;
        }

        //
        public string GetShaderName()
        {
            return name;
        }

        //
        public OpenGL.ShaderProgram GetShaderProgram()
        {
            return shaderProgram;
        }

    }
}
