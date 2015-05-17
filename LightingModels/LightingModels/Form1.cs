using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Globalization; // for parsing float from string with ".", not ","

// 01.05.15
namespace LightingModels
{
    public partial class Form1 : Form
    {
        Scene scene;
        int currentObjectIndex; // objects that answers to buttons control
        // 
        private bool openGLLoaded = false; // checking if openGL is already loaded

        //
        public Form1()
        {
            InitializeComponent();
            
        }

        //
        private void OnLoad(object sender, EventArgs e)
        {
            openGLLoaded = true;
            scene = new Scene(glControlMain.Width, glControlMain.Height);

           
            scene.OnLoad();
                       
            // add obejcts from scene to droplist
            foreach (Volume entry in scene.Objects)
            {
                objectsList.Items.Add(entry.Name);
            }
            objectsList.SelectedIndex = 0;
            currentObjectIndex = objectsList.SelectedIndex;


            // add shaders from scene to droplist
            foreach (KeyValuePair<string, ShaderProgram> entry in scene.Shaders)
            {
                shadersList.Items.Add(entry.Key);
            }

            shadersList.SelectedIndex = 0;

            scene.OnUpdateFrame();
        }

        private void OnResize(object sender, EventArgs e)
        {
            if (!openGLLoaded)
                return;
        }
       
        //
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (!openGLLoaded) 
                return;

            OnSceneUpdate();
        }

        //
        private void OnSceneUpdate()
        {
            scene.OnUpdateFrame();
            scene.OnRenderFrame();

            glControlMain.SwapBuffers();
        }

        // GUI functions
        # region Camera GUI
        //
        private void camMoveLeft_Click(object sender, EventArgs e)
        {
            scene.Camera.Move(-10, 0, 0);
            OnSceneUpdate();
        }

        //
        private void camMoveRight_Click(object sender, EventArgs e)
        {
            scene.Camera.Move(10, 0, 0);
            OnSceneUpdate();
        }

        //
        private void camMoveUp_Click(object sender, EventArgs e)
        {
            scene.Camera.Move(0, 0, 10);
            OnSceneUpdate();
        }

        //
        private void camMoveDown_Click(object sender, EventArgs e)
        {
            scene.Camera.Move(0, 0, -10);
            OnSceneUpdate();
        }

        //
        private void camMoveCloser_Click(object sender, EventArgs e)
        {
            scene.Camera.Move(0, 10, 0);
            OnSceneUpdate();
        }

        //
        private void camMoveFurther_Click(object sender, EventArgs e)
        {
            scene.Camera.Move(0, -10, 0);
            OnSceneUpdate();
        }     

        #endregion

        #region Object GUI
        //
        private void objRotateLeft_Click(object sender, EventArgs e)
        {
            scene.RotateObject(currentObjectIndex, 0, -10, 0);
            OnSceneUpdate();
        }

        //
        private void objRotateRight_Click(object sender, EventArgs e)
        {
            scene.RotateObject(currentObjectIndex, 0, 10, 0);
            OnSceneUpdate();
        }

        //
        private void objRotateUp_Click(object sender, EventArgs e)
        {
            scene.RotateObject(currentObjectIndex, -10, 0, 0);
            OnSceneUpdate();
        }

        //
        private void objRotateDown_Click(object sender, EventArgs e)
        {
            scene.RotateObject(currentObjectIndex, 10, 0, 0);
            OnSceneUpdate();
        }

        private void objMoveLeft_Click(object sender, EventArgs e)
        {
            scene.MoveObject(currentObjectIndex , -1, 0, 0);
            OnSceneUpdate();
        }

        private void objMoveRight_Click(object sender, EventArgs e)
        {
            scene.MoveObject(currentObjectIndex, 1, 0, 0);
            OnSceneUpdate();
        }

        private void objMoveUp_Click(object sender, EventArgs e)
        {
            scene.MoveObject(currentObjectIndex, 0, 1, 0);
            OnSceneUpdate();
        }

        private void objMoveDown_Click(object sender, EventArgs e)
        {
            scene.MoveObject(currentObjectIndex, 0, -1, 0);
            OnSceneUpdate();
        }

        private void objMoveCloser_Click(object sender, EventArgs e)
        {
            scene.MoveObject(currentObjectIndex, 0, 0, 1);
            OnSceneUpdate();
        }

        private void objMoveFurther_Click(object sender, EventArgs e)
        {
            scene.MoveObject(currentObjectIndex, 0, 0, -1);
            OnSceneUpdate();
        }

