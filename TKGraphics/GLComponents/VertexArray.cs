using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using TKGraphics.Abstraction;

namespace TKGraphics.GLComponents
{
    public class VertexArray : GLObject
    {
        public VertexBuffer Buffer { get; private set; }
        public VertexArrayLayout Layout { get; private set; }

        public VertexArray(VertexBuffer buffer, VertexArrayLayout layout)
        {
            Buffer = buffer;
            Layout = layout;

            Init();
            Update(buffer, layout);
        }

        public VertexArray()
        {
            Init();
        }

        protected override void Init()
        {
            Id = GL.GenVertexArray();
        }

        public void Update(VertexBuffer vertexBuffer, VertexArrayLayout layout)
        {
            vertexBuffer.Bind();
            Bind();

            int offset = 0;
            for (int i = 0; i < layout.Elements.Length; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribPointer(i, layout.Elements[i].Count, layout.Elements[i].Type, layout.Elements[i].Normalized, layout.Stride, layout.Elements[i].Offset);
            }
        }

        public void Update(VertexBuffer vertexBuffer)
        {
            vertexBuffer.Bind();
            Bind();

            int offset = 0;
            for (int i = 0; i < Layout.Elements.Length; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribPointer(i, Layout.Elements[i].Count, Layout.Elements[i].Type, Layout.Elements[i].Normalized, Layout.Stride, offset);
                offset += Layout.Elements[i].Size * Layout.Elements[i].Count;
            }
        }

        public override void Bind()
        {
            GL.BindVertexArray(Id);
        }

        public override void Unbind()
        {
            GL.BindVertexArray(0);
        }

        public override void Dispose()
        {
            GL.DeleteVertexArray(Id);
        }
    }
}
