using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTXToolkit
{
    public class CoreTextureKeyValuePair {
        public string key;
        public string value;
    }

    public class CoreTextureMipmapLevel {
        public byte[] pixels;
    }

    public class CoreTexture {
        public UInt32 glType;
        public UInt32 glTypeSize;
        public UInt32 glFormat;
        public UInt32 glInternalFormat;
        public UInt32 glBaseInternalFormat;
        public UInt32 pixelWidth;
        public UInt32 pixelHeight;
        public UInt32 pixelDepth;
        public UInt32 numberOfArrayElements;
        public UInt32 numberOfFaces;
        public CoreTextureKeyValuePair[] keyValuePairs;
        public CoreTextureMipmapLevel[] mipmapLevels;
    }

    public interface ITextureContainer {
        string[] extensions { get; }
        CoreTexture Load(string path);
        void Store(string path, CoreTexture texture);
    }

    public class GenericImageMipmapLevel {
        public double[] pixels;
    }

    public class GenericImage {
        public uint width;
        public uint height;
        public uint depth;
        public uint arrays;
        public uint faces;
        public uint channels;
        public GenericImageMipmapLevel[] mipmapLevels;

        public double GetPixelChannel(uint mipmap_, uint array_, uint depth_, uint height_, uint width_, uint channel_ ) {
            uint w = (uint)Math.Max( 1, (int)width >> (int)mipmap_ );
            uint h = (uint)Math.Max( 1, (int)height >> (int)mipmap_ );
            uint d = (uint)Math.Max( 1, (int)depth >> (int)mipmap_ );
            if ( depth_ >= d || height_ >= h || width_ >= w || channel_ >= channels ) {
                return 0;
            }

            uint image = w * h * d * channels;
            uint offset = ( w * h * depth_ + w * height_ + width_ ) * channels + channel_;
            return mipmapLevels[mipmap_].pixels[array_ * image + offset];
        }

        public void SetPixelChannel( uint mipmap_, uint array_, uint depth_, uint height_, uint width_, uint channel_, double value_ ) {
            uint w = (uint)Math.Max( 1, (int)width >> (int)mipmap_ );
            uint h = (uint)Math.Max( 1, (int)height >> (int)mipmap_ );
            uint d = (uint)Math.Max( 1, (int)depth >> (int)mipmap_ );
            if ( depth_ >= d || height_ >= h || width_ >= w || channel_ >= channels ) {
                return;
            }

            uint image = w * h * d * channels;
            uint offset = ( w * h * depth_ + w * height_ + width_ ) * channels + channel_;
            mipmapLevels[mipmap_].pixels[array_ * image + offset] = value_;
        }
    }

    public interface ITextureFormat {
        UInt32 glType { get; }
        UInt32 glFormat { get; }
        UInt32 glInternalFormat { get; }
        UInt32 glBaseInternalFormat { get; }
        GenericImage ToGenericImage( CoreTexture texture );
        CoreTexture ToCoreTexture( GenericImage image );
    }

    public interface IMipmapGenerator {
        void BuildMipmaps(ref GenericImage image);
    }

    public interface IPlugin
    {
        ITextureFormat[] TextureFormats { get; }
        ITextureContainer[] TextureContainer { get; }
        IMipmapGenerator[] MipmapGenerators { get; }
    }
}
