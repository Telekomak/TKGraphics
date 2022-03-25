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
        public VertexArrayLayout Layout { get; private set; }
        public VertexBuffer Buffer { get; private set; }

        public VertexArray(VertexArrayLayout layout, VertexBuffer buffer)
        {
            Init();
            Update(layout, buffer);
        }

        public VertexArray()
        {
            Init();
        }

        private void Init()
        {
            Id = GL.GenVertexArray();
        }

        public void Update(VertexArrayLayout layout, VertexBuffer buffer)
        {
            Buffer = buffer;
            Layout = layout;
            Update();
        }

        public void Update(VertexArrayLayout layout)
        {
            Layout = layout;
            Update();
        }

        private void Update()
        {
            Bind();
            Buffer.Bind();

            for (int i = 0; i < Layout.Elements.Length; i++)
            {
                GL.EnableVertexAttribArray(i);
                GL.VertexAttribPointer(i, Layout.Elements[i].Count, Layout.Elements[i].Type, Layout.Elements[i].Normalized, Layout.Stride, Layout.Elements[i].Offset);
            }

            //Unbind();
            //Buffer.Unbind();
        }

        public override void Bind()
        {
            IsBound = true;
            GL.BindVertexArray(Id);
        }

        public override void Unbind()
        {
            IsBound = false;
            GL.BindVertexArray(0);
        }

        public override void Dispose()
        {
            GL.DeleteVertexArray(Id);
        }
    }
}
