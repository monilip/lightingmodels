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
        public List<Shader> Shaders = new List<Shader>();
        public int ActiveShaderIndex = 0;

        // objects
       // private static Shader program;
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

            Application.Idle += OnRenderFrame;
        }

        // create light, shaders and objects
        private void InitScene()
        {
            // Lights
            Light lightRed = new Light("lightRed");
            lightRed.Position = new Vector3(1.0f, 1.0f, 1.0f);
            lightRed.Ambient = new Vector3(0.0f, 0.0f, 0.0f);
            lightRed.Diffuse = new Vector3(0.0f, 0.0f, 0.0f);
            lightRed.Specular = new Vector3(1.0f, 0.0f, 0.0f);

            // Shaders
            // Load shaders from files

            // Default
            Shaders.Add(new Shader("default", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs.glsl"), System.IO.File.ReadAllText(@"glsl/fs.glsl"))));

            // With Texture
            Shaders.Add(new Shader("textured", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_text.glsl"), System.IO.File.ReadAllText(@"glsl/fs_text.glsl"))));

            // Phong Lighting
            Shaders.Add(new Shader("phongLight", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightPhong.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightPhong.glsl"))));
            PhongProperty phong = new PhongProperty();
            phong.Activate();
            phong.AddLight(lightRed);
            phong.ChangeN(100);
            ShadersProperties.Add("phongLight", phong);

            Shaders.Add(new Shader("simpleLight", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_simpleLight.glsl"), System.IO.File.ReadAllText(@"glsl/fs_simpleLight.glsl"))));

            ActiveShaderIndex = 2;

            Shaders[ActiveShaderIndex].GetShaderProgram().Use();
            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));
            if (Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"] != null)
            {
                Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"].SetValue(new Vector3(0, 0, 1));
            }

            if (Shaders[ActiveShaderIndex].GetShaderProgram()["enableLighting"] != null)
            {
                Shaders[ActiveShaderIndex].GetShaderProgram()["enableLighting"].SetValue(lighting);
            }

            // add data from ShadersProperties         
            if (ShadersProperties.ContainsKey(Shaders[ActiveShaderIndex].GetShaderName()) && ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].Vector3PropertiesCount > 0)
            {
                foreach (KeyValuePair<string, Vector3> property in ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].Vector3Properties)
                {
                    if (Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key] != null)
                    {
                        Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key].SetValue(property.Value);
                    } 
                }
            }
            if (ShadersProperties.ContainsKey(Shaders[ActiveShaderIndex].GetShaderName()) && ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].FloatPropertiesCount > 0)
            {
                foreach (KeyValuePair<string, float> property in ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].FloatProperties)
                {
                    if (Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key] != null)
                    {
                        Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key].SetValue(property.Value);
                    } 
                }
            }

            // Objects
            //ObjVolume objectFromBlender = new ObjVolume();
            //objectFromBlender.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithTexture.obj");
            //objectFromBlender.Name = "Cube from Blender";
            //objectFromBlender.Scale = new Vector3(1, 1, 1);
            //objectFromBlender.Position = new Vector3(1, 0, 0);

            //Objects.Add(objectFromBlender);

            ObjVolume objectFromBlender2 = new ObjVolume();
            objectFromBlender2.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "ballWithTexture.obj");
            objectFromBlender2.Name = "Ball from Blender";
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
            Gl.UseProgram(Shaders[ActiveShaderIndex].GetShaderProgram());  
            
            foreach (var volume in Objects)
            {
                // set shaders parameters
                // default
                // texture
                // in
                Gl.BindBufferToShaderAttribute(volume.ColorsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vColor");
                Gl.BindBufferToShaderAttribute(volume.VertexsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vPosition");
                Gl.BindBufferToShaderAttribute(volume.UVsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "texcoord");

                Gl.BindBufferToShaderAttribute(volume.NormalsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vNormal");


                // uniform
                Shaders[ActiveShaderIndex].GetShaderProgram()["modelMatrix"].SetValue(Matrix4.CreateRotationY(yangle / 2) * Matrix4.CreateRotationX(xangle) * Matrix4.CreateTranslation(volume.Position) * Matrix4.CreateScaling(volume.Scale));



                // set up the model matrix and draw the cube
               //  Shaders[ActiveShaderIndex].GetShaderProgram()["enable_lighting"].SetValue(lighting);

                Gl.BindTexture(volume.GetTexture());

         
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
            Shaders[ActiveShaderIndex].GetShaderProgram().Use();
        }
       

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

