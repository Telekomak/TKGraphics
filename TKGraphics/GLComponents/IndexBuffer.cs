using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace TKGraphics.GLComponents
{
    public class IndexBuffer : GLObject
    { 
        public int Count { get; private set; }
        public int[] Data { get; private set; }
        public BufferUsageHint Usage { get; private set; }

        public IndexBuffer(int[] data, BufferUsageHint usage = BufferUsageHint.DynamicDraw)
        {
            Usage = usage;
            Data = data;
            Count = data.Length;

            Init();
            Update(data, usage);
        }

        public IndexBuffer()
        {
            Usage = BufferUsageHint.DynamicDraw;
            Count = 0;
            Data = Array.Empty<int>();

            Init();
        }

        protected override void Init()
        {
            Id = GL.GenBuffer();
        }

        public void Update(int[] data, BufferUsageHint usage)
        {
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, usage);

            Count = data.Length;
            Usage = usage;
            Data = data;
        }

        public void Update(int[] data)
        {
            Bind();
            GL.BufferData(BufferTarget.ElementArrayBuffer, data.Length * sizeof(uint), data, Usage);

            Count = data.Length;
            Data = data;
        }

        public override void Bind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Id);
        }

        public override void Unbind()
        {
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, Id);
        }

        public override void Dispose()
        {
            GL.DeleteBuffer(Id);
        }
    }
}
