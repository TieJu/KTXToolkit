using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System;
using System.Text;

namespace KTXToolkit {
    class KTXContainer : ITextureContainer {
        public override string ToString() {
            return "KTX Image";
        }
        public string[] extensions {
            get {
                return new string[] { ".ktx" };
            }
        }

        bool validateHeader( BinaryReader reader ) {
            byte[] magic = new byte[12] { 0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A };
            foreach ( byte mb in magic ) {
                if ( reader.ReadByte() != mb ) {
                    return false;
                }
            }
            return true;
        }

        bool isLittleEndian( BinaryReader reader ) {
            byte leadingByte = reader.ReadByte();
            if ( leadingByte == 4 ) {
                for ( byte i = 3; i > 0; --i ) {
                    if ( i != reader.ReadByte() ) {
                        throw new InvalidDataException( "corrupted ktx header (endian field)" );
                    }
                }
                return false;
            } else if ( leadingByte == 1 ) {
                for ( byte i = 2; i <= 4; ++i ) {
                    if ( i != reader.ReadByte() ) {
                        throw new InvalidDataException( "corrupted ktx header (endian field)" );
                    }
                }
                return true;
            } else {
                throw new InvalidDataException( "corrupted ktx header (endian field)" );
            }
        }

        void readMipmapLevelFromBinaryReader( Func<UInt32> uintReader, Func<UInt32, byte[]> byteReader, ref CoreTexture resultTexture, UInt32 levelIndex ) {
            UInt32 imageSize = uintReader();
            imageSize = ( imageSize + 3 ) & ~(UInt32)3;
            resultTexture.mipmapLevels[levelIndex].pixels = byteReader( imageSize );
        }

        void readMipmapLevelsFromBinaryReader( Func<UInt32> uintReader, Func<UInt32, byte[]> byteReader, ref CoreTexture resultTexture ) {           
            for ( UInt32 level = 0; level < resultTexture.mipmapLevels.Length; ++level ) {
                resultTexture.mipmapLevels[level] = new CoreTextureMipmapLevel();
                readMipmapLevelFromBinaryReader( uintReader, byteReader, ref resultTexture, level );
            }
        }

        CoreTextureKeyValuePair readKeyValuePairFromBinaryReader( Func<UInt32> uintReader, Func<UInt32, byte[]> byteReader, out UInt32 bytesRead ) {
            UTF8Encoding transform = new UTF8Encoding(false, false);
            bytesRead = uintReader();
            uint padding = 3 - ( ( bytesRead + 3 ) % 4 );
            byte[] dataSet = byteReader( bytesRead );
            byteReader( padding );
            bytesRead += padding + 4;

            

            int count = 1;
            for ( ; count < dataSet.Length; ++count ) {
                if ( dataSet[count] == 0 ) {
                    break;
                }
            }

            int end = dataSet.Length - 1;
            for ( ; end > 0; --end ) {
                if ( dataSet[end] != 0 ) {
                    break;
                }
            }

            return new CoreTextureKeyValuePair( transform.GetString( dataSet, 0, count ), transform.GetString( dataSet, count + 1, end - count ) );
        }

        void readKeyValuePairsFromBinaryReader( Func<UInt32> uintReader, Func<UInt32, byte[]> byteReader, UInt32 bytesOfKeyValueData, ref CoreTexture resultTexture ) {
            List<CoreTextureKeyValuePair> keyValuePairs = new List<CoreTextureKeyValuePair>();

            while ( bytesOfKeyValueData > 4 ) {
                UInt32 bytesRead;
                keyValuePairs.Add( readKeyValuePairFromBinaryReader( uintReader, byteReader, out bytesRead ) );
                bytesOfKeyValueData -= bytesRead;
            }

            if ( bytesOfKeyValueData > 0 ) {
                // skip over rest
                byteReader( bytesOfKeyValueData );
            }

            resultTexture.keyValuePairs = keyValuePairs.ToArray();
        }

        CoreTexture readFromBinaryReader( Func<UInt32> uintReader, Func<UInt32, byte[]> byteReader ) {
            CoreTexture resultTexture = new CoreTexture();

            resultTexture.glType = uintReader();
            resultTexture.glTypeSize = uintReader();
            resultTexture.glFormat = uintReader();
            resultTexture.glInternalFormat = uintReader();
            resultTexture.glBaseInternalFormat = uintReader();
            resultTexture.pixelWidth = uintReader();
            resultTexture.pixelHeight = uintReader();
            resultTexture.pixelDepth = uintReader();
            resultTexture.numberOfArrayElements = uintReader();
            resultTexture.numberOfFaces = uintReader();
            UInt32 numberOfMipmapLevels = uintReader();
            UInt32 bytesOfKeyValueData = uintReader();
            resultTexture.mipmapLevels = new CoreTextureMipmapLevel[numberOfMipmapLevels == 0 ? 1 : numberOfMipmapLevels];

            readKeyValuePairsFromBinaryReader( uintReader, byteReader, bytesOfKeyValueData, ref resultTexture );

            readMipmapLevelsFromBinaryReader( uintReader, byteReader, ref resultTexture );

            return resultTexture;
        }

