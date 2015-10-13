using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
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

        //
        private Stopwatch stopWatch = null;
        private bool measureTimeOfRenderFrame = false;
        private int measureTimeFrames = 5;
        private int measureTimeFrameNumber = 0;
        private double measuredTime = 0;
        private bool isSceneInited = false;
        public Form1()
        {
            InitializeComponent();
            SceneWidth = simpleOpenGlControl1.Width;
            SceneHeight = simpleOpenGlControl1.Height;

            simpleOpenGlControl1.InitializeContexts();
            // simpleOpenGlControl1.DestroyContexts();

            Gl.Enable(EnableCap.DepthTest);

            InitScene();

           // Application.Idle += OnRenderFrame;

            // init GUI varriables
            # region GUI
            // add shaders to droplist
            foreach (Shader shader in Shaders)
            {
                shadersList.Items.Add(shader.GetShaderName());
            }

            shadersList.SelectedIndex = 0;

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

            objectsList.SelectedIndex = 0;

            // update object's parameters
            UpdateObjectParameters();
            # endregion

            OnRenderFrame();
        }

        //
        private void UpdateObjectParameters()
        {
            UpdateObjectPositionParameters();

            UpdateObjectRotationParameters();

            UpdateObjectScaleParameters();

            UpdateObjectMaterialParameters(false);

            // update shaders properies
            UpdateShadersPropertiesForms();
            OnRenderFrame();
        }

        private void UpdateObjectMaterialParameters(bool checkIfNew = true)
        {
            if (Objects[ActiveObjectIndex].Material != null)
            {
                //if (Shaders[ActiveShaderIndex].GetShaderProgram()["Ka"] != null)
                //{
                //    if ((ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Ka").x == 0 &&
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Ka").y == 0 &
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Ka").z == 0) || checkIfNew == false)
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].SetVector3Property("Ka", Objects[ActiveObjectIndex].Material.Ka);
                //}

                //if (Shaders[ActiveShaderIndex].GetShaderProgram()["Kd"] != null)
                //{
                //    if ((ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Kd").x == 0 &&
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Kd").y == 0 &
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Kd").z == 0) || checkIfNew == false)
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].SetVector3Property("Kd", Objects[ActiveObjectIndex].Material.Kd);
                //}

                //if (Shaders[ActiveShaderIndex].GetShaderProgram()["Ks"] != null)
                //{
                //    if ((ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Ks").x == 0 &&
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Ks").y == 0 &
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetVector3Property("Ks").z == 0) || checkIfNew == false)
                //        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].SetVector3Property("Ks", Objects[ActiveObjectIndex].Material.Ks);
                //}

                if (Shaders[ActiveShaderIndex].GetShaderProgram()["Ns"] != null)
                {
                    if ((ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].GetFloatProperty("Ns") == 0) || checkIfNew == false)
                        ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].SetFloatProperty("Ns", Objects[ActiveObjectIndex].Material.Ns);
                }

            }
        }

        // 
        private void UpdateObjectPositionParameters()
        {
            // update object's position
            posX.Text = Objects[ActiveObjectIndex].Position.x.ToString();
            posY.Text = Objects[ActiveObjectIndex].Position.y.ToString();
            posZ.Text = Objects[ActiveObjectIndex].Position.z.ToString();
        }

        // 
        private void UpdateObjectRotationParameters()
        {
            // update object's rotation
            double x = Math.Round((double)Objects[ActiveObjectIndex].Rotation.x / Math.PI * 180);
            double y = Math.Round((double)Objects[ActiveObjectIndex].Rotation.y / Math.PI * 180);
            double z = Math.Round((double)Objects[ActiveObjectIndex].Rotation.z / Math.PI * 180);
            rotX.Text = x.ToString();
            rotY.Text = y.ToString();
            rotZ.Text = z.ToString();
        }

        // 
        private void UpdateObjectScaleParameters()
        {
            // update object's scale
            scaleX.Text = Objects[ActiveObjectIndex].Scale.x.ToString();
            scaleY.Text = Objects[ActiveObjectIndex].Scale.y.ToString();
            scaleZ.Text = Objects[ActiveObjectIndex].Scale.z.ToString();
        }

        // create light, shaders and objects
        private void InitScene()
        {
            // Lights
            Light ligthWhite = new Light("ligthWhite");
            ligthWhite.Position = new Vector3(5.0f, 5.0f, 1.0f);
            ligthWhite.Ambient = new Vector3(0.1f, 0.1f, 0.1f);
            ligthWhite.Diffuse = new Vector3(0.2f, 0.3f, 0.2f);
            ligthWhite.Specular = new Vector3(0.7f, 0.7f, 0.7f);
            Lights.Add(ligthWhite);

            Light ligthRed = new Light("ligthRed");
            ligthRed.Position = new Vector3(5.0f, 10.0f, 1.0f);
            ligthRed.Ambient = new Vector3(0.1f, 0.1f, 0.1f);
            ligthRed.Diffuse = new Vector3(0.3f, 0.1f, 0.1f);
            ligthRed.Specular = new Vector3(0.7f, 0.7f, 0.7f); 
            Lights.Add(ligthRed);

            // Shaders
            #region shaders
            // Load shaders from files
            // Default
            //Shaders.Add(new Shader("default", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs.glsl"), System.IO.File.ReadAllText(@"glsl/fs.glsl"))));

            //// With Texture
            //Shaders.Add(new Shader("textured", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_text.glsl"), System.IO.File.ReadAllText(@"glsl/fs_text.glsl"))));

            // Lambert
            Shaders.Add(new Shader("Lambert", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightLambert.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightLambert.glsl"))));
            LambertProperty lambert = new LambertProperty();
            lambert.Activate();
            lambert.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("Lambert", lambert);

            // Phong
            Shaders.Add(new Shader("Phong", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightPhong.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightPhong.glsl"))));
            PhongProperty phong = new PhongProperty();
            phong.Activate();
            phong.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("Phong", phong);

            // Blinn-Phong
            Shaders.Add(new Shader("Blinn-Phong", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightBlinnPhong.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightBlinnPhong.glsl"))));
            PhongProperty blinnPhong = new PhongProperty();
            blinnPhong.Activate("Blinn-Phong");
            blinnPhong.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("Blinn-Phong", blinnPhong);

            // Cook-Torrance
            Shaders.Add(new Shader("Cook-Torrance", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightCookTorrance.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightCookTorrance.glsl"))));
            CookTorranceProperty cookTorrence = new CookTorranceProperty();
            cookTorrence.Activate();
            cookTorrence.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("Cook-Torrance", cookTorrence);

            //Strauss
            Shaders.Add(new Shader("Strauss", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightStrauss.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightStrauss.glsl"))));
            StraussProperty strauss = new StraussProperty();
            strauss.Activate();
            strauss.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("Strauss", strauss);

            // Ward
            //Shaders.Add(new Shader("Ward anizotropic", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightWard.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightWard.glsl"))));
            //WardProperty ward = new WardProperty();
            //ward.Activate();
            //ward.ChangeLight(Lights[ActiveLightIndex]);
            //ShadersProperties.Add("Ward anizotropic", ward);

            // Ward
            Shaders.Add(new Shader("Ward isotropic", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightWard.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightWard2.glsl"))));
            Ward2Property ward2 = new Ward2Property();
            ward2.Activate();
            ward2.ChangeLight(Lights[ActiveLightIndex]);
            ShadersProperties.Add("Ward isotropic", ward2);


            //// Ashikhmin-Shirley
            //Shaders.Add(new Shader("Ashikhmin-Shirley", new ShaderProgram(System.IO.File.ReadAllText(@"glsl/vs_lightAshikhminShirley.glsl"), System.IO.File.ReadAllText(@"glsl/fs_lightAshikhminShirley.glsl"))));
            //AshikhminShirleyProperty ashikhminShirley = new AshikhminShirleyProperty();
            //ashikhminShirley.Activate();
            //ashikhminShirley.ChangeLight(Lights[ActiveLightIndex]);
            //ShadersProperties.Add("Ashikhmin-Shirley", ashikhminShirley);

            #endregion

            ActiveShaderIndex = 0;

            Shaders[ActiveShaderIndex].GetShaderProgram().Use();
            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));

            AddDataFromShadersProperties();


            //  Objects

            #region objects
            ObjVolume blackPlainBall = new ObjVolume();
            blackPlainBall.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "ballPlain.obj");
            blackPlainBall.Name = "Plain ball";
            blackPlainBall.UpdateVolume += UpdateVolume;
            Objects.Add(blackPlainBall);

            ObjVolume ballWithEarth = new ObjVolume();
            ballWithEarth.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "ballWithEarth.obj");
            ballWithEarth.Name = "Ball with Earth";
            ballWithEarth.Rotation = new Vector3(0.5f, 4.5f, 0.1f);
            ballWithEarth.UpdateVolume += UpdateVolume;
            Objects.Add(ballWithEarth);

            ObjVolume cubeWIthSquares = new ObjVolume();
            cubeWIthSquares.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "cubeWithSquares.obj");
            cubeWIthSquares.Name = "Cube with aquares";
            cubeWIthSquares.Rotation = new Vector3(0.3f, 0.3f, 0f);
            cubeWIthSquares.UpdateVolume += UpdateVolume;
            Objects.Add(cubeWIthSquares);

            ObjVolume monkey = new ObjVolume();
            monkey.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "monkey.obj");
            monkey.Name = "Monkey";
            monkey.UpdateVolume += UpdateVolume;
            Objects.Add(monkey);

            //ObjVolume smoothMonkey = new ObjVolume();
            //smoothMonkey.LoadFromFileFromBlenderObj(Useful.GetModelsPath() + "smoothMonkey.obj");
            //smoothMonkey.Name = "Smooth Monkey";
            //smoothMonkey.UpdateVolume += UpdateVolume;
            //Objects.Add(smoothMonkey);

            #endregion


            isSceneInited = true;

        }

        //
        private void AddDataFromShadersProperties()
        {
            if (ShadersProperties.ContainsKey(Shaders[ActiveShaderIndex].GetShaderName()))
            {
                ShadersProperty shaderProperties = ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()];
                foreach (Tuple<string, ShadersProperty.Type, string> property in shaderProperties.PropertiesList)
                {
                    switch (property.Item2)
                    {
                        case ShadersProperty.Type.VECTOR3:
                            if (Shaders[ActiveShaderIndex].GetShaderProgram()[property.Item3] != null)
                            {
                                Shaders[ActiveShaderIndex].GetShaderProgram()[property.Item3].SetValue(shaderProperties.Vector3Properties[property.Item3]);
                            }
                            break;
                        case ShadersProperty.Type.FLOAT:
                            if (Shaders[ActiveShaderIndex].GetShaderProgram()[property.Item3] != null)
                            {
                                Shaders[ActiveShaderIndex].GetShaderProgram()[property.Item3].SetValue(shaderProperties.FloatProperties[property.Item3]);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        //private void OnRenderFrame(object sender, EventArgs e)
        private void OnRenderFrame()
        {
            if (isSceneInited == false)
                return;

            if (measureTimeOfRenderFrame == true)
            { 
                stopWatch = new Stopwatch();
                stopWatch.Start();
                measureTimeFrameNumber++;
            }

            // set up the viewport and clear the previous depth and color buffers
            Gl.Viewport(0, 0, SceneWidth, SceneHeight);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Gl.ClearColor(0.2f, 0.2f, 0.2f, 1);
            // make sure the shader program and texture are being used
            Gl.UseProgram(Shaders[ActiveShaderIndex].GetShaderProgram());


            Shaders[ActiveShaderIndex].GetShaderProgram()["projectionMatrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            Shaders[ActiveShaderIndex].GetShaderProgram()["viewMatrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));


             //foreach (Volume volume in Objects)
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

                // update material parameters
                UpdateObjectMaterialParameters();

                // update shaders properies
                UpdateShadersPropertiesForms();

                //  add data from ShadersProperties      
                AddDataFromShadersProperties();

                Gl.BindBuffer(volume.TrianglesVBO);

                // draw volume
                Gl.DrawElements(BeginMode.Triangles, volume.TrianglesVBO.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);
            }

            simpleOpenGlControl1.SwapBuffers();

            if (measureTimeOfRenderFrame == true)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                measuredTime = ts.TotalMilliseconds;
                if (measureTimeFrameNumber > measureTimeFrames)
                {
                    Useful.Log("Time of scene render: " + (measuredTime / measureTimeFrames));
                    measureTimeOfRenderFrame = false;
                    measureTimeFrameNumber = 0;
                }
            }

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

        //
        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            // rotation control
            if (e.KeyCode == Keys.P)
                RotateObject(Objects[ActiveObjectIndex], 1, 0, 0);

            if (e.KeyCode == Keys.O)
                RotateObject(Objects[ActiveObjectIndex], -1, 0, 0);

            if (e.KeyCode == Keys.K)
                RotateObject(Objects[ActiveObjectIndex], 0, -1, 0);

            if (e.KeyCode == Keys.L)
                RotateObject(Objects[ActiveObjectIndex], 0, 1, 0);

            if (e.KeyCode == Keys.N)
                RotateObject(Objects[ActiveObjectIndex], 0, 0, -1);

            if (e.KeyCode == Keys.M)
                RotateObject(Objects[ActiveObjectIndex], 0, 0, 1);

            // position control
            if (e.KeyCode == Keys.W)
            {
                Objects[ActiveObjectIndex].MoveObject(0, 0.1f, 0);
            }
            if (e.KeyCode == Keys.S)
            { 
                Objects[ActiveObjectIndex].MoveObject(0, -0.1f, 0);
            }
            if (e.KeyCode == Keys.D)
                Objects[ActiveObjectIndex].MoveObject(0.1f, 0, 0);

            if (e.KeyCode == Keys.A)
                Objects[ActiveObjectIndex].MoveObject(-0.1f, 0, 0);

            if (e.KeyCode == Keys.Q)
                Objects[ActiveObjectIndex].MoveObject(0, 0, 0.1f);

            if (e.KeyCode == Keys.E)
                Objects[ActiveObjectIndex].MoveObject(0, 0, -0.1f);

            if (e.KeyCode == Keys.T)
            {
                measureTimeOfRenderFrame = true;
                measureTimeFrameNumber = 0;
                measuredTime = 0;
            }
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
            // update material parameters
            UpdateObjectMaterialParameters();

            // update shaders properies
            UpdateShadersPropertiesForms();


            OnRenderFrame();
        }

        //
        private void lightsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActiveLightIndex = lightsList.SelectedIndex;
            string activeShaderName = Shaders[ActiveShaderIndex].GetShaderName();
            foreach (KeyValuePair<string,ShadersProperty> shader in ShadersProperties)
            {
                shader.Value.ChangeLight(Lights[ActiveLightIndex]);   
            }
            
            UpdateShadersPropertiesForms();

            OnRenderFrame();
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
            switch (((System.Windows.Forms.TextBoxBase)sender).Name)
            {
                case "posX":
                    if (rotX.Text.Length == 0)
                        rotX.Text = "0";
                    Objects[ActiveObjectIndex].Position.x = Useful.GetFloat(posX.Text);
                    break;
                case "posY":
                    if (rotY.Text.Length == 0)
                        rotY.Text = "0";
                    Objects[ActiveObjectIndex].Position.y = Useful.GetFloat(posY.Text);
                    break;
                case "posZ":
                    if (rotZ.Text.Length == 0)
                        rotZ.Text = "0";
                    Objects[ActiveObjectIndex].Position.z = Useful.GetFloat(posZ.Text);
                    break;
                default:
                    break;
            }

            OnRenderFrame();
        }

        //
        private void UpdateObjectRotation(object sender, System.EventArgs e)
        {
            switch (((System.Windows.Forms.TextBoxBase)sender).Name)
            {
                case "rotX":
                    if (rotX.Text.Length == 0)
                        rotX.Text = "0";
                    float x = (float)(Useful.GetFloat(rotX.Text) * Math.PI / 180);
                    Objects[ActiveObjectIndex].Rotation.x = x;
                    break;
                case "rotY":
                    if (rotY.Text.Length == 0)
                        rotY.Text = "0";
                    float y = (float)(Useful.GetFloat(rotY.Text) * Math.PI / 180);
                    Objects[ActiveObjectIndex].Rotation.y = y;
                    break;
                case "rotZ":
                    if (rotZ.Text.Length == 0)
                        rotZ.Text = "0";
                    float z = (float)(Useful.GetFloat(rotZ.Text) * Math.PI / 180);
                    Objects[ActiveObjectIndex].Rotation.z = z;
                    break;
                default:
                    break;
            }

            OnRenderFrame();
        }

        //
        private void UpdateObjectScale(object sender, System.EventArgs e)
        {
            switch (((System.Windows.Forms.TextBoxBase)sender).Name)
            {
                case "scaleX":
                    if (Useful.GetFloat(scaleX.Text) == 0)
                        scaleX.Text = "1";
                    Objects[ActiveObjectIndex].Scale.x = Useful.GetFloat(scaleX.Text);
                    break;
                case "scaleY":
                     if (Useful.GetFloat(scaleY.Text) == 0)
                        scaleY.Text = "1";
                     Objects[ActiveObjectIndex].Scale.y = Useful.GetFloat(scaleY.Text);
                    break;
                case "scaleZ":
                     if (Useful.GetFloat(scaleZ.Text) == 0)
                        scaleZ.Text = "1";
                    Objects[ActiveObjectIndex].Scale.z = Useful.GetFloat(scaleZ.Text);
                    break;
                default:
                    break;
            }

            OnRenderFrame();
        }

        //
        private void UpdateVolume(Volume.UpdateType type)
        {
            switch (type)
            {
                case Volume.UpdateType.POSITION:
                    UpdateObjectPositionParameters();
                    break;
                case Volume.UpdateType.ROTATION:
                    UpdateObjectRotationParameters();
                    break;             
                default:
                    break;
            }

            OnRenderFrame();
        }

        //
        private void UpdateShadersPropertiesForms()
        {
            // clear panel
            shadersPanel.Controls.Clear();

            // add data from ShadersProperties if exist
            if (ShadersProperties.ContainsKey(Shaders[ActiveShaderIndex].GetShaderName()))
            {
                ShadersProperty shaderProperties = ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()];
                int top = 0;
                foreach (Tuple<string,ShadersProperty.Type,string> property in shaderProperties.PropertiesList)
                {
                    Label label = new Label();          

                    switch (property.Item2)
                    {
                        case ShadersProperty.Type.VECTOR3:
                            // add main label
                            label.Text = property.Item1;
                            label.Top = top;
                            label.Left = 0;
                            label.Width = 80;
                            shadersPanel.Controls.Add(label);

                            // add X textBox
                            TextBox xTextBox = new TextBox();
                            xTextBox.Text = shaderProperties.Vector3Properties[property.Item3].x.ToString().Replace(',', '.');
                            xTextBox.Top = top;
                            xTextBox.Name = property.Item1 + "X";
                            xTextBox.Location = new Point(label.Right + 5, top);
                            xTextBox.Width = 30;
                            shadersPanel.Controls.Add(xTextBox);

                            // add Y textBox
                            TextBox yTextBox = new TextBox();
                            yTextBox.Text = shaderProperties.Vector3Properties[property.Item3].y.ToString().Replace(',', '.');
                            yTextBox.Top = top;
                            yTextBox.Name = property.Item1 + "Y";
                            yTextBox.Location = new Point(xTextBox.Right + 5, top);
                            yTextBox.Width = 30;
                            shadersPanel.Controls.Add(yTextBox);

                            // add Z textBox
                            TextBox zTextBox = new TextBox();
                            zTextBox.Text = shaderProperties.Vector3Properties[property.Item3].z.ToString().Replace(',', '.');
                            zTextBox.Top = top;
                            zTextBox.Name = property.Item1 + "Z";
                            zTextBox.Location = new Point(yTextBox.Right + 5, top);
                            zTextBox.Width = 30;
                            shadersPanel.Controls.Add(zTextBox);

                            top += label.Height;
                            break;
                        case ShadersProperty.Type.FLOAT:
                            // add label
                            label.Text = property.Item1;
                            label.Top = top;
                            label.Left = 0;
                            label.Width = 80;
                            shadersPanel.Controls.Add(label);

                            // add textBox
                            TextBox textBox = new TextBox();
                            textBox.Text = shaderProperties.FloatProperties[property.Item3].ToString();
                            textBox.Top = top;
                            textBox.Name = property.Item1;
                            textBox.Location = new Point(label.Right + 5, top);
                            textBox.Width = 100;
                            shadersPanel.Controls.Add(textBox);

                            top += label.Height;
                            break;
                        default:
                            break;
                    }
                }

                // update button
                Button updateButton = new Button();
                updateButton.Text = "Update shader";
                updateButton.Top = top;
                updateButton.Width = 100;
                updateButton.Click += UpdareShaderProperties;
                shadersPanel.Controls.Add(updateButton);
            }
        }

        //
        private void UpdareShaderProperties(object sender, EventArgs e)
        {
            ShadersProperty shaderProperties = ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()];
            Dictionary<string, float> floatProperties = new Dictionary<string, float>();
            Dictionary<string, Vector3> vector3Properties = new Dictionary<string, Vector3>();
            
            foreach (Tuple<string, ShadersProperty.Type, string> property in shaderProperties.PropertiesList)
            {
                switch (property.Item2)
                {
                    case ShadersProperty.Type.VECTOR3:
                        float x = Useful.GetFloat(shadersPanel.Controls.Find(property.Item1 + "X", true)[0].Text);
                        float y = Useful.GetFloat(shadersPanel.Controls.Find(property.Item1 + "Y", true)[0].Text);
                        float z = Useful.GetFloat(shadersPanel.Controls.Find(property.Item1 + "Z", true)[0].Text);

                        vector3Properties.Add(property.Item3, new Vector3(x, y, z));
                        break;
                    case ShadersProperty.Type.FLOAT:
                        float val = Useful.GetFloat(shadersPanel.Controls.Find(property.Item1, true)[0].Text);
                        floatProperties.Add(property.Item3, val);
                        break;
                    default:
                        break;
                }
            }

            ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].FloatProperties = floatProperties;
            ShadersProperties[Shaders[ActiveShaderIndex].GetShaderName()].Vector3Properties = vector3Properties;

            OnRenderFrame();
        }

        private void renderScene_Click(object sender, EventArgs e)
        {
            OnRenderFrame();
        }

        # endregion
    }
}

