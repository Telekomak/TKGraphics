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
        public int Stride { get; private set; }

        public VertexArrayElement[] Elements
        {
            get
            {
                return _elements.ToArray();
            }
        }

        public VertexArrayLayout(VertexArrayElement[] elements)
        {
            _elements = new List<VertexArrayElement>();
            _elements.AddRange(elements);
            Stride = elements.Sum(x => x.Size);
        }

        public VertexArrayLayout()
        {
            _elements = new List<VertexArrayElement>();
            Stride = 0;
        }

        public void Add(VertexArrayElement element)
        {
            Stride += element.Size;
            _elements.Add(element);
        }
    }
}
