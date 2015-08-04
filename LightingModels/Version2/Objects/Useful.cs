using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization; // for parsing float from string with ".", not ","
using OpenGL;

// 11.07.2015
namespace Version2
{
    public class Useful
    {
        // 
        public static Vector3 Normalize(Vector3 normal)
        {
            // calculate the length of the vector
            float len = (float)(Math.Sqrt((normal.x * normal.x) + (normal.y * normal.y) + (normal.z * normal.z)));

            // avoid division by 0
            if (len == 0.0f)
                len = 1.0f;

            // reduce to unit size
            normal.x /= len;
            normal.y /= len;
            normal.z /= len;
            return normal;
        }

        // debug log 
        public static void Log(string log)
        {
            if (log.Length > 0)
            { 
                Debug.WriteLine("***");
                Debug.WriteLine(log);
                Debug.WriteLine("***");
            }
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
                Useful.Log("Error parsing float: " + s);
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
                Useful.Log("Error parsing int: " + s);
            }

            return i;
        }

        //
        public static string GetModelsPath()
        {
            return "models/";
        }
    }
}
