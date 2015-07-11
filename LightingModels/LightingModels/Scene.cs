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

        public List<Volume> Objects = new List<Volume>();
        public Dictionary<string, ShadersProperty> ShadersProperties = new Dictionary<string, ShadersProperty>();
        public Dictionary<string, int> Textures = new Dictionary<string, int>();
        public Dictionary<string, ShaderProgram> Shaders = new Dictionary<string, ShaderProgram>();
        public string ActiveShader = "default";

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

            // Create lights
            Light.Enable();
            Light lightRed = new Light("lightRed");
            lightRed.Position = new Vector3(1.0f, 1.0f, 1.0f);
            lightRed.Ambient = new Vector3(0.3f, 0.0f, 0.0f);
            lightRed.Diffuse = new Vector3(0.5f, 0.0f, 0.0f);
            lightRed.Specular = new Vector3(1.0f, 1.0f, 1.0f);
            Light.Add(lightRed, 0);

            // Load textures from files
             string texturePath = "textures/text_orange.png";
            string textureName = "text_orange.png";
            Textures.Add(textureName, Texture.LoadImage(texturePath));

            // Load shaders from files
            // Default
            Shaders.Add("default", new ShaderProgram("glsl/vs.glsl", "glsl/fs.glsl"));

            // With Texture
            Shaders.Add("textured", new ShaderProgram("glsl/vs_text.glsl", "glsl/fs_text.glsl"));

            // Phong Lighting
            Shaders.Add("phongLight", new ShaderProgram("glsl/vs_lightPhong.glsl", "glsl/fs_lightPhong.glsl"));
            PhongProperty phong = new PhongProperty();
            phong.Activate();
            phong.AddLight(lightRed);
            ShadersProperties.Add("phongLight", phong);
          

            // Create objects 
            TestTexturedCube ttc = new TestTexturedCube();
            ttc.Name = "Cube";
            ttc.Position = new Vector3(-1, 0, 0);
            ttc.TextureID = Textures[textureName];
            Objects.Add(ttc);


            // ball with amterial
            Volume objFromBlenderObjFile = ObjVolume.LoadFromFileFromBlenderObj("models/fromBlender/ball.obj");
            objFromBlenderObjFile.Name = "Ball from Blender";
            objFromBlenderObjFile.Position += new Vector3(1, 0, 0);
            objFromBlenderObjFile.Scale = new Vector3(0.4f, 0.4f, 0.4f);
            objFromBlenderObjFile.TextureID = Textures[textureName];
            Objects.Add(objFromBlenderObjFile);

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

            Shaders[ActiveShader].EnableVertexAttribArrays();

            int indiceat = 0;

            // Draw all our objects
            foreach (Volume v in Objects)
            {
                GL.BindTexture(TextureTarget.Texture2D, v.TextureID);
                GL.UniformMatrix4(Shaders[ActiveShader].GetUniform("modelView"), false, ref v.ModelViewProjectionMatrix);
                GL.UniformMatrix4(Shaders[ActiveShader].GetUniform("modelViewMatrix"), false, ref v.ModelMatrix);
                GL.UniformMatrix4(Shaders[ActiveShader].GetUniform("projectionMatrix"), false, ref v.ViewProjectionMatrix);

                if (Shaders[ActiveShader].GetAttribute("maintexture") != -1)
                {
                    GL.Uniform1(Shaders[ActiveShader].GetAttribute("maintexture"), v.TextureID);
                }

                GL.DrawElements(BeginMode.Triangles, v.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += v.IndiceCount;
            }

            Shaders[ActiveShader].DisableVertexAttribArrays();

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
            foreach (Volume v in Objects)
            {
                //v.VertCountIndex = vertcount;
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

            GL.BindBuffer(BufferTarget.ArrayBuffer, Shaders[ActiveShader].GetBuffer("vPosition"));

            GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(Shaders[ActiveShader].GetAttribute("vPosition"), 3, VertexAttribPointerType.Float, false, 0, 0);

            // Buffer vertex color if shader supports it
            if (Shaders[ActiveShader].GetAttribute("vColor") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, Shaders[ActiveShader].GetBuffer("vColor"));
                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Shaders[ActiveShader].GetAttribute("vColor"), 3, VertexAttribPointerType.Float, true, 0, 0);
            }

            // Buffer normal color if shader supports it
            if (Shaders[ActiveShader].GetAttribute("vNormal") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, Shaders[ActiveShader].GetBuffer("vNormal"));

                GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(normalsdata.Length * Vector3.SizeInBytes), normalsdata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Shaders[ActiveShader].GetAttribute("vNormal"), 3, VertexAttribPointerType.Float, false, 0, 0);

            }  
            // Buffer texture coordinates if shader supports it
            if (Shaders[ActiveShader].GetAttribute("texcoord") != -1)
            {
                GL.BindBuffer(BufferTarget.ArrayBuffer, Shaders[ActiveShader].GetBuffer("texcoord"));
                GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr)(texcoorddata.Length * Vector2.SizeInBytes), texcoorddata, BufferUsageHint.StaticDraw);
                GL.VertexAttribPointer(Shaders[ActiveShader].GetAttribute("texcoord"), 2, VertexAttribPointerType.Float, true, 0, 0);
            }

            // add data from ShadersProperties         
            if (ShadersProperties.ContainsKey(ActiveShader) && ShadersProperties[ActiveShader].Vector3PropertiesCount > 0)
            {
                foreach (KeyValuePair<string,Vector3> property in ShadersProperties[ActiveShader].Vector3Properties)
                {
                    GL.Uniform3(GL.GetUniformLocation(Shaders[ActiveShader].ProgramID, property.Key),property.Value);
                }
            }
            if (ShadersProperties.ContainsKey(ActiveShader) && ShadersProperties[ActiveShader].FloatPropertiesCount > 0)
            {
                foreach (KeyValuePair<string, float> property in ShadersProperties[ActiveShader].FloatProperties)
                {
                    float f = property.Value;
                    GL.Uniform1(GL.GetUniformLocation(Shaders[ActiveShader].ProgramID, property.Key), 1, ref f);
                }
            }
             
            // Update model view matrices
            foreach (Volume v in Objects)
            {
                v.CalculateModelMatrix();
                v.ViewProjectionMatrix = Camera.GetViewMatrix() * Matrix4.CreatePerspectiveFieldOfView(1.3f, Width / (float)Height, 1.0f, 40.0f);
                v.ModelViewProjectionMatrix = v.ModelMatrix * v.ViewProjectionMatrix;
            }

            GL.UseProgram(Shaders[ActiveShader].ProgramID);

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

            Objects[objectIndex].RotateObject(x, y, z);
        }

        // Move object
        public void MoveObject(int objectIndex, float x, float y, float z)
        {
            Objects[objectIndex].MoveObject(x, y, z);
        }
    }
}
