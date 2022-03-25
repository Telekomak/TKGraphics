using System;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using TKGraphics.GLComponents;

namespace TKGraphics.Windowing
{
    public unsafe class GLWindow : NativeWindow
    {
        public bool ShouldClose
        {
            get { return GLFW.WindowShouldClose(WindowPtr); }
            private set { GLFW.SetWindowShouldClose(WindowPtr, value); }
        }

        public double Time
        {
            get { return GLFW.GetTime(); }
            set { GLFW.SetTime(value); }
        }

        public GLWindow(NativeWindowSettings settings, Color4 clearColor) : base(settings)
        {
            Init(clearColor);
        }

        private void Init(Color4 clearColor)
        {
            GL.ClearColor(clearColor);
        }

        public void DrawElements(VertexArray vertexArray, Buffer<uint> indexBuffer, ShaderProgram shader, BeginMode mode)
        {
            vertexArray.Bind();
            indexBuffer.Bind();
            shader.Bind();

            GL.DrawElements(mode, indexBuffer.Data.Length, DrawElementsType.UnsignedInt, 0);
            //GL.DrawElements(PrimitiveType.Triangles, indexBuffer.Data.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
        }

        public void DrawArrays(PrimitiveType primitive, int first, int count, VertexArray vertexArray, ShaderProgram shader)
        {
            vertexArray.Bind();
            shader.Bind();

            GL.DrawArrays(primitive, first, count);
        }

        public void SwapBuffers()
        { 
            GLFW.SwapBuffers(WindowPtr);
        }

        public void Clear(ClearBufferMask mask)
        {
           GL.Clear(mask);
        }
    }
}
