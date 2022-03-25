using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL4;
using PixelFormat = OpenTK.Graphics.OpenGL4.PixelFormat;

namespace TKGraphics.GLComponents
{
    public class Texture : GLObject
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Filepath { get; private set; }
        public TextureSettings Settings { get; private set; }

        public Texture(string filepath, TextureSettings settings)
        {
            Settings = settings;
            Filepath = filepath;
            Init();
            Update(filepath);
        }

        public Texture()
        {
            Filepath = "";
            Settings = TextureSettings.DefaultSettings;
            Init();
        }

        private void Init()
        {
            Id = GL.GenTexture();
        }

        public void Update(string filepath)
        {
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)Settings.MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)Settings.MagFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)Settings.TextureWrapS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)Settings.TextureWrapT);

            using (Bitmap image = new Bitmap(filepath))
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Width = image.Width;
                Height = image.Height;

                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.MemoryBmp);
                    Bind();
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0,PixelFormat.Rgba, PixelType.UnsignedByte, stream.ToArray());
                }
            }

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(Settings.BlendingSFactor, Settings.BlendingDFactor);

            //Unbind();
        }

        public void Update(string filepath, TextureSettings settings)
        {
            Settings = settings;

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)Settings.MinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)Settings.MagFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)Settings.TextureWrapS);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)Settings.TextureWrapT);

            using (Bitmap image = new Bitmap(filepath))
            {
                image.RotateFlip(RotateFlipType.RotateNoneFlipY);
                Width = image.Width;
                Height = image.Height;

                using (MemoryStream stream = new MemoryStream())
                {
                    image.Save(stream, ImageFormat.MemoryBmp);
                    Bind();
                    GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, image.Width, image.Height, 0, PixelFormat.Rgba, PixelType.UnsignedByte, stream.ToArray());
                }
            }

            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(Settings.BlendingSFactor, Settings.BlendingDFactor);
        }

        public override void Bind()
        {
            IsBound = true;
            GL.BindTexture(TextureTarget.Texture2D, Id);
        }

        public void Bind(int slot)
        {
            IsBound = true;
            GL.ActiveTexture(TextureUnit.Texture0 + slot);
            GL.BindTexture(TextureTarget.Texture2D, Id);
        }

        public override void Unbind()
        {
            IsBound = false;
            GL.BindTexture(TextureTarget.Texture2D, Id);
        }

        public override void Dispose()
        {
            GL.DeleteTexture(Id);
        }
    }
}
