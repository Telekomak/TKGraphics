using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TKGraphics.Abstraction;
using TKGraphics.GLComponents;
using TKGraphics.Windowing;

namespace Test
{
    internal class Application : IDisposable
    {
        private GLWindow _window;

        private VertexBuffer _vertexBuffer;
        private IndexBuffer _indexBuffer;
        private VertexArray _vertexArray;
        private ShaderProgram _shader;
        private Texture _texture;

        private MyVertex[] _vertices;

        private int _count = 10;
        private int _depth = 1000;

        public Application()
        {
            Init();
        }

        private void Init()
        {
            _window = new GLWindow(new NativeWindowSettings(){Location = new Vector2i(100,100), Title = "test", Size = new Vector2i(800, 800)}, Color4.Black);
            _vertexBuffer = new VertexBuffer(GetVertices(GenVertexArray(_depth)));
            _indexBuffer = new IndexBuffer(GenIndexBuffer(_depth, _count));

            VertexArrayLayout layout = new VertexArrayLayout();
            layout.Add(new VertexArrayElement(VertexAttribPointerType.Float, 3, 12));
            layout.Add(new VertexArrayElement(VertexAttribPointerType.Float, 4, 16));

            _vertexArray = new VertexArray(_vertexBuffer, layout);

            Shader vertexShader = new Shader(ShaderType.VertexShader, @"shaders\vertex.glsl");
            Shader fragmentShader = new Shader(ShaderType.FragmentShader, @"shaders\fragment.glsl");

            _shader = new ShaderProgram();
            _shader.AttachShader(vertexShader);
            _shader.AttachShader(fragmentShader);

            Matrix4 projectionMatrix = Matrix4.CreateOrthographic(800, 800, 10, -10);
            Matrix4 viewMatrix = Matrix4.CreateTranslation(100, 150, 0);
            Matrix4 modelMatrix = Matrix4.CreateTranslation(1, 1, 1);

            //_shader.SetUniform<Matrix4>(projectionMatrix * viewMatrix * modelMatrix,"u_mvp");
            //_shader.SetUniform<Matrix4>(projectionMatrix, "u_mvp");

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

            _window.Clear(ClearBufferMask.ColorBufferBit);
            _window.SwapBuffers();
            _window.Render(_vertexArray, _indexBuffer, _shader, BeginMode.Lines);
            _window.SwapBuffers();
        }

        public void Update()
        {
            _window.ProcessEvents();
        }

        public void Run()
        {
            GL.LineWidth(3);
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
            
            vertices.Add(new MyVertex(new Vector3(0,0,0), new Vector4(1,0,0,1)));

            for (int i = 1; i < depth; i++)
            {
                radius = (i / depth) * 5;
                vertices.Add(new MyVertex(new Vector3(MathF.Cos(i) * radius, MathF.Sin(i) * radius, 0), new Vector4(MathF.Cos(i), MathF.Sin(i), 1 - (MathF.Cos(i) + MathF.Sin(i)), 1)));
            }

            return vertices.ToArray();
        }

        private Vector4 GetColor(float value, float alpha)
        {
            float third = (MathF.PI * 2) / 3;

            float r = 1 - ((value - (MathF.PI * 2)) / third);
            float g = 1 - ((value - third) / third);
            float b = 1 - ((value - third * 2) / third);

            return new Vector4(//WTF??
                r > 0 ? r : 0,
                g > 0 ? g : 0,
                b > 0 ? b : 0,
                alpha);
        }

        private int[] GenIndexBuffer(int depth, int count)
        {
            List<int> indicies = new List<int>();
            count %= depth;

            for (int i = 1; i <= count; i++)
            {
                indicies.Add(i-1);
                indicies.Add(i);
            }

            return indicies.ToArray();
        }

        private float[] GetVertices(Vertex[] vertices)
        {
            List<float> result = new List<float>();

            foreach (Vertex vertex in vertices)
            {
                result.AddRange(vertex.Vertices);    
            }

            return result.ToArray();
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
