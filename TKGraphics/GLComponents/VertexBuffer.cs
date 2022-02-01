using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace TKGraphics.GLComponents
{
    public class VertexBuffer : GLObject
    {
        public BufferUsageHint Usage { get; private set; }
        public float[] Vertices { get; private set; }
        public VertexBuffer(float[] vertices, BufferUsageHint usage = BufferUsageHint.StaticDraw)
        {
            Vertices = vertices;
            Usage = usage;
            Init();
            Update(vertices);
        }

        public VertexBuffer()
        {
            Usage = BufferUsageHint.StaticDraw;
            Vertices = Array.Empty<float>();
            Init();
        }

        protected override void Init()
        {
            Id = GL.GenBuffer();
        }

        public void Update(float[] vertices)
        {
            Bind();
            Vertices = vertices;
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, Usage);
        }

        public void Update(BufferUsageHint usage)
        {
            Bind();
            Usage = usage;
            GL.BufferData(BufferTarget.ArrayBuffer, Vertices.Length * sizeof(float), Vertices, Usage);
        }

        public void Update(float[] vertices, BufferUsageHint usage)
        {
            Bind();
            Usage = usage;
            Vertices = vertices;
            GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices, usage);
        }

        public override void Bind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, Id);
        }

        public override void Unbind()
        {
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
        }

        public override void Dispose()
        {
            GL.DeleteBuffer(Id);
        }
    }
}
