using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// 07.05.15
// Shader stuff
namespace LightingModels
{
    public class ShaderProgram
    {
        public int Program = -1;
        public int VertShader = -1;
        public int FragShader = -1;

        // Address of the color parameter
        private int _attributeVCol;
        public int AttributeVCol { get { return _attributeVCol; } }
  
        // Address of the position parameter
        private int _attributeVPos;
        public int AttributeVPos { get { return _attributeVPos; } }
  
        // Address of the modelview matrix uniform
        private int _uniformModelView;
        public int UniformModelView { get { return _uniformModelView; } }
                 
        // Address of the Vertex Buffer Object for our position paramete
        private int _vboPosition;
        public int VBOPosition { get { return _vboPosition; } }
  
        // Address of the Vertex Buffer Object for our color parameter
        private int _vboColor;
        public int VBOColor { get { return _vboColor; } }
  
        // Address of the Vertex Buffer Object for our modelview matrix
        private int _vboModelView;
        public int VBOModelView { get { return _vboModelView; } }


        //
        public void InitProgram()
        {
            Program = GL.CreateProgram();

            // Load shaders from file
            string vertShadersPath = @"shaders/vs.glsl";
            string fragShaderPath = @"shaders/fs.glsl";

            Usefull.LoadShader(vertShadersPath, ShaderType.VertexShader, Program, out VertShader);
            Usefull.LoadShader(fragShaderPath, ShaderType.FragmentShader, Program, out FragShader);

            // Linking to shaders to program
            GL.LinkProgram(Program);

            // Add to log if there are probles
            Usefull.Log(GL.GetProgramInfoLog(Program));

            _attributeVPos = GL.GetAttribLocation(Program, "vPosition");
            _attributeVCol = GL.GetAttribLocation(Program, "vColor");
            _uniformModelView = GL.GetUniformLocation(Program, "modelview");

            // Vertex Buffer Object (VBO)
            GL.GenBuffers(1, out _vboPosition);
            GL.GenBuffers(1, out _vboColor);
            GL.GenBuffers(1, out _vboModelView);
        } 
       
    }
}
