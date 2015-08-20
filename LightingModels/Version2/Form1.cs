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
        public List<Light> Lights = new List<Light>();
        public int ActiveShaderIndex = 0;
        public int ActiveObjectIndex = 0;
        public int ActiveLightIndex = 0;

        // Scene
        public int SceneWidth = 500;
        public int SceneHeight = 500;
        private static bool lighting = true;

        public Form1()
        {
            InitializeComponent();
            SceneWidth = simpleOpenGlControl1.Width;
            SceneHeight = simpleOpenGlControl1.Height;

            simpleOpenGlControl1.InitializeContexts();
            // simpleOpenGlControl1.DestroyContexts();

            Gl.Enable(EnableCap.DepthTest);

            InitScene();

            Application.Idle += OnRenderFrame;

            // init GUI varriables
            # region GUI
            // add shaders to droplist
            foreach (Shader shader in Shaders)
            {
                shadersList.Items.Add(shader.GetShaderName());
            }

            shadersList.SelectedIndex = 1;

            // add lights to droplist
            foreach (Light light in Lights)
            {
                lightsList.Items.Add(light.Name);
            }

            lightsList.SelectedIndex = 0;

            // add objects to droplist
            foreach (Volume volume in Objects)
            {
                objectsList.Items.Add(volume.Name);
            }

            objectsList.SelectedIndex = 1;

            // update object's parameters
            // UpdateObjectParameters();

            # endregion
        }

        //
        private void UpdateObjectParameters()
        {
            // update object's position
            //posX.Text = Objects[ActiveObjectIndex].Position.x.ToString();
            //posY.Text = Objects[ActiveObjectIndex].Position.y.ToString();
            //posZ.Text = Objects[ActiveObjectIndex].Position.z.ToString();

            //// update object's rotation

            //double x = Math.Round((double)Objects[ActiveObjectIndex].Rotation.x / Math.PI * 180);
            //double y = Math.Round((double)Objects[ActiveObjectIndex].Rotation.y / Math.PI * 180);
            //double z = Math.Round((double)Objects[ActiveObjectIndex].Rotation.z / Math.PI * 180);
            //rotX.Text = x.ToString();
            //rotY.Text = y.ToString();
            //rotZ.Text = z.ToString();

            //// update object's scale
            //scaleX.Text = Objects[ActiveObjectIndex].Scale.x.ToString();
            //scaleY.Text = Objects[ActiveObjectIndex].Scale.y.ToString();
            //scaleZ.Text = Objects[ActiveObjectIndex].Scale.z.ToString();
        }

        // create light, shaders and objects
        private void InitScene()
        {
            // Lights
            Light ligthWhite = new Light("ligthWhite");
            ligthWhite.Position = new Vector3(5.0f, 5.0f, 1.0f);
            ligthWhite.Ambient = new Vector3(0.1f, 0.1f, 0.1f);
            ligthWhite.Diffuse = new Vector3(0.8f, 0.8f, 0.8f);
            ligthWhite.Specular = new Vector3(1.0f, 1.0f, 1.0f);
            Lights.Add(ligthWhite);

            Light ligthRed = new Light("ligthRed");
            ligthRed.Position = new Vector3(5.0f, 10.0f, 1.0f);
            ligthRed.Ambient = new Vector3(0.1f, 0.1f, 0.1f);
            ligthRed.Diffuse = new Vector3(0.5f, 0.5f, 0.5f);
            ligthRed.Specular = new Vector3(1.0f, 0.1f, 0.1f);
            Lights.Add(ligthRed);

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
            phong.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("phongLight", phong);

            ActiveShaderIndex = 2;

            Shaders[ActiveShaderIndex].GetShaderProgram().Use();
            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));


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
            ObjVolume blackPlainBall = new ObjVolume();
            blackPlainBall.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "ballBlackPlain.obj");
            blackPlainBall.Name = "Plain black ball";
            Objects.Add(blackPlainBall);

            ObjVolume ballWithEarth = new ObjVolume();
            ballWithEarth.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "ballWithEarth.obj");
            ballWithEarth.Name = "Ball with Earth";
            ballWithEarth.Rotation = new Vector3(0.5f, -1.5f, 0.1f);
            Objects.Add(ballWithEarth);

            ObjVolume cubeWIthSquares = new ObjVolume();
            cubeWIthSquares.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithSquares.obj");
            cubeWIthSquares.Name = "Cube with aquares";
            cubeWIthSquares.Rotation = new Vector3(-0.3f, 0.3f, 0f);
            Objects.Add(cubeWIthSquares);
        }

        private void OnRenderFrame(object sender, EventArgs e)
        {
            // set up the viewport and clear the previous depth and color buffers
            Gl.Viewport(0, 0, SceneWidth, SceneHeight);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.ClearColor(0.2f, 0.2f, 0.2f, 1);
            // make sure the shader program and texture are being used
            Gl.UseProgram(Shaders[ActiveShaderIndex].GetShaderProgram());


            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));

            if (Shaders[ActiveShaderIndex].GetShaderProgram()["enableLighting"] != null)
            {
                Shaders[ActiveShaderIndex].GetShaderProgram()["enableLighting"].SetValue(lighting);
            }

            //  add data from ShadersProperties      
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

            // foreach (Volume volume in Objects)
            {
                Volume volume = Objects[ActiveObjectIndex];

                // bind texture
                if (volume.GetTexture() != null)
                {
                    Gl.BindTexture(volume.GetTexture());
                    if (Shaders[ActiveShaderIndex].GetShaderProgram()["isTexture"] != null)
                        Shaders[ActiveShaderIndex].GetShaderProgram()["isTexture"].SetValue(true);
                }
                else
                {
                    if (Shaders[ActiveShaderIndex].GetShaderProgram()["isTexture"] != null)
                        Shaders[ActiveShaderIndex].GetShaderProgram()["isTexture"].SetValue(false);
                }

                // set uniform
                Shaders[ActiveShaderIndex].GetShaderProgram()["modelMatrix"].SetValue(volume.CalculateModelMatrix());

                // set shaders parameters
                Gl.BindBufferToShaderAttribute(volume.ColorsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vColor");
                Gl.BindBufferToShaderAttribute(volume.VertexsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vPosition");
                if (volume.UVsVBO != null)
                    Gl.BindBufferToShaderAttribute(volume.UVsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "texcoord");
                Gl.BindBufferToShaderAttribute(volume.NormalsVBO, Shaders[ActiveShaderIndex].GetShaderProgram(), "vNormal");

                // set uniforms parametrs for material (if volume has them)
                if (volume.Material != null)
                {
                    if (Shaders[ActiveShaderIndex].GetShaderProgram()["Ka"] != null)
                    {
                        Shaders[ActiveShaderIndex].GetShaderProgram()["Ka"].SetValue(volume.Material.Ka);
                    }

                    if (Shaders[ActiveShaderIndex].GetShaderProgram()["Kd"] != null)
                    {
                        Shaders[ActiveShaderIndex].GetShaderProgram()["Kd"].SetValue(volume.Material.Kd);
                    }

                    if (Shaders[ActiveShaderIndex].GetShaderProgram()["Ks"] != null)
                    {
                        Shaders[ActiveShaderIndex].GetShaderProgram()["Ks"].SetValue(volume.Material.Ks);
                    }

                    if (Shaders[ActiveShaderIndex].GetShaderProgram()["Ns"] != null)
                    {
                        Shaders[ActiveShaderIndex].GetShaderProgram()["Ns"].SetValue(volume.Material.Ns);
                    }
                }

                Gl.BindBuffer(volume.TrianglesVBO);

                // draw volume
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
           // Shaders[ActiveShaderIndex].GetShaderProgram().Use();
        }

        //
        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            // shaders control
            //if (e.KeyCode == Keys.D1)
            //    ActiveShaderIndex = 0;

            //if (e.KeyCode == Keys.D2)
            //    ActiveShaderIndex = 1;

            //if (e.KeyCode == Keys.D3)
            //    ActiveShaderIndex = 2;

            // light control
            //if (e.KeyCode == Keys.D0)
            //{
            //    ActiveLightIndex = 0;
            //    string activeShaderName = Shaders[ActiveShaderIndex].GetShaderName();
            //    if (ShadersProperties.ContainsKey(activeShaderName))
            //        ShadersProperties[activeShaderName].ChangeLight(Lights[ActiveLightIndex]);
            //}

            //if (e.KeyCode == Keys.D9)
            //{
            //    ActiveLightIndex = 1;
            //    string activeShaderName = Shaders[ActiveShaderIndex].GetShaderName();
            //    if (ShadersProperties.ContainsKey(activeShaderName))
            //        ShadersProperties[activeShaderName].ChangeLight(Lights[ActiveLightIndex]);
            //}

            // active object control
            //if (e.KeyCode == Keys.NumPad1)
            //    ActiveObjectIndex = 0;

            //if (e.KeyCode == Keys.NumPad2)
            //    ActiveObjectIndex = 1;

            //if (e.KeyCode == Keys.NumPad3)
            //    ActiveObjectIndex = 2;

            // rotation control
            if (e.KeyCode == Keys.P)
                RotateObject(Objects[ActiveObjectIndex], 1, 0, 0);

            if (e.KeyCode == Keys.O)
                RotateObject(Objects[ActiveObjectIndex], -1, 0, 0);

            if (e.KeyCode == Keys.K)
                RotateObject(Objects[ActiveObjectIndex], 0, 1, 0);

            if (e.KeyCode == Keys.L)
                RotateObject(Objects[ActiveObjectIndex], 0, -1, 0);

            if (e.KeyCode == Keys.N)
                RotateObject(Objects[ActiveObjectIndex], 0, 0, -1);

            if (e.KeyCode == Keys.M)
                RotateObject(Objects[ActiveObjectIndex], 0, 0, 1);

            // position control
            if (e.KeyCode == Keys.W)
                Objects[ActiveObjectIndex].MoveObject(0, 0.1f, 0);

            if (e.KeyCode == Keys.S)
                Objects[ActiveObjectIndex].MoveObject(0, -0.1f, 0);

            if (e.KeyCode == Keys.D)
                Objects[ActiveObjectIndex].MoveObject(0.1f, 0, 0);

            if (e.KeyCode == Keys.A)
                Objects[ActiveObjectIndex].MoveObject(-0.1f, 0, 0);

            if (e.KeyCode == Keys.Q)
                Objects[ActiveObjectIndex].MoveObject(0, 0, 0.1f);

            if (e.KeyCode == Keys.E)
                Objects[ActiveObjectIndex].MoveObject(0, 0, -0.1f);
        }

        //
        public void RotateObject(Volume obj, float xx, float yy, float zz)
        {
            float x = (float)((double)xx * Math.PI / 180);
            float y = (float)((double)yy * Math.PI / 180);
            float z = (float)((double)zz * Math.PI / 180);

            obj.RotateObjectAxis(x, y, z);
        }


        // GUI functions
        #region GUI
        private void shadersList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveShaderIndex = shadersList.SelectedIndex;
        }

        //
        private void lightsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveLightIndex = lightsList.SelectedIndex;
            string activeShaderName = Shaders[ActiveShaderIndex].GetShaderName();
            if (ShadersProperties.ContainsKey(activeShaderName))
                ShadersProperties[activeShaderName].ChangeLight(Lights[ActiveLightIndex]);
        }

        //
        private void objectsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveObjectIndex = objectsList.SelectedIndex;
            UpdateObjectParameters();
        }

        //
        private void UpdateObjectPosition(object sender, System.EventArgs e)
        {
            // Objects[ActiveObjectIndex].Position = new Vector3(Useful.GetFloat(posX.Text),Useful.GetFloat(posY.Text),Useful.GetFloat(posZ.Text));
        }

        //
        private void UpdateObjectRotation(object sender, System.EventArgs e)
        {
            //if (rotX.Text.Length > 0 && rotY.Text.Length > 0 && rotZ.Text.Length > 0)
            //{
            //    float x = (float)(Useful.GetFloat(rotX.Text) * Math.PI / 180);
            //    float y = (float)(Useful.GetFloat(rotY.Text) * Math.PI / 180);
            //    float z = (float)(Useful.GetFloat(rotZ.Text) * Math.PI / 180);

            //    Objects[ActiveObjectIndex].Rotation = new Vector3(x, y, z);
            //}

        }

        //
        private void UpdateObjectScale(object sender, System.EventArgs e)
        {
            //Objects[ActiveObjectIndex].Scale = new Vector3(Useful.GetFloat(scaleX.Text), Useful.GetFloat(scaleY.Text), Useful.GetFloat(scaleZ.Text));
        }

        # endregion
    }
}

