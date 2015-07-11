using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// 01.05.15
namespace LightingModels
{
   partial class Usefull
   {
        // debug log 
        public static void Log(string log)
        {
            Debug.WriteLine("***");
            Debug.WriteLine(log);
            Debug.WriteLine("***");
        }

    }
}
