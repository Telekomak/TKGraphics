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

        public void Render(VertexArray vertexArray, IndexBuffer indexBuffer, ShaderProgram shader, BeginMode mode)
        {
            vertexArray.Bind();
            indexBuffer.Bind();
            shader.Bind();

            GL.DrawElements(mode, indexBuffer.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void SwapBuffers()
        { 
            GLFW.SwapBuffers(WindowPtr);
        }

        public void Clear(ClearBufferMask mask)
        {
           GL.Clear(mask);
           GLFW.SwapBuffers(WindowPtr);
        }
    }
}
