using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TKGraphics.GLComponents
{
    public abstract class GLObject : IDisposable
    {
        public int Id { get; protected set; }
        public bool IsBound { get; protected set; }

        protected GLObject()
        {
            
        }

        public abstract void Dispose();
        public abstract void Bind();
        public abstract void Unbind();
    }
}
