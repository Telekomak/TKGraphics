using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TKGraphics.Abstraction;
using TKGraphics.Abstraction.DataStructures;
using TKGraphics.GLComponents;
using TKGraphics.Windowing;

namespace SpiralTest
{
    internal class Application : IDisposable
    {
        private GLWindow _window;

        private VertexBuffer _vertexBuffer;
        private Buffer<uint> _indexBuffer;
        private VertexArray _vertexArray;
        private ShaderProgram _shader;

        private int _count = 100;
        private int _depth = 10000;

        public Application()
        {
            Init();
        }

        private void Init()
        {
            _window = new GLWindow(new NativeWindowSettings(){Location = new Vector2i(100,100), Title = "test", Size = new Vector2i(800, 600)}, Color4.Black);
            _vertexBuffer = new VertexBuffer(GenVertexArray(_depth), BufferUsageHint.StaticDraw);
            _indexBuffer = new Buffer<uint>(GenIndexBuffer(_depth, _count), BufferUsageHint.DynamicDraw);

            VertexArrayLayout layout = new VertexArrayLayout();
            layout.Add(new VertexArrayElement(VertexAttribPointerType.Float, 3, 12, layout.Stride));
            layout.Add(new VertexArrayElement(VertexAttribPointerType.Float, 4, 16, layout.Stride));

            _vertexArray = new VertexArray(layout, _vertexBuffer);

            Shader vertexShader = new Shader(ShaderType.VertexShader, @"shaders\vertex.glsl");
            Shader fragmentShader = new Shader(ShaderType.FragmentShader, @"shaders\fragment.glsl");

            _shader = new ShaderProgram();
            _shader.AttachShader(vertexShader);
            _shader.AttachShader(fragmentShader);

            Matrix4 projectionMatrix = Matrix4.CreateOrthographicOffCenter(-2, 2, -1.5f, 1.5f, -1, 1);

            _shader.SetUniform<Matrix4>(projectionMatrix, "u_mvp");

            _window.KeyDown += WindowOnKeyDown;
        }

        private void WindowOnKeyDown(KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Keys.Up:
                    _count++;
                    break;

                case Keys.Down:
                    _count--;
                    break;
            }

            _indexBuffer.Update(GenIndexBuffer(_depth, _count));

            _window.DrawElements(_vertexArray, _indexBuffer, _shader, BeginMode.Lines);
            _window.SwapBuffers();
        }

        public void Update()
        {
            //_shader.SetUniform<float>((float)(_window.Time % 0.5) / 10, "u_time");
            _window.ProcessEvents();
        }

        public void Run()
        {
            GL.LineWidth(5);
            _window.Clear(ClearBufferMask.ColorBufferBit);
            _window.DrawElements(_vertexArray, _indexBuffer, _shader, BeginMode.Lines);
            _window.SwapBuffers();

            while (!_window.ShouldClose) Update();
        }

        public void Dispose()
        {
            _window.Dispose();
        }

        private MyVertex[] GenVertexArray(float depth)
        {
            List<MyVertex> vertices = new List<MyVertex>();
            float radius = 0;

            vertices.Add(new MyVertex(new Vector3(0, 0, 0.5f), new Vector4(1, 0, 0, 1)));

            for (int i = 1; i < depth; i++)
            {
                radius = (i / depth) * 15;
                float temp = i / 8f;
                vertices.Add(new MyVertex(new Vector3(MathF.Cos(temp) * radius, MathF.Sin(temp) * radius, 0.5f), new Vector4(MathF.Cos(temp), MathF.Sin(temp), 1 - (MathF.Sin(temp) + MathF.Cos(temp)), 1)));
            }

            return vertices.ToArray();
        }

        private uint[] GenIndexBuffer(int depth, int count)
        {
            List<uint> indicies = new List<uint>();
            count %= depth;

            for (uint i = 1; i <= count; i++)
            {
                indicies.Add(i - 1);
                indicies.Add(i);
            }

            return indicies.ToArray();
        }
    }

    class MyVertex : Vertex
    {
        public Vector4 Color { get; set; }

        public override float[] Vertices
        {
            get
            {
                return new[] { Position.X, Position.Y, Position.Z, Color.X, Color.Y, Color.Z, Color.W };
            }
        }

        public MyVertex(Vector3 position, Vector4 color) : base(position)
        {
            Color = color;
        }

        public override string ToString()
        {
            return $"{Position.X}, {Position.Y}, {Position.Z}";
        }
    }
}
