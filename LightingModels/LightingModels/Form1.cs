using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

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
            foreach (Volume entry in scene.objects)
            {
                objectsList.Items.Add(entry.Name);
            }
            objectsList.SelectedIndex = 0;
            currentObjectIndex = objectsList.SelectedIndex;

            currentObjectIndex = 0;

            // add shaders from scene to droplist
            foreach (KeyValuePair<string, ShaderProgram> entry in scene.shaders)
            {
                shadersList.Items.Add(entry.Key);
            }

            shadersList.SelectedIndex = 0;

            scene.OnUpdateFrame();

          
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

        private void shadersList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            scene.activeShader = shadersList.SelectedItem.ToString();
            if (openGLLoaded)
                OnSceneUpdate();
        }

        private void objectsList_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            currentObjectIndex = objectsList.SelectedIndex;
            if (openGLLoaded)
                OnSceneUpdate();
        }
        
        # endregion

        


    }
}
