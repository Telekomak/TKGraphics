using System.Diagnostics;
using System.Text;
using TKGraphics.Utilities.Logger;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace TKGraphics.GLComponents
{
    public class ShaderProgram : GLObject
    {
        private Dictionary<string, int> _uniforms;
        private List<Shader> _attachedShaders;

        public Shader[] AttachedShaders
        {
            get { return _attachedShaders.ToArray(); }
        }

        public ShaderProgram()
        {
            _uniforms = new Dictionary<string, int>();
            _attachedShaders = new List<Shader>();

            Init();
        }

        public ShaderProgram(IEnumerable<Shader> shaders)
        {
            _attachedShaders = new List<Shader>();
            _uniforms = new Dictionary<string, int>();

            Init();
            AttachShaders(shaders);
            Update();
        }

        private void Init()
        {
            Id = GL.CreateProgram();
        }

        private void CacheUniforms(Shader shader)
        {
            IEnumerable<string> uniforms = shader.GetUniforms();
            List<string> lostUniforms = new List<string>();
            List<string> foundUniforms = new List<string>();

            foreach (string uniform in uniforms)
            {
                if (_uniforms.ContainsKey(uniform)) continue;

                int location;
                if ((location = GL.GetUniformLocation(Id, uniform)) != -1)
                {
                    _uniforms.Add(uniform, location);
                    foundUniforms.Add(uniform);
                }
                else lostUniforms.Add(uniform);
            }

            LogFoundUniforms(foundUniforms, shader);
            LogLostUniforms(lostUniforms, shader);
        }

        private void LogLostUniforms(IEnumerable<string> names, Shader shader)
        {
            if (!names.Any())
            {
                Logger.Log(new LogEntry("No uniforms lost", shader.Type.ToString(), "0 uniforms was removed by shader compiler", EntryType.Info, DateTime.Now - Process.GetCurrentProcess().StartTime));
                return;
            }

            StringBuilder builder = new StringBuilder($"{names.Count()} uniforms was removed by shader compiler: \r\n");
            foreach (string name in names) builder.Append($"{name} \r\n");

            Logger.Log(new LogEntry($"{names.Count()} uniforms lost", shader.Type.ToString(), builder.ToString(), EntryType.Info, DateTime.Now - Process.GetCurrentProcess().StartTime));
        }

        private void LogFoundUniforms(IEnumerable<string> names, Shader shader)
        {
            if (!names.Any())
            {
                Logger.Log(new LogEntry("No uniforms found", shader.Type.ToString(), "0 uniforms was found", EntryType.Info, DateTime.Now - Process.GetCurrentProcess().StartTime));
                return;
            }

            StringBuilder builder = new StringBuilder($"{names.Count()} uniforms was found: \r\n");
            foreach (string name in names) builder.Append($"{name} \r\n");

            Logger.Log(new LogEntry($"{names.Count()} uniforms found", shader.Type.ToString(), builder.ToString(), EntryType.Info, DateTime.Now - Process.GetCurrentProcess().StartTime));
        }

        private void AttachShaders(IEnumerable<Shader> shaders)
        {
            foreach (Shader shader in shaders)
            {
                _attachedShaders.Add(shader);
                GL.AttachShader(Id, shader.Id);
                CacheUniforms(shader);
            }
            Update();
        }

        public void AttachShader(Shader shader)
        {
            _attachedShaders.Add(shader);
            GL.AttachShader(Id, shader.Id);
            Update();
            CacheUniforms(shader);
        }

        public void SetUniform<T>(T value, string name) where T : IEquatable<T>
        {
            Bind();

            try
            {
                switch (value)
                {
                    case float x:
                        GL.Uniform1(_uniforms[name], x);
                        return;
                    case Vector2 x:
                        GL.Uniform2(_uniforms[name], x);
                        return;
                    case Vector3 x:
                        GL.Uniform3(_uniforms[name], x);
                        return;
                    case Vector4 x:
                        GL.Uniform4(_uniforms[name], x);
                        return;
                    case Matrix2 x:
                        GL.UniformMatrix2(_uniforms[name], false, ref x);
                        return;
                    case Matrix2x3 x:
                        GL.UniformMatrix2x3(_uniforms[name], 1, false, x.ToArray());
                        return;
                    case Matrix2x4 x:
                        GL.UniformMatrix2x4(_uniforms[name], 1, false, x.ToArray());
                        return;
                    case Matrix3x2 x:
                        GL.UniformMatrix3x2(_uniforms[name], 1, false, x.ToArray());
                        return;
                    case Matrix3 x:
                        GL.UniformMatrix3(_uniforms[name], false, ref x);
                        return;
                    case Matrix3x4 x:
                        GL.UniformMatrix3x4(_uniforms[name], 1, false, x.ToArray());
                        return;
                    case Matrix4x2 x:
                        GL.UniformMatrix4x3(_uniforms[name], 1, false, x.ToArray());
                        return;
                    case Matrix4x3 x:
                        GL.UniformMatrix4x3(_uniforms[name], 1, false, x.ToArray());
                        return;
                    case Matrix4 x:
                        GL.UniformMatrix4(_uniforms[name], false, ref x);
                        return;

                    default:
                        throw new ArgumentException("Type not supported!");
                }
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(e.ToString(),"SetUniform", e.Message, EntryType.Error, DateTime.Now - Process.GetCurrentProcess().StartTime));
            }

            //Unbind();
        }

        public void Update()
        {
            GL.LinkProgram(Id);
            GL.ValidateProgram(Id);
        }

        public override void Bind()
        {
            IsBound = true;
            GL.UseProgram(Id);
        }

        public override void Unbind()
        {
            IsBound = false;
            GL.UseProgram(0);
        }

        public override void Dispose()
        {
            GL.DeleteProgram(Id);
        }
    }
}
