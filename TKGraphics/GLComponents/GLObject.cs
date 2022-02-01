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

        protected GLObject()
        {
            
        }

        protected virtual void Init()
        {

        }

        protected virtual void Init(object o)
        {

        }

        public virtual void Bind()
        {

        }

        public virtual void Unbind()
        {

        }
        public abstract void Dispose();
    }
}
