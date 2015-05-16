using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

// 15.05.2015
namespace LightingModels
{
    class Scene
    {
        Vector3[] vertdata;
        Vector3[] normalsdata;
        Vector3[] coldata;
        Vector2[] texcoorddata;
        int[] indicedata;
        int ibo_elements;

        public List<Volume> objects = new List<Volume>(); 
        public Dictionary<string, int> textures = new Dictionary<string, int>();
        public Dictionary<string, ShaderProgram> shaders = new Dictionary<string, ShaderProgram>();
        public string activeShader = "default";

        public int Height;
        public int Width;
        public Camera Camera = new Camera();
        // constructor for scene with given width and height
        public Scene(int width, int height)
        {
            Height = height;
            Width = width;
        }

        //
        void initProgram()
        {
            GL.GenBuffers(1, out ibo_elements);

            // Load shaders from files
            // Default
            shaders.Add("default", new ShaderProgram("glsl/vs.glsl", "glsl/fs.glsl"));

            // With Texture
            shaders.Add("textured", new ShaderProgram("glsl/vs_text.glsl", "glsl/fs_text.glsl"));

            // Phong Lighting
            shaders.Add("phongLight", new ShaderProgram("glsl/vs_lightPhong.glsl", "glsl/fs_lightPhong.glsl"));


            activeShader = "default";
            // Load textures from files
            string texturePath = "textures/text_orange.png";
            string textureName = "text_orange.png";
            textures.Add(textureName, loadImage(texturePath));
         
            // Create objects 
            TestTexturedCube ttc = new TestTexturedCube();
            ttc.Name = "Cube";
            ttc.Position = new Vector3(-1,0,0);
            ttc.TextureID = textures[textureName];
            objects.Add(ttc);


            ObjVolume objFromFile = ObjVolume.LoadFromFile("models/teapot.obj");
            objFromFile.Name = "Teapot";
            objFromFile.Position += new Vector3(1, 0, 0);
            objFromFile.Scale = new Vector3( 0.2f, 0.2f, 0.2f);
            objFromFile.TextureID = textures[textureName];
            objects.Add(objFromFile);
            
            
            // Move camera away from origin
            Camera.Position += new Vector3(0f, 0f, 3f);
        }

        //
        public void OnLoad()
        {
            initProgram();

            GL.ClearColor(Color.Gray);
        }
        //
        public void OnRenderFrame()
        {
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.Enable(EnableCap.DepthTest);

            shaders[activeShader].EnableVertexAttribArrays();

            int indiceat = 0;

            // Draw all our objects
            foreach (Volume v in objects)
            {
                GL.BindTexture(TextureTarget.Texture2D, v.TextureID);
                GL.UniformMatrix4(shaders[activeShader].GetUniform("modelView"), false, ref v.ModelViewProjectionMatrix);
                GL.UniformMatrix4(shaders[activeShader].GetUniform("modelViewMatrix"), false, ref v.ModelMatrix);
                GL.UniformMatrix4(shaders[activeShader].GetUniform("projectionMatrix"), false, ref v.ViewProjectionMatrix);

                if (shaders[activeShader].GetAttribute("maintexture") != -1)
                {
                    GL.Uniform1(shaders[activeShader].GetAttribute("maintexture"), v.TextureID);
                }

                GL.DrawElements(BeginMode.Triangles, v.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += v.IndiceCount;
            }

            shaders[activeShader].DisableVertexAttribArrays();

            GL.Flush();
            //SwapBuffers(); -> can't do that here, must be done in Form
        }

        //
        public void OnUpdateFrame()
        {
            List<Vector3> verts = new List<Vector3>();
            List<Vector3> normals = new List<Vector3>();
            List<int> inds = new List<int>();
            List<Vector3> colors = new List<Vector3>();
            List<Vector2> texcoords = new List<Vector2>();

            // Assemble vertex and indice data for all volumes
            int vertcount = 0;
            foreach (Volume v in objects)
            {
                verts.AddRange(v.GetVerts().ToList());
                normals.AddRange(v.GetNormals().ToList());
                inds.AddRange(v.GetIndices(vertcount).ToList());
                colors.AddRange(v.GetColorData().ToList());
                texcoords.AddRange(v.GetTextureCoords());
                vertcount += v.VertCount;
            }

            vertdata = verts.ToArray();
            normalsdata = verts.ToArray();
            indicedata = inds.ToArray();
            coldata = colors.ToArray();
            texcoorddata = texcoords.ToArray();

            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vPosition"));

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            // Buffer vertex color if shader supports it
            if (shaders[activeShader].GetAttribute("vColor") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vColor"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vColor"), 3, VertexAttribPointerType.Float, true, 0, 0);
            }


            // Buffer normal color if shader supports it
            if (shaders[activeShader].GetAttribute("vNormal") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("vNormal"));

                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(normalsdata.Length * Vector3.SizeInBytes), normalsdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            }

            // Buffer texture coordinates if shader supports it
            if (shaders[activeShader].GetAttribute("texcoord") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[activeShader].GetBuffer("texcoord"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(shaders[activeShader].GetAttribute("texcoord"), 2, VertexAttribPointerType.Float, true, 0, 0);
            }

            // Update model view matrices
            foreach (Volume v in objects)
            {
                v.CalculateModelMatrix();
                v.ViewProjectionMatrix = Camera.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(1.3f, Width / (float)Height, 1.0f, 40.0f);
                v.ModelViewProjectionMatrix = v.ModelMatrix * v.ViewProjectionMatrix;
            }

            GL.UseProgram(shaders[activeShader].ProgramID);

            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // Buffer index data
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr)(indicedata.Length * sizeof(int)), indicedata, BufferUsageHint.StaticDraw);
        }

        // Rotate object
        // x, y, z - parametrs are in degress
        public void RotateObject(int objectIndex, float x, float y, float z)
        {
            x = (float)((double)x * Math.PI / 180);
            y = (float)((double)y * Math.PI / 180);
            z = (float)((double)z * Math.PI / 180);

            objects[objectIndex].RotateObject(x, y, z);
        }

        // Move object
        public void MoveObject(int objectIndex, float x, float y, float z)
        {
            objects[objectIndex].MoveObject(x, y, z);
        }

        //         
        #region files functions
        int loadImage(Bitmap image)
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

        int loadImage(string filename)
        {
            try
            {
                Bitmap file = new Bitmap(filename);
                return loadImage(file);
            }
            catch (FileNotFoundException e)
            {
                return -1;
            }
        }
        #endregion

        # region static normals
        // Surface normals
        public static Vector3 CalculateFaceNormal(Vector3 a, Vector3 b, Vector3 c)
        {
            Vector3 A, B;

            // A
            A.X = a.X - b.X;
            A.Y = a.Y - b.Y;
            A.Z = a.Z - b.Z;

            // B
            B.X = b.X - c.X;
            B.Y = b.Y - c.Y;
            B.Z = b.Z - c.Z;

            // calculate the cross product and place the resulting vector
            // into the normal
            Vector3 faceNormal = new Vector3();
            faceNormal.X = (A.Y * B.Z) - (A.Z * B.Y);
            faceNormal.Y = (A.Z * B.X) - (A.X * B.Z);
            faceNormal.Z = (A.X * B.Y) - (A.Y * B.X);

            // normalize
            return normalize(faceNormal);
        }

        // 
        internal static Vector3 normalize(Vector3 normal)
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

        #endregion
    }
}
