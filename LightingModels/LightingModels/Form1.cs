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
        
        // 
        private bool openGLLoaded = false; // just in case?

        //
        public Form1()
        {
            InitializeComponent();         
        }

        //
        private void OnLoad(object sender, EventArgs e)
        {
            openGLLoaded = true;

            Singleton.OpenTKManager.SetupViewport(glControlMain.Width, glControlMain.Height);
            Singleton.OpenTKManager.Clear();

            Singleton.ShaderProgram.InitProgram();
            Singleton.Scene1.UpdateScene();
        }
       
        //
        private void OnPaint(object sender, PaintEventArgs e)
        {
            if (!openGLLoaded) 
                return;

            Singleton.Scene1.UpdateScene();
            Singleton.Scene1.RenderScene();
          
            glControlMain.SwapBuffers();
        }

        
    }
}