        static byte[] readBytesInOrder(BinaryReader reader, UInt32 bytes ) {
            return reader.ReadBytes( (int)bytes );
        }

        static byte[] readBytesreverseOrder(BinaryReader reader, UInt32 bytes ) {
            byte[] result = reader.ReadBytes( (int)bytes );
            Array.Reverse( result );
            return result;
        }

        static UInt32 readUInt32InOrder(BinaryReader reader ) {
            return reader.ReadUInt32();
        }

        static UInt32 readUInt32ReverseOrder(BinaryReader reader) {
            byte[] bytes = BitConverter.GetBytes( reader.ReadUInt32() );
            Array.Reverse( bytes );
            return BitConverter.ToUInt32( bytes, 0 );
        }

        CoreTexture readFromBinaryReader(BinaryReader reader) {
            if ( !validateHeader( reader ) ) {
                return null;
            }

            if ( isLittleEndian(reader) && BitConverter.IsLittleEndian ) {
                return readFromBinaryReader( () => readUInt32InOrder(reader), x => readBytesInOrder(reader, x) );
            } else {
                return readFromBinaryReader( () => readUInt32ReverseOrder(reader), x => readBytesreverseOrder(reader, x) );
            }
        }

        public CoreTexture Load( string path ) {
            FileStream file = new FileStream( path, FileMode.Open, FileAccess.Read );
            if ( file == null) {
                return null;
            }

            BinaryReader fileReader = new BinaryReader( file );
            CoreTexture tex = readFromBinaryReader( fileReader );
            file.Dispose();
            return tex;
        }

        void WriteImages( BinaryWriter writer, CoreTexture texture ) {
            foreach ( CoreTextureMipmapLevel mip in texture.mipmapLevels ) {
                writer.Write( (UInt32)mip.pixels.Length );
                writer.Write( mip.pixels );
                int padding = 3 - ( ( mip.pixels.Length + 3 ) % 4 );
                for ( int i = 0; i < padding; ++i ) {
                    writer.Write( (byte)0 );
                }
            }
        }

        byte[] WriteKeyValuePairsToBuffer( CoreTexture texture ) {
            UTF8Encoding transform = new UTF8Encoding( false, false );
            BinaryWriter writer = new BinaryWriter( new MemoryStream() );

            foreach ( CoreTextureKeyValuePair pair in texture.keyValuePairs ) {
                byte[] keyInBytes = transform.GetBytes( pair.key );
                byte[] valueInBytes = transform.GetBytes( pair.value );
                UInt32 len = (UInt32)( keyInBytes.Length + 1 + valueInBytes.Length + 1 );
                writer.Write( len );
                writer.Write( keyInBytes );
                writer.Write( (byte)0 );
                writer.Write( valueInBytes );
                writer.Write( (byte)0 );
                uint padding = 3 - ( ( len + 3 ) % 4 );
                for ( uint p = 0; p < padding; ++p ) {
                    writer.Write( (byte)0 );
                }
            }
            return ( (MemoryStream)writer.BaseStream ).ToArray();
        }

        void WriteKeyValuePairs( BinaryWriter writer, CoreTexture texture ) {
            if ( texture.keyValuePairs.Length == 0 ) {
                writer.Write( (UInt32)0 );
            } else {
                byte[] buffer = WriteKeyValuePairsToBuffer( texture );
                writer.Write( (UInt32)buffer.Length );
                writer.Write( buffer );
            }
        }

        void WriteHeader(BinaryWriter writer, CoreTexture texture ) {
            writer.Write( new byte[12] { 0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A } );
            for ( byte b = 1; b <= 4; ++b ) {
                writer.Write( b );
            }
            writer.Write( texture.glType );
            writer.Write( texture.glTypeSize );
            writer.Write( texture.glFormat );
            writer.Write( texture.glInternalFormat );
            writer.Write( texture.glBaseInternalFormat );
            writer.Write( texture.pixelWidth );
            writer.Write( texture.pixelHeight );
            writer.Write( texture.pixelDepth );
            writer.Write( texture.numberOfArrayElements );
            writer.Write( texture.numberOfFaces );
            writer.Write( (UInt32)texture.mipmapLevels.Length );
        }

        void WriteToBinaryWriter(BinaryWriter writer, CoreTexture texture ) {
            WriteHeader( writer, texture );
            WriteKeyValuePairs( writer, texture );
            WriteImages( writer, texture );
        }

        public void Store( string path, CoreTexture texture, GenericImage image ) {
            FileStream file = new FileStream( path, FileMode.Create, FileAccess.Write );
            if ( file == null ) {
                return;
            }

            BinaryWriter fileWriter = new BinaryWriter( file );
            WriteToBinaryWriter( fileWriter, texture );
            file.Flush();
            file.Dispose();
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
            TextureContainer = new ITextureContainer[] { new KTXContainer() };
            MipmapGenerators = new IMipmapGenerator[] { };
            InternalPixelFormats = new IGLInteralPixelFormat[] { };
            PixelFormats = new IGLPixelFormat[] { };
            DataFormats = new IGLDataFormat[] { };
        }
    }
}
