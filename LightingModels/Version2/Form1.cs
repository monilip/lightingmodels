using System;
using System.Drawing;
using System.Windows.Forms;
using OpenGL;
using System.Collections.Generic;

// 04.08.2015
namespace Version2
{
    public partial class Form1 : Form
    {
        public List<Volume> Objects = new List<Volume>();
        public Dictionary<string, ShadersProperty> ShadersProperties = new Dictionary<string, ShadersProperty>();
        public Dictionary<string, ShaderProgram> Shaders = new Dictionary<string, ShaderProgram>();
        public string ActiveShader = "default";

        // objects
        private static ShaderProgram program;
       // private static ObjVolume cube;
       // private static Texture texture;

        
        // Scene
        private static bool left, right, up, down;
        private static float xangle = 0f, yangle = 0f;
        public int SceneWidth = 640;
        public int SceneHeight = 360;
        private static bool lighting = true;

         
        public Form1()
        {
            InitializeComponent();

            simpleOpenGlControl1.InitializeContexts();
           // simpleOpenGlControl1.DestroyContexts();

            Gl.Enable(EnableCap.DepthTest);


            InitScene();

            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));

            program["light_direction"].SetValue(new Vector3(0, 0, 1));
            program["enable_lighting"].SetValue(lighting);

         //   texture = new Texture("2.png");

            //cube = new ObjVolume();
            //cube.LoadFromFileFromBlenderObj(modelsPath + "cubeWithTexture.obj");
            //cube.Name = "Cube from Blender";
            //cube.Position = new Vector3(0, 0, 0);

            Application.Idle += OnRenderFrame;
        }

        // create light, shaders and objects
        private void InitScene()
        {
            // Lights
            Light lightRed = new Light("lightRed");
            lightRed.Position = new Vector3(1.0f, 1.0f, 1.0f);
            lightRed.Ambient = new Vector3(0.3f, 0.0f, 0.0f);
            lightRed.Diffuse = new Vector3(0.5f, 0.0f, 0.0f);
            lightRed.Specular = new Vector3(1.0f, 1.0f, 1.0f);

            // Shaders
            // Load shaders from files
            // Default
            Shaders.Add("default", new ShaderProgram("glsl/vs.glsl", "glsl/fs.glsl"));

            // With Texture
            Shaders.Add("textured", new ShaderProgram("glsl/vs_text.glsl", "glsl/fs_text.glsl"));

            // Phong Lighting
            Shaders.Add("phongLight", new ShaderProgram("glsl/vs_lightPhong.glsl", "glsl/fs_lightPhong.glsl"));
            PhongProperty phong = new PhongProperty();
            phong.Activate();
            phong.AddLight(lightRed);
            ShadersProperties.Add("phongLight", phong);   
        
            // Objects
            ObjVolume objectFromBlender = new ObjVolume();
            objectFromBlender.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithTexture.obj");
            objectFromBlender.Name = "Cube from Blender";
            objectFromBlender.Scale = new Vector3(1, 1, 1);
            objectFromBlender.Position = new Vector3(1, 0, 0);

            Objects.Add(objectFromBlender);

            ObjVolume objectFromBlender2 = new ObjVolume();
            objectFromBlender2.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithTexture.obj");
            objectFromBlender2.Name = "Smaller ube from Blender";
            objectFromBlender2.Scale = new Vector3(0.5, 0.5, 0.5);
            objectFromBlender2.Position = new Vector3(-3,0,0);
            Objects.Add(objectFromBlender2);

        }     

        private void OnRenderFrame(object sender, EventArgs e)
        {

            if (right) yangle += 0.05f;
            if (left) yangle -= 0.05f;
            if (up) xangle -= 0.05f;
            if (down) xangle += 0.05f;

            // set up the viewport and clear the previous depth and color buffers
            Gl.Viewport(0, 0, SceneWidth, SceneHeight);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

             // make sure the shader program and texture are being used
            Gl.UseProgram(program);         
            

            foreach (var volume in Objects)
            {
                // set up the model matrix and draw the cube
                program["model_matrix"].SetValue(Matrix4.CreateRotationY(yangle) * Matrix4.CreateRotationX(xangle) * Matrix4.CreateTranslation(volume.Position) * Matrix4.CreateScaling(volume.Scale));
                program["enable_lighting"].SetValue(lighting);

                Gl.BindTexture(volume.GetTexture());

                Gl.BindBufferToShaderAttribute(volume.VertexsVBO, program, "vertexPosition");
                Gl.BindBufferToShaderAttribute(volume.NormalsVBO, program, "vertexNormal");
                Gl.BindBufferToShaderAttribute(volume.UVsVBO, program, "vertexUV");

                Gl.BindBuffer(volume.TrianglesVBO);

                Gl.DrawElements(BeginMode.Triangles, volume.TrianglesVBO.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }

           


            simpleOpenGlControl1.SwapBuffers();
        }


        // used instead of glutDisplayFunc
        private void OnPaint(object sender, PaintEventArgs e)
        {
            // nothing for purpouse!
        }

        // used instead of glutReshapeFunc
        private void OnResize(object sender, EventArgs e)
        {

        }
        #region shaders
        public static string VertexShader = @"
#version 130

in vec3 vertexPosition;
in vec3 vertexNormal;
in vec2 vertexUV;

out vec3 normal;
out vec2 uv;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

void main(void)
{
    normal = normalize((model_matrix * vec4(floor(vertexNormal), 0)).xyz);
    uv = vertexUV;

    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";

        public static string FragmentShader = @"
#version 130

uniform sampler2D texture;
uniform vec3 light_direction;
uniform bool enable_lighting;

in vec3 normal;
in vec2 uv;

out vec4 fragment;

void main(void)
{
    float diffuse = max(dot(normal, light_direction), 0);
    float ambient = 0.3;
    float lighting = (enable_lighting ? max(diffuse, ambient) : 1);

    fragment = lighting * texture2D(texture, uv);
}
";
        #endregion shaders

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) 
                up = true;
            else 
                up = false;

            if (e.KeyCode == Keys.S)
                down = true;
            else
                down = false;
            
            if (e.KeyCode == Keys.D) 
                right = true;
            else
                right = false;

            if (e.KeyCode == Keys.A)
                left = true;
            else
                left = false;
        }




    }
}

