using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using TKGraphics.Abstraction.DataStructures;

namespace TKGraphics.GLComponents
{
    public class VertexBuffer : Buffer<float>
    {
        private List<Vertex> _vertices;

        public Vertex[] Vertices
        {
            get
            {
                return _vertices.ToArray();
            }
        }

        public VertexBuffer(IEnumerable<Vertex> vertices, BufferUsageHint usage = BufferUsageHint.DynamicDraw) : base(usage)
        {
            _vertices = vertices.ToList();
            Init();
            ResizeBuffer(GetData());
        }

        public VertexBuffer(BufferUsageHint usage = BufferUsageHint.DynamicDraw) : base(usage)
        {
            Init();
        }

        private float[] GetData()
        {
            List<float> retData = new List<float>();

            foreach (Vertex vertex in _vertices)
            {
                retData.AddRange(vertex.Vertices);
            }

            return retData.ToArray();
        }

        private float[] GetData(IEnumerable<Vertex> vertices)
        {
            _vertices = vertices.ToList();
            List<float> retData = new List<float>();

            foreach (Vertex vertex in _vertices)
            {
                retData.AddRange(vertex.Vertices);
            }


            return retData.ToArray();
        }

        public void Update(IEnumerable<Vertex> data)
        {
            Update(GetData(data));
        }

        public void Add(Vertex vertex)
        {
            _vertices.Add(vertex);
            Update(GetData());
        }

        public void Add(IEnumerable<Vertex> vertices)
        {
            _vertices.AddRange(vertices);
            Update(GetData());
        }

        public void Replace(Vertex oldVertex, Vertex newVertex)
        {
            _vertices[_vertices.IndexOf(oldVertex)] = newVertex;
            Update(GetData());
        }
        
        public void Replace(int index, Vertex newVertex)
        {
            _vertices[index] = newVertex;
            Update(GetData());
        }

        public void Remove(Vertex vertex)
        {
            _vertices.Remove(vertex);
            Update(GetData());
        }
    }
}
