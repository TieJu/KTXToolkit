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
