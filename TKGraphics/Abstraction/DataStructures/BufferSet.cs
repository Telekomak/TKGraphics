using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TKGraphics.GLComponents;

namespace TKGraphics.Abstraction.DataStructures
{
    public struct BufferSet
    {
        public VertexBuffer VertexBuffer { get; set; }
        public Buffer<int> IndexBuffer { get; set; }

        public BufferSet(VertexBuffer vertexBuffer, Buffer<int> indexBuffer)
        {
            VertexBuffer = vertexBuffer;
            IndexBuffer = indexBuffer;
        }
    }
}
