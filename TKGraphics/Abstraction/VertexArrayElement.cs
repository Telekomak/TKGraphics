using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace TKGraphics.Abstraction
{
    public class VertexArrayElement
    {
        public VertexAttribPointerType Type { get; private set; }
        public int Count { get; private set; }
        public int Size { get; private set; }
        public int Offset { get; set; }
        public bool Normalized { get; private set; }

        public VertexArrayElement(VertexAttribPointerType type, int count, int size, bool normalized = false)
        {
            Type = type;
            Count = count;
            Size = size;
            Offset = 0;
            Normalized = normalized;
        }
    }
}
