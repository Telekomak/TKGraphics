using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace TKGraphics.GLComponents
{
    public class Buffer<T> : GLObject where T : unmanaged
    {
        private bool _isBatched;
        public BufferUsageHint Usage { get; protected set; }
        public BufferTarget Target { get; protected set; }
        public T[] Data { get; private set; }

        public bool IsBatched
        {
            get { return _isBatched;}
            set
            {
                if (value != _isBatched)
                {
                    if (!value)
                    {
                        Init();
                        ResizeBuffer();
                    }
                    else Dispose();

                    _isBatched = value;
                }
            }
        }

        public delegate void BufferUpdatedHandle<T>(Buffer<T> buffer) where T : unmanaged;
        public event BufferUpdatedHandle<T> BufferUpdated;


        public Buffer(IEnumerable<T> data, BufferUsageHint usage = BufferUsageHint.DynamicDraw, BufferTarget target = BufferTarget.ArrayBuffer)
        {
            Data = data.ToArray();
            Usage = usage;
            Target = target;
            Init();
            ResizeBuffer();
        }

        public Buffer(BufferUsageHint usage = BufferUsageHint.DynamicDraw, BufferTarget target = BufferTarget.ArrayBuffer)
        {
            Data = Array.Empty<T>();
            Usage = usage;
            Target = target;
            Init();
        }

        protected void Init()
        {
            Id = GL.GenBuffer();
        }

        protected void UpdateBuffer(IEnumerable<T> data)
        {
            Data = data.ToArray();
            UpdateBuffer();
        }

        protected void ResizeBuffer(IEnumerable<T> data)
        {
            Data = data.ToArray();
            ResizeBuffer();
        }

        protected void UpdateBuffer()
        {
            if (!_isBatched)
            {
                Bind();
                GL.BufferSubData(Target, IntPtr.Zero, (IntPtr)(Data.Length * Marshal.SizeOf(typeof(T))), Data);
                //Unbind();
            }
            BufferUpdated?.Invoke(this);
        }

        protected void ResizeBuffer()
        {
            if (!_isBatched)
            {
                Bind();
                GL.BufferData(Target, Data.Length * Marshal.SizeOf(typeof(T)), Data, Usage);
                //Unbind();
            }
            BufferUpdated?.Invoke(this);
        }

        public virtual void Update(IEnumerable<T> data)
        {
            if (data.Count() == Data.Length) UpdateBuffer(data);
            else ResizeBuffer(data);
        }

        public virtual void Add(T item)
        {
            //TODO Error handling
            T[] tmp = Data;
            Data = new T[Data.Length + 1];
            tmp.CopyTo(Data, 0);
            Data[^1] = item;

            ResizeBuffer();
        }

        public virtual void Add(IEnumerable<T> data)
        {
            T[] tmp = Data;
            Data = new T[Data.Length + data.Count()];
            tmp.CopyTo(Data, 0);
            data.ToArray().CopyTo(Data, tmp.Length);

            ResizeBuffer();
        }

        public virtual void Replace(T item, int index)
        {
            Data[index] = item;
            UpdateBuffer();
        }

        public virtual void Replace(IEnumerable<T> data, int index)
        {
            data.ToArray().CopyTo(Data, index);
            UpdateBuffer();
        }

        public virtual void Remove(int index)
        {
            T[] tmp = Data;
            Data = new T[Data.Length - 1];

            if (index == 0) Array.Copy(tmp, 1, Data, 0, Data.Length);
            else if (index == Data.Length - 1) Array.Copy(tmp, 0, Data, 0, Data.Length);
            else
            {
                Array.Copy(tmp, 0, Data, 0, index);
                Array.Copy(tmp, index + 1, Data, 0, tmp.Length - (index + 1));
            }

            ResizeBuffer();
        }

        public virtual void Remove(int index, int count)
        {
            T[] tmp = Data;
            Data = new T[Data.Length - count];

            if (index == 0) Array.Copy(tmp, 1 + count, Data, 0, Data.Length);
            else if (index + count == Data.Length - 1) Array.Copy(tmp, 0, Data, 0, Data.Length);
            else
            {
                Array.Copy(tmp, 0, Data, 0, index);
                Array.Copy(tmp, index + count + 1, Data, 0, tmp.Length - (index + count + 1));
            }

            ResizeBuffer();
        }

        public override void Bind()
        {
            IsBound = true;
            GL.BindBuffer(Target, Id);
        }

        public override void Unbind()
        {
            IsBound = false;
            GL.BindBuffer(Target, 0);
        }

        public override void Dispose()
        {
            GL.DeleteBuffer(Id);
        }
    }
}
