using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using TKGraphics.Abstraction;
using TKGraphics.Abstraction.DataStructures;
using TKGraphics.GLComponents;
using TKGraphics.Windowing;

namespace BatchTest
{
    internal class Application : IDisposable
    {
        private GLWindow _window;
        private VertexArray _vertexArray;
        private VertexBuffer _vertexBuffer;
        private Buffer<uint> _indexBuffer;
        private ShaderProgram _program;

        public Application()
        {
            Init();
        }

        private void Init()
        {
            _window = new GLWindow(new NativeWindowSettings() { Location = new Vector2i(100, 100), Title = "Batch test", Size = new Vector2i(800, 600), IsEventDriven = false, APIVersion = new Version(4, 6)}, Color4.Orange);
            _indexBuffer = new Buffer<uint>(new[] { 0u, 1u, 2u }, BufferUsageHint.DynamicDraw);

            List<MyVertex> vertices = new List<MyVertex>();
            vertices.Add(new MyVertex(new Vector3(0.5f, 0, 0), Color4.Red));
            vertices.Add(new MyVertex(new Vector3(0.5f, 0.5f, 0), Color4.Blue));
            vertices.Add(new MyVertex(new Vector3(-0.5f, 0, 0), Color4.Green));

            _vertexBuffer = new VertexBuffer(vertices, BufferUsageHint.StaticDraw);

            VertexArrayLayout layout = new VertexArrayLayout();
            layout.Add(new VertexArrayElement(VertexAttribPointerType.Float, 3, 3 * sizeof(float), layout.Stride));
            layout.Add(new VertexArrayElement(VertexAttribPointerType.Float, 4, 4 * sizeof(float), layout.Stride));

            _vertexArray = new VertexArray(layout, _vertexBuffer);

            Shader vertex = new Shader(ShaderType.VertexShader, "shaders/vertex.glsl");
            Shader fragment = new Shader(ShaderType.FragmentShader, "shaders/fragment.glsl");

            _program = new ShaderProgram(new[] { vertex, fragment });
        }

        private void Update()
        {
            _window.ProcessEvents();
            //GetError();
        }

        private void Loop()
        {
            _window.Clear(ClearBufferMask.ColorBufferBit);

            while (!_window.ShouldClose)
            {
                Update();
                _window.DrawElements(_vertexArray, _indexBuffer, _program, BeginMode.Triangles);
                _window.SwapBuffers();
            }
        }

        public void Run()
        {
            Loop();
        }

        public void Dispose()
        {
            _window.Dispose();
        }

        private void GetError()
        {
            Console.WriteLine(GL.GetError());
        }
    }

    class MyVertex : Vertex
    {
        public Color4 Color { get; set; }

        public override float[] Vertices
        {
            get
            {
                return new[] { Position.X, Position.Y, Position.Z, Color.R, Color.G, Color.B, Color.A };
            }
        }

        public MyVertex(Vector3 position, Color4 color) : base(position)
        {
            Color = color;
        }
    }
}
