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
        public Vector3 LightDirection;
        public Vector3 LightPosition;
        // objects
       // private static Shader program;
       // private static ObjVolume cube;
       // private static Texture texture;

        
        // Scene
        public int SceneWidth = 900;
        public int SceneHeight = 500;
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


            Light lightRed = new Light("lightRed");
            lightRed.Position = new Vector3(-2.0f, 0.0f, 0.0f);
            lightRed.Ambient = new Vector3(0.1f, 0.1f, 0.1f);
            lightRed.Diffuse = new Vector3(1.0f, 0.1f, 0.1f);
            lightRed.Specular = new Vector3(1.0, 0.0f, 0.0f);
            lightRed.LightDirection = new Vector3(0, 0, 0);
            LightDirection = lightRed.LightDirection;
            LightPosition = lightRed.Position;
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
            phong.ChangeN(4);
            ShadersProperties.Add("phongLight", phong);

            Shaders.Add(new Shader("simpleLight", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_simpleLight.glsl"), System.IO.File.ReadAllText(@"glsl/fs_simpleLight.glsl"))));

            ActiveShaderIndex = 2;

            Shaders[ActiveShaderIndex].GetShaderProgram().Use();
            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));
            //if (Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"] != null)
            //{
            //    Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"].SetValue(LightDirection);
            //}

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
            objectFromBlender3.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "ballWithEarth.obj");
            objectFromBlender3.Name = "Test from Blender";
            objectFromBlender3.Scale = new Vector3(1, 1, 1);
            objectFromBlender3.Position = new Vector3(1, 0, 0);
            objectFromBlender3.Texture = new Texture("2.png");
            Objects.Add(objectFromBlender3);

            ObjVolume lightObj1 = new ObjVolume();
            lightObj1.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithSquares.obj");
            lightObj1.Name = "As light from Blender";
            lightObj1.Scale = new Vector3(0.2, 0.2, 0.2);
            lightObj1.Position = LightPosition;
            Objects.Add(lightObj1);
        }     

        private void OnRenderFrame(object sender, EventArgs e)
        {
            // set up the viewport and clear the previous depth and color buffers
            Gl.Viewport(0, 0, SceneWidth, SceneHeight);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

             // make sure the shader program and texture are being used
            Gl.UseProgram(Shaders[ActiveShaderIndex].GetShaderProgram());

            // add data from ShadersProperties      
            //if (ShadersProperties.ContainsKey(Shaders[ActiveShaderIndex].GetShaderName()) && ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].Vector3PropertiesCount > 0)
            //{
            //    foreach (KeyValuePair<string, Vector3> property in ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].Vector3Properties)
            //    {
            //        if (Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key] != null)
            //        {
            //            Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key].SetValue(property.Value);
            //        }
            //    }
            //}
            //if (ShadersProperties.ContainsKey(Shaders[ActiveShaderIndex].GetShaderName()) && ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].FloatPropertiesCount > 0)
            //{
            //    foreach (KeyValuePair<string, float> property in ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].FloatProperties)
            //    {
            //        if (Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key] != null)
            //        {
            //            Shaders[ActiveShaderIndex].GetShaderProgram()[property.Key].SetValue(property.Value);
            //        }
            //    }
            //}    

            //if (Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"] != null)
            //{
            //    Shaders[ActiveShaderIndex].GetShaderProgram()["lightDirection"].SetValue(LightDirection);
            //}

            //if (Shaders[ActiveShaderIndex].GetShaderProgram()["lightPos"] != null)
            //{
            //    Shaders[ActiveShaderIndex].GetShaderProgram()["lightPos"].SetValue(LightPosition);
            //} 

            foreach (var volume in Objects)
            {
                Gl.BindTexture(volume.GetTexture());
                // uniform
                Shaders[ActiveShaderIndex].GetShaderProgram()["modelMatrix"].SetValue(volume.CalculateModelMatrix());


                // set shaders parameters
                // default
                // texture
                // i               
                Gl.BindBufferToShaderAttribute(volume.ColorsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vColor");
                Gl.BindBufferToShaderAttribute(volume.VertexsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vPosition");
                if (volume.UVsVBO != null)
                    Gl.BindBufferToShaderAttribute(volume.UVsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "texcoord");
                Gl.BindBufferToShaderAttribute(volume.NormalsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vNormal");
                        
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
            //if (e.KeyCode == Keys.NumPad1)
            //    ActiveObjectIndex = 0;

            //if (e.KeyCode == Keys.NumPad2)
            //    ActiveObjectIndex = 1;

            if (e.KeyCode == Keys.W)
                //RotateObject(Objects[ActiveObjectIndex], -10, 0, 0);
                Objects[ActiveObjectIndex].MoveObject(0, 0.1f, 0);                

            if (e.KeyCode == Keys.S)
              //  RotateObject(Objects[ActiveObjectIndex], 10, 0, 0);
                Objects[ActiveObjectIndex].MoveObject(0, -0.1f, 0);

            if (e.KeyCode == Keys.D) 
               // RotateObject(Objects[ActiveObjectIndex],0, 10, 0);                
                Objects[ActiveObjectIndex].MoveObject(0.1f, 0, 0);

            if (e.KeyCode == Keys.A)
               // RotateObject(Objects[ActiveObjectIndex], 0, -10, 0);
                Objects[ActiveObjectIndex].MoveObject(-0.1f, 0, 0);

            if (e.KeyCode == Keys.Q)
                Objects[ActiveObjectIndex].MoveObject(0, 0, 0.1f);

            if (e.KeyCode == Keys.E)
                Objects[ActiveObjectIndex].MoveObject(0, 0, -0.1f);

            if (e.KeyCode == Keys.M)
            {
                Objects[1].MoveObject(-0.1f, 0, 0);
              //  Objects[2].MoveObject(-0.1f, 0, 0);
                LightPosition.x -= 0.1f;
            }
            if (e.KeyCode == Keys.N)
            {
                LightPosition.y -= 0.1f;
                Objects[1].MoveObject(0, -0.1f, 0);
             //   Objects[2].MoveObject(0, -0.1f, 0);
            }
            if (e.KeyCode == Keys.B)
            { 
                LightPosition.z -= 0.1f;
                Objects[1].MoveObject(0, 0, -0.1f);
             //   Objects[2].MoveObject(0, 0, -0.1f);
            }
            if (e.KeyCode == Keys.L)
            {
                LightPosition.x += 0.1f;
                Objects[1].MoveObject(+0.1f, 0, 0);
               // Objects[2].MoveObject(+0.1f, 0, 0);
            }
            if (e.KeyCode == Keys.K)
            { 
                LightPosition.y += 0.1f;
                Objects[1].MoveObject(0, +0.1f, 0);
              //  Objects[2].MoveObject(0, +0.1f, 0);
            }
            if (e.KeyCode == Keys.J)
            { 
                LightPosition.z += 0.1f;
                Objects[1].MoveObject(0, 0, +0.1f);
              //  Objects[2].MoveObject(0, 0, +0.1f);
            }

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