        private void objectsList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            currentObjectIndex = objectsList.SelectedIndex;
            OnSceneUpdate();
        }
        
        # endregion

        # region Shader GUI

        private void shadersList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            scene.ActiveShader = shadersList.SelectedItem.ToString();
            UpdateShadersPropertiesForms();
            OnSceneUpdate();
        }

        private void UpdateShadersPropertiesForms()
        {
            // clear panel
            shaderPanel.Controls.Clear();

            // add data from ShadersProperties if exist
            if (scene.ShadersProperties.ContainsKey(scene.ActiveShader))
            {
                int top = 0;
                if (scene.ShadersProperties[scene.ActiveShader].Vector3PropertiesCount > 0)
                {
                    foreach (KeyValuePair<string, Vector3> property in scene.ShadersProperties[scene.ActiveShader].Vector3Properties)
                    {
                        // add main label
                        Label label = new Label();
                        label.Text = property.Key;
                        label.Top = top;
                        label.Left = 0;
                        label.Width = 80;
                        shaderPanel.Controls.Add(label);

                        // add X textBox
                        TextBox xTextBox = new TextBox();
                        xTextBox.Text = (property.Value.X.ToString()).Replace(',','.');
                        xTextBox.Top = top;
                        xTextBox.Name = property.Key + "X";
                        xTextBox.Location = new Point(label.Right + 5, top);
                        xTextBox.Width = 30;
                        shaderPanel.Controls.Add(xTextBox);

                        // add Y textBox
                        TextBox yTextBox = new TextBox();
                        yTextBox.Text = (property.Value.Y.ToString()).Replace(',', '.');
                        yTextBox.Top = top;
                        yTextBox.Name = property.Key + "Y";
                        yTextBox.Location = new Point(xTextBox.Right + 5, top);
                        yTextBox.Width = 30;
                        shaderPanel.Controls.Add(yTextBox);

                        // add Z textBox
                        TextBox zTextBox = new TextBox();
                        zTextBox.Text = (property.Value.Z.ToString()).Replace(',', '.');
                        zTextBox.Top = top;
                        zTextBox.Name = property.Key + "Z";
                        zTextBox.Location = new Point(yTextBox.Right + 5, top);
                        zTextBox.Width = 30;
                        shaderPanel.Controls.Add(zTextBox);

                        top += label.Height;
                    }
                }
                if (scene.ShadersProperties.ContainsKey(scene.ActiveShader) && scene.ShadersProperties[scene.ActiveShader].FloatPropertiesCount > 0)
                {
                    foreach (KeyValuePair<string, float> property in scene.ShadersProperties[scene.ActiveShader].FloatProperties)
                    {
                        // add label
                        Label label = new Label();
                        label.Text = property.Key;
                        label.Top = top;
                        label.Left = 0;
                        label.Width = 80;
                        shaderPanel.Controls.Add(label);

                        // add textBox
                        TextBox textBox = new TextBox();
                        textBox.Text = property.Value.ToString();
                        textBox.Top = top;
                        textBox.Name = property.Key;
                        textBox.Location = new Point(label.Right + 5, top);
                        textBox.Width = 30;
                        shaderPanel.Controls.Add(textBox);

                        top += label.Height;
                    }
                }

                // update button
                Button updateButton = new Button();
                updateButton.Text = "Update shader";
                updateButton.Top = top;
                updateButton.Width = 100;
                updateButton.Click += UpdareShaderProperties;
                shaderPanel.Controls.Add(updateButton);
            }
        }

        private void UpdareShaderProperties(object sender, EventArgs e)
        {
            // update float properties
            Dictionary<string, float> floatProperties = new Dictionary<string, float>();
            foreach (KeyValuePair<string,float> property in scene.ShadersProperties[scene.ActiveShader].FloatProperties)
            {
                float val = float.Parse(shaderPanel.Controls.Find(property.Key, true)[0].Text, CultureInfo.InvariantCulture.NumberFormat);
                floatProperties.Add(property.Key, val);
            }
            scene.ShadersProperties[scene.ActiveShader].FloatProperties = floatProperties;

            // update vec3 properties
            Dictionary<string, Vector3> vector3Properties = new Dictionary<string, Vector3>();
            foreach (KeyValuePair<string, Vector3> property in scene.ShadersProperties[scene.ActiveShader].Vector3Properties)
            {
                float x = float.Parse(shaderPanel.Controls.Find(property.Key+"X", true)[0].Text, CultureInfo.InvariantCulture.NumberFormat);
                float y = float.Parse(shaderPanel.Controls.Find(property.Key+"Y", true)[0].Text, CultureInfo.InvariantCulture.NumberFormat);
                float z = float.Parse(shaderPanel.Controls.Find(property.Key+"Z", true)[0].Text, CultureInfo.InvariantCulture.NumberFormat);

                vector3Properties.Add(property.Key, new Vector3(x, y, z));
            }
            scene.ShadersProperties[scene.ActiveShader].Vector3Properties = vector3Properties;


            // update scene
            OnSceneUpdate();
        }


        # endregion


    }
}
