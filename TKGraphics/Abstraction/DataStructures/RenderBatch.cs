using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using TKGraphics.GLComponents;

namespace TKGraphics.Abstraction
{
    public class RenderBatch
    {
        public IndexBuffer IndexBuffer { get; set; }
        public VertexBuffer VertexBuffer { get; set; }
        public ShaderProgram Program { get; set; }
        public PrimitiveType PrimitiveType { get; set; }

        public RenderBatch(IndexBuffer indexBuffer, VertexBuffer vertexBuffer, ShaderProgram shader, PrimitiveType primitiveType)
        {
            IndexBuffer = indexBuffer;
            VertexBuffer = vertexBuffer;
            Program = shader;
            PrimitiveType = primitiveType;
        }

        public RenderBatch()
        {
            
        }

        public void Bind()
        {
            IndexBuffer.Bind();
            VertexBuffer.Bind();
            Program.Bind();
        }

        public void Unbind()
        {
            IndexBuffer.Unbind();
            VertexBuffer.Unbind();
            Program.Unbind();
        }
    }
}
