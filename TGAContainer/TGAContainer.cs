using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace KTXToolkit {
    public class TGAContainer : ITextureContainer {
        public override string ToString() {
            return "Truevision TGA";
        }
        public TGAContainer() {
            extensions = new string[] { ".tga" };
        }
        public string[] extensions { get; set; }

        /*
         * header
         * 1 byte id length
         * 1 byte color map type
         * 1 byte image type
         * 2 bytes color map offset
         * 2 bytes color map entry count
         * 1 byte color map bpp
         * 2 bytes xorg
         * 2 bytes yorg
         * 2 bytes width
         * 2 bytes height
         * 1 byte bpp
         * 1 byte desc
         */
        public class TGAImage {
            byte IDLength { get; set; }
            byte ColorMapType { get;set; }
            byte ImageType { get;set; }
            ushort ColorMapOffset { get;set; }
            ushort ColorMapEntryCount { get;set; }
            byte ColorMapBitsPerPixel { get;set; }
            ushort XOrigin { get;set; }
            ushort YOrigin { get;set; }
            ushort Width { get;set; }
            ushort Height { get;set; }
            byte BitsPerPixel { get;set; }
            byte DiscriptioField { get;set; }
            byte[] ImageID { get;set; }
            byte[] ColorMap { get;set; }
            byte[] ImageData { get;set; }
            byte[] UncompressedImage {
                get {
                    return ImageData;
                }
            }

            public TGAImage( string name ) {
                LoadFrom( name );
            }

            public TGAImage( GenericImage image ) {
                IDLength = 0;
                ColorMapType = 0;
                // currently only uncomrepssed export is supported
                ImageType = 2;
                ColorMapOffset = 0;
                ColorMapEntryCount = 0;
                ColorMapBitsPerPixel = 0;
                XOrigin = 0;
                YOrigin = 0;
                Width = (ushort)image.width;
                Height = (ushort)image.height;
                int channelCount = (int)Math.Max( 3, Math.Min( 4, image.channels ) );
                BitsPerPixel = (byte)( channelCount * 8 );
                DiscriptioField = 0;
                ImageID = new byte[0];
                ColorMap = new byte[0];
                ImageData = new byte[Width * Height * channelCount];
                for ( int y = 0; y < Height; ++y ) {
                    for ( int x = 0; x < Width; ++x ) {
                        for ( int c = 0; c < channelCount; ++c ) {
                            ImageData[c + channelCount * ( x + y * Width )] = (byte)( image.GetPixelChannel( 0, 0, 0, (uint)y, (uint)x, (uint)c ) * 255 );
                        }
                    }
                }
            }

            void ReadExtensionData( MemoryMappedViewAccessor view ) {

            }

            void ReadDeveloperData( MemoryMappedViewAccessor view ) {

            }

            void ReadFooter( MemoryMappedViewAccessor view ) {
            }

            void ReadImageData( MemoryMappedViewAccessor view ) {
                int ids = GetImageDataSize();
                ImageData = new byte[ids];
                view.ReadArray( GetImageDataStart(), ImageData, 0, ImageData.Length );
            }

            void ReadColorMap( MemoryMappedViewAccessor view ) {
                int cms = GetColorMapSize();
                ColorMap = new byte[cms];
                view.ReadArray( GetColorMapStart(), ColorMap, 0, ColorMap.Length );
            }

            int GetColorMapStart() {
                return 18 + ImageID.Length;
            }

            int GetColorMapSize() {
                if ( ColorMapType == 0 ) {
                    return 0;
                } else if ( ColorMapType == 1 ) {
                    int ew = ColorMapBitsPerPixel < 16 ? 16 : ColorMapBitsPerPixel;
                    return ( ColorMapEntryCount * ew + 7 ) / 8;
                } else {
                    return 0;
                }
            }

            int GetImageDataStart() {
                return GetColorMapSize() + GetColorMapStart();
            }

            int GetImageDataSize() {
                return ( Width * Height * BitsPerPixel ) / 8;
            }

            void ReadImageID( MemoryMappedViewAccessor view ) {
                ImageID = new byte[IDLength];
                if ( IDLength > 0 ) {
                    view.ReadArray( 18, ImageID, 0, IDLength );
                }
            }

            void ReadHeader( MemoryMappedViewAccessor view ) {
                IDLength = view.ReadByte( 0 );
                ColorMapType = view.ReadByte( 1 );
                ImageType = view.ReadByte( 2 );
                ColorMapOffset = view.ReadUInt16( 3 );
                ColorMapEntryCount = view.ReadUInt16( 5 );
                ColorMapBitsPerPixel = view.ReadByte( 7 );
                XOrigin = view.ReadUInt16( 8 );
                YOrigin = view.ReadUInt16( 10 );
                Width = view.ReadUInt16( 12 );
                Height = view.ReadUInt16( 14 );
                BitsPerPixel = view.ReadByte( 16 );
                DiscriptioField = view.ReadByte( 17 );
            }

            void LoadFrom( string path ) {
                using ( MemoryMappedFile file = MemoryMappedFile.CreateFromFile(path) ) {
                    using ( MemoryMappedViewAccessor view = file.CreateViewAccessor() ) {
                        ReadHeader( view );
                        ReadImageID( view );
                        ReadColorMap( view );
                        ReadImageData( view );
                        ReadFooter( view );
                        ReadDeveloperData( view );
                        ReadExtensionData( view );
                    }
                }
            }

            void WriteHeader( BinaryWriter file ) {
                file.Write( IDLength );
                file.Write( ColorMapType );
                file.Write( ImageType );
                file.Write( ColorMapOffset );
                file.Write( ColorMapEntryCount );
                file.Write( ColorMapBitsPerPixel );
                file.Write( XOrigin );
                file.Write( YOrigin );
                file.Write( Width );
                file.Write( Height );
                file.Write( BitsPerPixel );
                file.Write( DiscriptioField );
            }

            void WriteImageData( BinaryWriter file ) {
                file.Write( ImageData );
            }

            public void Save( string path ) {
                using ( FileStream file = new FileStream( path, FileMode.Create, FileAccess.Write ) ) {
                    using ( BinaryWriter fileWriter = new BinaryWriter( file )) {
                        WriteHeader( fileWriter );
                        WriteImageData( fileWriter );
                    }
                }
            }

            public void CopyTo( ref CoreTexture texture ) {
                texture.glType = 0x1401;
                texture.glTypeSize = 1;
                if ( BitsPerPixel > 24 ) {
                    texture.glBaseInternalFormat =
                    texture.glInternalFormat = 0x1908;
                    texture.glFormat = 0x80E1;
                } else {
                    texture.glBaseInternalFormat =
                    texture.glInternalFormat = 0x1907;
                    texture.glFormat = 0x80E0;
                }

                texture.pixelWidth = (UInt32)Width;
                texture.pixelHeight = (UInt32)Height;
                texture.pixelDepth = 0;
                texture.numberOfArrayElements = 0;
                texture.numberOfFaces = 1;
                texture.keyValuePairs = new CoreTextureKeyValuePair[0];
                texture.mipmapLevels = new CoreTextureMipmapLevel[1];
                texture.mipmapLevels[0] = new CoreTextureMipmapLevel();
                if ( BitsPerPixel >= 24 ) {
                    texture.mipmapLevels[0].pixels = UncompressedImage;
                } else {
                    // todo: use extended stuff...
                }
            }
        }

        public CoreTexture Load( string path ) {
            TGAImage image = new TGAImage( path );
            CoreTexture texture = new CoreTexture();
            image.CopyTo( ref texture );
            return texture;
        }
        // texture and image contain the same image, for some containers its easyer to use image instead of texture
        public void Store( string path, CoreTexture texture, GenericImage image ) {
            TGAImage tga = new TGAImage( image );
            tga.Save( path );
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
            TextureContainer = new ITextureContainer[] { new TGAContainer() };
            MipmapGenerators = new IMipmapGenerator[] { };
            InternalPixelFormats = new IGLInteralPixelFormat[] { };
            PixelFormats = new IGLPixelFormat[] { };
            DataFormats = new IGLDataFormat[] { };
        }
    }
}
