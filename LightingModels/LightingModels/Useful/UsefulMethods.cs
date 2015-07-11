using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Globalization; // for parsing float from string with ".", not ","

// 11.07.2015
namespace LightingModels
{
    class UsefulMethods
    {
        // 
        public static Vector3 Normalize(Vector3 normal)
        {
            // calculate the length of the vector
            float len = (float)(Math.Sqrt((normal.X * normal.X) + (normal.Y * normal.Y) + (normal.Z * normal.Z)));

            // avoid division by 0
            if (len == 0.0f)
                len = 1.0f;

            // reduce to unit size
            normal.X /= len;
            normal.Y /= len;
            normal.Z /= len;
            return normal;
        }

        // debug log 
        public static void Log(string log)
        {
            Debug.WriteLine("***");
            Debug.WriteLine(log);
            Debug.WriteLine("***");
        }

        //
        public static float GetFloat(string s)
        {
            float f = 0.0f;

            try
            {
                f = float.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception e)
            {
                UsefulMethods.Log("Error parsing float: " + s);
            }

            return f;
        }

        //
        public static int GetInt(string s)
        {
            int i = 0;

            try
            {
                i = int.Parse(s, CultureInfo.InvariantCulture.NumberFormat);
            }
            catch (Exception e)
            {
                UsefulMethods.Log("Error parsing int: " + s);
            }

            return i;
        }

        // check for GL error
        public static void CheckForGLError(string className)
        {
            ErrorCode error = ErrorCode.NoError;

            GL.Finish();

            error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                throw new ArgumentException("Error: " + className);
            }
        }
    }
}
