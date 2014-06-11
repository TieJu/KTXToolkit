﻿using System.Collections.Generic;
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
            CoreTextureKeyValuePair pair = new CoreTextureKeyValuePair();
            UInt32 bi = 0;
            List<byte> byteBuffer = new List<byte>();
            for ( ; bi < bytesRead; ++bi ) {
                byte bv = byteReader(1)[0];
                if ( bv == 0 ) {
                    break;
                }
                byteBuffer.Add( bv );
            }
            pair.key = transform.GetString( byteBuffer.ToArray() );

            byteBuffer.Clear();
            for ( ; bi < bytesRead; ++bi ) {
                byte bv = byteReader( 1 )[0];
                if ( bv == 0 ) {
                    break;
                }
                byteBuffer.Add( bv );
            }
            pair.value = transform.GetString( byteBuffer.ToArray() );

            bytesRead += 3 - ( ( bytesRead + 3 ) % 4 );
            for ( ;bi < bytesRead; ++bi ) {
                byte b = byteReader( 1 )[0];
            }
            return pair;
        }

        void readKeyValuePairsFromBinaryReader( Func<UInt32> uintReader, Func<UInt32, byte[]> byteReader, UInt32 bytesOfKeyValueData, ref CoreTexture resultTexture ) {
            List<CoreTextureKeyValuePair> keyValuePairs = new List<CoreTextureKeyValuePair>();
            /*
            while ( bytesOfKeyValueData > 0 ) {
                UInt32 bytesRead;
                keyValuePairs.Add( readKeyValuePairFromBinaryReader( uintReader, byteReader, out bytesRead ) );
                bytesOfKeyValueData -= bytesRead;
            }*/
            byteReader( bytesOfKeyValueData );

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

        CoreTexture readFromBinaryReaderBigEndian( BinaryReader reader ) {
            return null;
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
            return readFromBinaryReader( fileReader );
        }

        void WriteHeaderToBinaryWriter(BinaryWriter writer, CoreTexture texture ) {
            writer.Write( new byte[12] { 0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A } );
            for ( byte b = 1; b <= 4; ++b ) {
                writer.Write( b );
            }
        }

        void WriteToBinaryWriter(BinaryWriter writer, CoreTexture texture ) {
            WriteHeaderToBinaryWriter( writer, texture );
        }

        public void Store( string path, CoreTexture texture ) {
            FileStream file = new FileStream( path, FileMode.Create, FileAccess.Write );
            if ( file == null ) {
                return;
            }

            BinaryWriter fileWriter = new BinaryWriter( file );
            WriteToBinaryWriter( fileWriter, texture );
        }
    }

    [Export(typeof(IPlugin))]
    class KTXPlugin : IPlugin {
        public ITextureFormat[] TextureFormats {
            get {
                return null;
            }
        }
        public ITextureContainer[] TextureContainer {
            get {
                return new ITextureContainer[1] { new KTXContainer() };
            }
        }
    }
}