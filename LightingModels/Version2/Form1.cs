using System;
using System.Drawing;
using System.Windows.Forms;
using OpenGL;



// 04.08.2015
namespace Version2
{
    public partial class Form1 : Form
    {
        public int SceneWidth = 640;
        public int SceneHeight = 360;
        private static ShaderProgram program;
        private static VBO<Vector3> cube, cubeNormals;
        private static VBO<Vector2> cubeUV;
        private static VBO<int> cubeQuads;
        private static Texture texture;
        private static bool lighting = true;
        private static float xangle = 0f, yangle = 0f;
        private static bool left, right, up, down;

        public Form1()
        {
            InitializeComponent();

            simpleOpenGlControl1.InitializeContexts();
           // simpleOpenGlControl1.DestroyContexts();

            Gl.Enable(EnableCap.DepthTest);

            program = new ShaderProgram(VertexShader, FragmentShader);

            program.Use();
            program["projection_matrix"].SetValue(Matrix4.CreatePerspectiveFieldOfView(0.45f, (float)Width / Height, 0.1f, 1000f));
            program["view_matrix"].SetValue(Matrix4.LookAt(new Vector3(0, 0, 10), Vector3.Zero, Vector3.Up));

            program["light_direction"].SetValue(new Vector3(0, 0, 1));
            program["enable_lighting"].SetValue(lighting);

            texture = new Texture("2.png");
            
            cube = new VBO<Vector3>(new Vector3[] {
                    new Vector3(1, 1, -1), new Vector3(-1, 1, -1), new Vector3(-1, 1, 1), new Vector3(1, 1, 1),         // top
                    new Vector3(1, -1, 1), new Vector3(-1, -1, 1), new Vector3(-1, -1, -1), new Vector3(1, -1, -1),     // bottom
                    new Vector3(1, 1, 1), new Vector3(-1, 1, 1), new Vector3(-1, -1, 1), new Vector3(1, -1, 1),         // front face
                    new Vector3(1, -1, -1), new Vector3(-1, -1, -1), new Vector3(-1, 1, -1), new Vector3(1, 1, -1),     // back face
                    new Vector3(-1, 1, 1), new Vector3(-1, 1, -1), new Vector3(-1, -1, -1), new Vector3(-1, -1, 1),     // left
                    new Vector3(1, 1, -1), new Vector3(1, 1, 1), new Vector3(1, -1, 1), new Vector3(1, -1, -1) });      // right
            cubeNormals = new VBO<Vector3>(new Vector3[] {
                    new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), new Vector3(0, 1, 0), 
                    new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), new Vector3(0, -1, 0), 
                    new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), new Vector3(0, 0, 1), 
                    new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), new Vector3(0, 0, -1), 
                    new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), new Vector3(-1, 0, 0), 
                    new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0), new Vector3(1, 0, 0) });
            cubeUV = new VBO<Vector2>(new Vector2[] {
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1),
                    new Vector2(0, 0), new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1) });

            cubeQuads = new VBO<int>(new int[] { 3, 2, 1,0, 4, 5, 6, 7, 20, 21, 22, 23, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, }, BufferTarget.ElementArrayBuffer);



            Application.Idle += OnRenderFrame;
        }     

        private void OnRenderFrame(object sender, EventArgs e)
        {

            if (right) yangle += 0.05f;
            if (left) yangle -= 0.05f;
            if (up) xangle -= 0.05f;
            if (down) xangle += 0.05f;

            // set up the viewport and clear the previous depth and color buffers
            Gl.Viewport(0, 0, SceneWidth, SceneHeight);
            Gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

             // make sure the shader program and texture are being used
            Gl.UseProgram(program);
            Gl.BindTexture(texture);

            // set up the model matrix and draw the cube
            program["model_matrix"].SetValue(Matrix4.CreateRotationY(yangle) * Matrix4.CreateRotationX(xangle));
            program["enable_lighting"].SetValue(lighting);

            Gl.BindBufferToShaderAttribute(cube, program, "vertexPosition");
            Gl.BindBufferToShaderAttribute(cubeNormals, program, "vertexNormal");
            Gl.BindBufferToShaderAttribute(cubeUV, program, "vertexUV");
            Gl.BindBuffer(cubeQuads);
            
            Gl.DrawElements(BeginMode.Quads, cubeQuads.Count, DrawElementsType.UnsignedInt, IntPtr.Zero);


            simpleOpenGlControl1.SwapBuffers();
        }


        // used instead of glutDisplayFunc
        private void OnPaint(object sender, PaintEventArgs e)
        {
            // nothing for purpouse!
        }

        // used instead of glutReshapeFunc
        private void OnResize(object sender, EventArgs e)
        {

        }
        #region shaders
        public static string VertexShader = @"
#version 130

in vec3 vertexPosition;
in vec3 vertexNormal;
in vec2 vertexUV;

out vec3 normal;
out vec2 uv;

uniform mat4 projection_matrix;
uniform mat4 view_matrix;
uniform mat4 model_matrix;

void main(void)
{
    normal = normalize((model_matrix * vec4(floor(vertexNormal), 0)).xyz);
    uv = vertexUV;

    gl_Position = projection_matrix * view_matrix * model_matrix * vec4(vertexPosition, 1);
}
";

        public static string FragmentShader = @"
#version 130

uniform sampler2D texture;
uniform vec3 light_direction;
uniform bool enable_lighting;

in vec3 normal;
in vec2 uv;

out vec4 fragment;

void main(void)
{
    float diffuse = max(dot(normal, light_direction), 0);
    float ambient = 0.3;
    float lighting = (enable_lighting ? max(diffuse, ambient) : 1);

    fragment = lighting * texture2D(texture, uv);
}
";
        #endregion shaders

        private void simpleOpenGlControl1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W) up = true;
            else if (e.KeyCode == Keys.S) down = true;
            else if (e.KeyCode == Keys.D) right = true;
            else if (e.KeyCode == Keys.A) left = true;
        }




    }
}

