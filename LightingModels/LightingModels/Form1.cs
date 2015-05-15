using System;
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

        // 
        private bool openGLLoaded = false; // just in case?

        //
        public Form1()
        {
            InitializeComponent();
            scene = new Scene(glControlMain.Width, glControlMain.Height);
        }

        //
        private void OnLoad(object sender, EventArgs e)
        {
            openGLLoaded = true;

            scene.OnLoad();
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

        //
        private void objRotateLeft_Click(object sender, EventArgs e)
        {
            scene.RotateObject(0, 0, -10, 0);
            OnSceneUpdate();
        }

        //
        private void objRotateRight_Click(object sender, EventArgs e)
        {
            scene.RotateObject(0, 0, 10, 0);
            OnSceneUpdate();
        }

        //
        private void objRotateUp_Click(object sender, EventArgs e)
        {
            scene.RotateObject(0, -10, 0, 0);
            OnSceneUpdate();
        }

        //
        private void objRotateDown_Click(object sender, EventArgs e)
        {
            scene.RotateObject(0, 10, 0, 0);
            OnSceneUpdate();
        }
    }
}
