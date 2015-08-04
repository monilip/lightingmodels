using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL;

// 11.07.2015
namespace LightingModels
{
    public class Texture
    {
        public static bool LoadTextures = true;

        public static Dictionary<string, int> Textures = new Dictionary<string, int>();

        //
        public static int LoadImage(string filepath, string filename)
        {
            try
            {
                // create bitmap
                Bitmap file = new Bitmap(filepath);
                // add to TexturesList
                int textureID = LoadImage(file);
                Textures.Add(filename, textureID);
                return textureID;
            }
            catch (FileNotFoundException e)
            {
                UsefulMethods.Log("Didn't found texture "+filepath);
                return -1;
            }
        }

        //
        private static int LoadImage(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

    }
}
