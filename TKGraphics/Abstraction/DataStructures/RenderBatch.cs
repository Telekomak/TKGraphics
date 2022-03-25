using OpenTK.Graphics.OpenGL4;
using TKGraphics.GLComponents;

namespace TKGraphics.Abstraction.DataStructures
{
    public class RenderBatch : IDisposable
    {
        private Buffer<int> _mainIndexBuffer;
        private VertexBuffer _mainVertexBuffer;
        private List<BufferSet> _bufferSets;

        public VertexArray VertexArray { get; private set; }
        public BufferSet[] BufferSets
        {
            get
            {
                return _bufferSets.ToArray();
            }
        }

        public RenderBatch(IEnumerable<BufferSet> bufferSets, VertexArrayLayout layout)
        {
            _bufferSets = bufferSets.ToList();
            Init(layout);
            Update();
        }

        public RenderBatch(BufferSet bufferSet, VertexArrayLayout layout)
        {
            _bufferSets = new List<BufferSet>();
            _bufferSets.Add(bufferSet);
            Init(layout);
            Update();
        }

        public RenderBatch(VertexArrayLayout layout)
        {
            _bufferSets = new List<BufferSet>();
            Init(layout);
        }

        private void Init(VertexArrayLayout layout)
        {
            _mainIndexBuffer = new Buffer<int>();
            _mainVertexBuffer = new VertexBuffer();
            VertexArray = new VertexArray(layout, _mainVertexBuffer);

            foreach (BufferSet bufferSet in _bufferSets)
            {
                bufferSet.IndexBuffer.BufferUpdated += OnBufferUpdated;
                bufferSet.VertexBuffer.BufferUpdated += OnBufferUpdated;
            }
        }

        private void OnBufferUpdated<T>(Buffer<T> buffer) where T : unmanaged
        {
            Update();
        }

        private void Update()
        {
            List<int> indices = new List<int>();
            List<Vertex> vertices = new List<Vertex>();

            for (int i = 0; i < _bufferSets.Count; i++)
            {
                foreach (int index in _bufferSets[i].IndexBuffer.Data)
                {
                    indices.Add(index + i);
                }
                vertices.AddRange(_bufferSets[i].VertexBuffer.Vertices);
            }

            _mainIndexBuffer.Update(indices);
            _mainVertexBuffer.Update(vertices);
        }

        public void Bind()
        {
            _mainIndexBuffer.Bind();
            _mainVertexBuffer.Bind();
            VertexArray.Bind();
        }

        public void Unbind()
        {
            //_mainIndexBuffer.Unbind();
            //_mainVertexBuffer.Unbind();
            //VertexArray.Unbind();
        }

        public void Dispose()
        {
            _mainIndexBuffer.Dispose();
            _mainVertexBuffer.Dispose();
        }

        public void Add(BufferSet bufferSet)
        {
            _bufferSets.Add(bufferSet);
            bufferSet.IndexBuffer.BufferUpdated += OnBufferUpdated;
            bufferSet.VertexBuffer.BufferUpdated += OnBufferUpdated;
            Update();
        }

        public void Remove(BufferSet bufferSet)
        {
            _bufferSets.Remove(bufferSet);
            bufferSet.IndexBuffer.BufferUpdated -= OnBufferUpdated;
            bufferSet.VertexBuffer.BufferUpdated -= OnBufferUpdated;
            Update();
        }
    }
}
