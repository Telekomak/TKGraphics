﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TKGraphics.Utilities.Logger;
using OpenTK.Graphics.OpenGL4;

namespace TKGraphics.GLComponents
{
    public class Shader : IDisposable
    {
        public int Id { get; private set; }
        public ShaderType Type { get; private set; }
        public string Source { get; private set; }

        public Shader(ShaderType type, string filePath)
        {
            Type = type;
            Source = ReadFile(filePath);

            Init(type);
            Compile(Source);
            LogCompileInfo(Source);
        }

        private void Init(ShaderType type)
        {
            Id = GL.CreateShader(type);
        }

        private void Compile(string source)
        {
            GL.ShaderSource(Id, source);
            GL.CompileShader(Id);
        }

        private string ReadFile(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                return reader.ReadToEnd();
            }
        }

        private void LogCompileInfo(string shader)
        {
            GL.GetShader(Id, ShaderParameter.CompileStatus, out int result);
            //ERROR: 0:10: 'uniform' : syntax error syntax error

            if (result == 0)
            {
                GL.GetShaderInfoLog(Id, out string log);
                Logger.Log(new SyntaxErrorEntry(DateTime.Now - Process.GetCurrentProcess().StartTime, Type.ToString(), $"{Type} compile error!", log, shader, Int32.Parse(Regex.Match(log, @"(?<=\:)([0 - 9] *?)(?=\:)").Value), 50));
            }
            else Logger.Log(new LogEntry($"{Type} compile successful", Type.ToString(), $"{Type} compiled successfully", EntryType.Success, DateTime.Now - Process.GetCurrentProcess().StartTime));
        }

        public IEnumerable<string> GetUniforms()
        {
            List<string> retData = new List<string>();
            MatchCollection names = Regex.Matches(Source, @"(?<=uniform .* )(.*)(?=\;)");

            foreach (Match match in names) retData.Add(match.Value);
            return retData;
        }

        public void Dispose()
        {
            GL.DeleteShader(Id);
        }
    }
}
