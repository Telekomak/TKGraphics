using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGraphics.Abstraction
{
    public class VertexArrayLayout
    {
        private List<VertexArrayElement> _elements;

        public VertexArrayElement[] Elements { get; private set; }
        public int Stride { get; private set; }

        public VertexArrayLayout(VertexArrayElement[] elements)
        {
            Elements = Array.Empty<VertexArrayElement>();
            _elements = new List<VertexArrayElement>();
            _elements.AddRange(elements);
            Stride = GetStride(elements);
        }

        public VertexArrayLayout()
        {
            Elements = Array.Empty<VertexArrayElement>();
            _elements = new List<VertexArrayElement>();
            Stride = 0;
        }

        private int GetStride(VertexArrayElement[] elements)
        {
            int stride = 0;

            foreach (VertexArrayElement element in elements) stride += element.Size;

            return stride;
            return Elements.Sum(x => x.Size);
        }

        public void Add(VertexArrayElement element)
        {
            element.Offset = Stride;
            Stride += element.Size;
            _elements.Add(element);
            Elements = _elements.ToArray();
        }
    }
}
