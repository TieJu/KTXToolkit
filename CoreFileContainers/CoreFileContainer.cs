using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;

namespace KTXToolkit {
    public class CoreFileContainer : ITextureContainer {
        public override string ToString() {
            return "Image";
        }
        public string[] extensions {
            get {
                return new string[] { ".bmp", ".gif", ".exif", ".jpg", ".png", ".tiff" };
            }
        }

        public CoreTexture Load( string path ) {
            Bitmap image = new Bitmap( path );
            CoreTexture texture = new CoreTexture();
            texture.glTypeSize = 1;
            texture.glType = 0x1401;
            int channels = 0;
            switch ( image.PixelFormat ) {
                default:
                case System.Drawing.Imaging.PixelFormat.Undefined:
                    return null;
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb555:
                case System.Drawing.Imaging.PixelFormat.Format16bppRgb565:
                case System.Drawing.Imaging.PixelFormat.Format24bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    texture.glFormat = 0x1907;
                    texture.glInternalFormat = 0x1907;
                    texture.glBaseInternalFormat = 0x1907;
                    channels = 3;
                    break;
                case System.Drawing.Imaging.PixelFormat.Indexed:
                case System.Drawing.Imaging.PixelFormat.Format1bppIndexed:
                case System.Drawing.Imaging.PixelFormat.Format4bppIndexed:
                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                case System.Drawing.Imaging.PixelFormat.Gdi:
                case System.Drawing.Imaging.PixelFormat.Alpha:
                case System.Drawing.Imaging.PixelFormat.PAlpha:
                case System.Drawing.Imaging.PixelFormat.Format16bppArgb1555:
                case System.Drawing.Imaging.PixelFormat.Format32bppPArgb:
                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                case System.Drawing.Imaging.PixelFormat.Canonical:
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                    texture.glFormat = 0x1908;
                    texture.glInternalFormat = 0x1908;
                    texture.glBaseInternalFormat = 0x1908;
                    channels = 4;
                    break;
            }
            texture.pixelWidth = (UInt32)image.Width;
            texture.pixelHeight = (UInt32)image.Height;
            texture.pixelDepth = 0;
            texture.numberOfArrayElements = 0;
            texture.numberOfFaces = 1;
            texture.keyValuePairs = new CoreTextureKeyValuePair[0];
            texture.mipmapLevels = new CoreTextureMipmapLevel[1];
            texture.mipmapLevels[0] = new CoreTextureMipmapLevel( texture.pixelWidth * texture.pixelHeight * channels );
            byte[] pixBuf = new byte[] { 0, 0, 0, 1 };
            for ( int y = 0; y < texture.pixelHeight; ++y ) {
                for ( int x = 0; x < texture.pixelWidth; ++x ) {
                    Color color = image.GetPixel( x, y );
                    pixBuf[0] = color.R;
                    pixBuf[1] = color.G;
                    pixBuf[2] = color.B;
                    pixBuf[3] = color.A;
                    for ( int channel = 0; channel < channels; ++channel ) {
                        int offset = (int)( ( x + y * texture.pixelWidth ) * channels ) + channel;
                        texture.mipmapLevels[0].pixels[offset] = pixBuf[channel];
                    }
                }
            }
            return texture;
        }

        public void Store( string path, CoreTexture texture, GenericImage image ) {
            if ( image.depth > 1 ) {
                MessageBox.Show( "The image is a volumetric image, the container format can not store volumetric images"
                               , "Inconpatible image type"
                               , MessageBoxButtons.OK
                               , MessageBoxIcon.Error );
                return;
            }
            if ( image.arrays > 1 ) {
                MessageBox.Show( "The image is a array of images, the container format can not store an array of images"
                               , "Inconpatible image type"
                               , MessageBoxButtons.OK
                               , MessageBoxIcon.Error );
                return;
            }
            if ( image.faces > 1 ) {
                MessageBox.Show( "The image is a cubemap image, the container format can not store a cubemap image"
                               , "Inconpatible image type"
                               , MessageBoxButtons.OK
                               , MessageBoxIcon.Error );
                return;
            }
            if ( image.mipmapLevels.Length > 1 ) {
                MessageBox.Show( "The image has multiple mipmap levels, the container format does not support mipmap images, ony the first mipmap image is store"
                               , "Container can't store mipmaps"
                               , MessageBoxButtons.OK
                               , MessageBoxIcon.Information );
            }

            int[] buf = new int[] { 0, 0, 0, 255 };
            System.Drawing.Imaging.PixelFormat pfm = System.Drawing.Imaging.PixelFormat.Canonical;
            if ( image.channels <= 3 ) {
                pfm = System.Drawing.Imaging.PixelFormat.Format24bppRgb;
            }
            Bitmap bitmap = new Bitmap( (int)texture.pixelWidth, (int)texture.pixelHeight, pfm );
            for ( int y = 0; y < bitmap.Height; ++y ) {
                for ( int x = 0; x < bitmap.Width; ++x ) {
                    for ( int c = 0; c < image.channels; ++c ) {
                        buf[c] = (byte)( ( image.mipmapLevels[0].pixels[( x + y * image.width ) * image.channels + c] ) * 255 );
                    }
                    bitmap.SetPixel( x, y, Color.FromArgb( buf[3], buf[0], buf[1], buf[2] ) );
                }
            }

            bitmap.Save( path );
        }
    }

    [Export(typeof(IPlugin))]
    class KTXPlugin : IPlugin {
        public ITextureContainer[] TextureContainer { get; set; }
        public IMipmapGenerator[] MipmapGenerators { get; set; }
        public IGLInteralPixelFormat[] InternalPixelFormats { get; set; }
        public IGLPixelFormat[] PixelFormats { get; set; }
        public IGLDataFormat[] DataFormats { get; set; }

        KTXPlugin() {
            TextureContainer = new ITextureContainer[] { new CoreFileContainer() };
            MipmapGenerators = new IMipmapGenerator[] { };
            InternalPixelFormats = new IGLInteralPixelFormat[] { };
            PixelFormats = new IGLPixelFormat[] { };
            DataFormats = new IGLDataFormat[] { };
        }
    }
}
