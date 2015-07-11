using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

// 11.07.2015
namespace LightingModels
{
    class Texture
    {
        string textureName = null;
        uint textureID = 0;
        //int width, height;

        //public int Width { get { return width; } }
        //public int Height { get { return height; } }
        public static Dictionary<string, int> Textures = new Dictionary<string, int>();

        //
        public static int LoadImage(string filename)
        {
            try
            {
                Bitmap file = new Bitmap(filename);
                return LoadImage(file);
            }
            catch (FileNotFoundException e)
            {
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
