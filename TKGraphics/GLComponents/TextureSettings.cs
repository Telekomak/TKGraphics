using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace TKGraphics.GLComponents
{
    public struct TextureSettings
    {
        public static TextureSettings DefaultSettings{
            get
            {
                return new TextureSettings(
                    TextureTarget.Texture2D,
                    TextureMagFilter.Linear,
                    TextureMinFilter.Linear,
                    TextureWrapMode.ClampToEdge,
                    TextureWrapMode.ClampToEdge,
                    BlendingFactor.SrcAlpha,
                    BlendingFactor.OneMinusSrcAlpha
                    );
            }
        }

        public TextureTarget TextureTarget { get; set; }
        public TextureMagFilter MagFilter { get; set; }
        public TextureMinFilter MinFilter { get; set; }
        public TextureWrapMode TextureWrapS { get; set; }
        public TextureWrapMode TextureWrapT { get; set; }
        public BlendingFactor BlendingSFactor { get; set; }
        public BlendingFactor BlendingDFactor { get; set; }

        public TextureSettings(TextureTarget textureTarget, TextureMagFilter magFilter, TextureMinFilter minFilter, TextureWrapMode textureWrapS, TextureWrapMode textureWrapT, BlendingFactor blendingSFactor, BlendingFactor blendingDFactor)
        {
            TextureTarget = textureTarget;
            MagFilter = magFilter;
            MinFilter = minFilter;
            TextureWrapS = textureWrapS;
            TextureWrapT = textureWrapT;
            BlendingSFactor = blendingSFactor;
            BlendingDFactor = blendingDFactor;
        }
    }
}
