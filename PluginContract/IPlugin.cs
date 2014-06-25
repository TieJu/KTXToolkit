using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTXToolkit
{
    public class CoreTextureKeyValuePair {
        public CoreTextureKeyValuePair(string key_, string value_) { key = key_; value = value_; }
        public string key;
        public string value;
    }

    public class CoreTextureMipmapLevel {
        public CoreTextureMipmapLevel() { }
        public CoreTextureMipmapLevel(long count) { pixels = new byte[count]; }
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

    public class GenericImageMipmapLevel {
        public GenericImageMipmapLevel() { }
        public GenericImageMipmapLevel(long count) { pixels = new double[count]; }
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

    public interface ITextureContainer {
        string[] extensions { get; }
        CoreTexture Load( string path );
        // texture and image contain the same image, for some containers its easyer to use image instead of texture
        void Store( string path, CoreTexture texture, GenericImage image );
    }

    public interface IGLDataFormat {
        UInt32 Value { get; }
        int PixelSize(int channels);
        byte[] ToCoreFormat(double[] values, int offset, int channel, int channels);
        double ToGenericFormat(byte[] data, int offset, int channel, int channels);
    }

    public interface IGLPixelFormat {
        UInt32 Value { get; }
        int Channels { get; }
        GenericImage ToGenericImage( CoreTexture texture, IGLDataFormat dataFormat );
        CoreTexture ToCoreTexture( GenericImage image, IGLDataFormat dataFormat );
    }

    public interface IGLInteralPixelFormat {
        UInt32 Value { get; }
        bool IsCompressed { get; }
        bool IsCompatible( IGLPixelFormat pixelFormat, IGLDataFormat dataFormat);
        GenericImage ToGenericImage( CoreTexture texture, IGLPixelFormat pixelFormat, IGLDataFormat dataFormat );
        CoreTexture ToCoreTexture( GenericImage image, IGLPixelFormat pixelFormat, IGLDataFormat dataFormat );
    }

    public interface IMipmapGenerator {
        void BuildMipmaps(ref GenericImage image);
    }

    public interface IPlugin
    {
        ITextureContainer[] TextureContainer { get; }
        IMipmapGenerator[] MipmapGenerators { get; }
        IGLInteralPixelFormat[] InternalPixelFormats { get; }
        IGLPixelFormat[] PixelFormats { get; }
        IGLDataFormat[] DataFormats { get; }
    }
}
