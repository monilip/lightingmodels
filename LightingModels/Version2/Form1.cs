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
        public int ActiveObjectIndex = 0;
         
        // objects
       // private static Shader program;
       // private static ObjVolume cube;
       // private static Texture texture;

        
        // Scene
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
            //Light lightRed = new Light("lightRed");
            //lightRed.Position = new Vector3(1.0f, 1.0f, 1.0f);
            //lightRed.Ambient = new Vector3(0.2f, 0.2f, 0.2f);
            //lightRed.Diffuse = new Vector3(0.2f, 0.2f, 0.2f);
            //lightRed.Specular = new Vector3(1.0f, 0.0f, 0.0f);

            Light lightWhite = new Light("lightWhite");
            lightWhite.Position = new Vector3(2.0f, 2.0f, 2.0f);
            lightWhite.Ambient = new Vector3(0.1f, 0.1f, 0.1f);
            lightWhite.Diffuse = new Vector3(0.1f, 0.1f, 0.1f);
            lightWhite.Specular = new Vector3(0.4, 0.4f, 0.4f);
            lightWhite.LightDirection = new Vector3(0, 0, 1);

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
            phong.AddLight(lightWhite);
            phong.ChangeN(4);
            ShadersProperties.Add("phongLight", phong);

            Shaders.Add(new Shader("simpleLight", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_simpleLight.glsl"), System.IO.File.ReadAllText(@"glsl/fs_simpleLight.glsl"))));

            ActiveShaderIndex = 3;

            Shaders[ActiveShaderIndex].GetShaderProgram().Use();
            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));
            if (Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"] != null)
            {
                Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"].SetValue(lightWhite.LightDirection);
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

            ObjVolume objectFromBlender3 = new ObjVolume();
            objectFromBlender3.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "Ball1.obj");
            objectFromBlender3.Name = "Test from Blender";
            objectFromBlender3.Scale = new Vector3(0.7, 0.7, 0.7);
            objectFromBlender3.Position = new Vector3(0, 0, 0);
            Objects.Add(objectFromBlender3);

            //ObjVolume objectFromBlender2 = new ObjVolume();
            //objectFromBlender2.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithSquares.obj");
            //objectFromBlender2.Name = "As light from Blender";
            ////objectFromBlender2.Scale = new Vector3(0.2, 0.2, 0.2);
            //objectFromBlender2.Position = new Vector3(1.0f, 0.0f, 0.0f);
            //Objects.Add(objectFromBlender2);

        }     

        private void OnRenderFrame(object sender, EventArgs e)
        {
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
                Shaders[ActiveShaderIndex].GetShaderProgram()["modelMatrix"].SetValue(volume.CalculateModelMatrix());

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
            
            if (e.KeyCode == Keys.NumPad1)
                ActiveObjectIndex = 0;

            if (e.KeyCode == Keys.NumPad2)
                ActiveObjectIndex = 1;

            if (e.KeyCode == Keys.W)
                RotateObject(Objects[ActiveObjectIndex], -10, 0, 0);

            if (e.KeyCode == Keys.S)
                RotateObject(Objects[ActiveObjectIndex], 10, 0, 0);
            
            if (e.KeyCode == Keys.D) 
                RotateObject(Objects[ActiveObjectIndex],0, 10, 0);

            if (e.KeyCode == Keys.A)
                RotateObject(Objects[ActiveObjectIndex], 0, -10, 0);

        }


        public void RotateObject(Volume obj, float xx, float yy, float zz)
        {
            float x = (float)((double)xx * Math.PI / 180);
            float y = (float)((double)yy * Math.PI / 180);
            float z = (float)((double)zz * Math.PI / 180);

            obj.RotateObject(x, y, z);
        }
    }
}

