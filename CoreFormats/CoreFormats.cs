using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace KTXToolkit {/*
    class CoreTexelUnsignedNormalized8 : ITexel {
        public CoreTexelUnsignedNormalized8(uint count, uint glName_) {
            colorSet = new byte[count];
            glName = glName_;
        }
        byte[] colorSet;
        public double red {
            get {
                return colorSet[0] / 255.0;
            }
            set {
                colorSet[0] = (byte)( value * 255 );
            }
        }
        public double green {
            get {
                return colorSet[1] / 255.0;
            }
            set {
                colorSet[1] = (byte)( value * 255 );
            }
        }
        public double blue {
            get {
                return colorSet[2] / 255.0;
            }
            set {
                colorSet[2] = (byte)( value * 255 );
            }
        }
        public double alpha {
            get {
                return colorSet[3] / 255.0;
            }
            set {
                colorSet[3] = (byte)( value * 255 );
            }
        }
        public uint glName { get; set; }
    }

    class CoreTexelSignedNormalized8 : ITexel {
        public CoreTexelSignedNormalized8( uint count, uint glName_ ) {
            colorSet = new byte[count];
            glName = glName_;
        }
        byte[] colorSet;
        public double red {
            get {
                return ( colorSet[0] / 255.0 ) * 2.0 - 1.0;
            }
            set {
                colorSet[0] = (byte)( ( value + 1.0 ) / 2.0 * 255 );
            }
        }
        public double green {
            get {
                return ( colorSet[1] / 255.0 ) * 2.0 - 1.0;
            }
            set {
                colorSet[1] = (byte)( ( value + 1.0 ) / 2.0 * 255 );
            }
        }
        public double blue {
            get {
                return ( colorSet[2] / 255.0 ) * 2.0 - 1.0;
            }
            set {
                colorSet[2] = (byte)( ( value + 1.0 ) / 2.0 * 255 );
            }
        }
        public double alpha {
            get {
                return ( colorSet[3] / 255.0 ) * 2.0 - 1.0;
            }
            set {
                colorSet[3] = (byte)( ( value + 1.0 ) / 2.0 * 255 );
            }
        }
        public uint glName { get; set; }
    }
    class CoreTexelUnsigned8 : ITexel {
        public CoreTexelUnsigned8( uint count, uint glName_ ) {
            colorSet = new byte[count];
            glName = glName_;
        }
        byte[] colorSet;
        public double red {
            get {
                return colorSet[0];
            }
            set {
                if ( value > 255.0 ) {
                    colorSet[0] = 255;
                } else if ( value < 0 ) {
                    colorSet[0] = 0;
                } else {
                    colorSet[0] = (byte)value;
                }
            }
        }
        public double green {
            get {
                return colorSet[1];
            }
            set {
                if ( value > 255.0 ) {
                    colorSet[1] = 255;
                } else if ( value < 0 ) {
                    colorSet[1] = 0;
                } else {
                    colorSet[1] = (byte)value;
                }
            }
        }
        public double blue {
            get {
                return colorSet[2];
            }
            set {
                if ( value > 255.0 ) {
                    colorSet[2] = 255;
                } else if ( value < 0 ) {
                    colorSet[2] = 0;
                } else {
                    colorSet[2] = (byte)value;
                }
            }
        }
        public double alpha {
            get {
                return colorSet[3];
            }
            set {
                if ( value > 255.0 ) {
                    colorSet[3] = 255;
                } else if ( value < 0 ) {
                    colorSet[3] = 0;
                } else {
                    colorSet[3] = (byte)value;
                }
            }
        }
        public uint glName { get; set; }
    }

    class CoreTexelSigned8 : ITexel {
        public CoreTexelSigned8( uint count, uint glName_ ) {
            colorSet = new sbyte[count];
            glName = glName_;
        }
        sbyte[] colorSet;
        public double red {
            get {
                return colorSet[0];
            }
            set {
                if ( value > 127 ) {
                    colorSet[0] = 127;
                } else if ( value < -128 ) {
                    colorSet[0] = -128;
                } else {
                    colorSet[0] = (sbyte)value;
                }
            }
        }
        public double green {
            get {
                return colorSet[1];
            }
            set {
                if ( value > 127 ) {
                    colorSet[1] = 127;
                } else if ( value < -128 ) {
                    colorSet[1] = -128;
                } else {
                    colorSet[1] = (sbyte)value;
                }
            }
        }
        public double blue {
            get {
                return colorSet[2];
            }
            set {
                if ( value > 127 ) {
                    colorSet[1] = 127;
                } else if ( value < -128 ) {
                    colorSet[1] = -128;
                } else {
                    colorSet[1] = (sbyte)value;
                }
            }
        }
        public double alpha {
            get {
                return colorSet[3];
            }
            set {
                if ( value > 127 ) {
                    colorSet[1] = 127;
                } else if ( value < -128 ) {
                    colorSet[1] = -128;
                } else {
                    colorSet[1] = (sbyte)value;
                }
            }
        }
        public uint glName { get; set; }
    }

    class CoreTextureLayer<Type> where Type : ITexel {
        ITexel[][] texels;
        public void allocate( ITexture values, Func<Type> constructor ) {
            uint x = values.width;
            uint y = values.height;
            uint z = values.depth;

            switch ( values.type ) {
                case ImageTypes.Image1D:
                case ImageTypes.Image1DArray:
                    y = 1;
                    z = 1;
                    break;
                case ImageTypes.Image2D:
                case ImageTypes.Image2DArray:
                case ImageTypes.ImageCubeMap:
                case ImageTypes.ImageCubeMapArray:
                    z = 1;
                    break;
                case ImageTypes.Image3D:
                    break;
            }

            texels = new ITexel[values.mipmaps][];
            for ( uint m = 0; m < values.mipmaps; ++m ) {
                x /= 2;
                y /= 2;
                z /= 2;
                if ( x < 1 ) {
                    x = 1;
                }
                if ( y < 1 ) {
                    y = 1;
                }
                if ( z < 1 ) {
                    z = 1;
                }
                uint tc = x * y * z;
                texels[m] = new ITexel[tc];
                for ( uint t = 0; t < tc; ++t ) {
                    texels[m][t] = constructor();
                }
            }
        }
        public ITexel[] image( uint mipmap ) {
            if ( mipmap < texels.Length) {
                return texels[mipmap];
            }
            return null;
        }
    }

    class CoreTexture<Type> : ITexture where Type : ITexel {
        public CoreTexture(Func<Type> constr) {
            constructor = constr;
        }
        Func<Type> constructor;
        ImageTypes imageType;
        CoreTextureLayer<Type>[] texels;
        public ImageTypes type {
            get {
                return imageType;
            }
            set {
                if ( imageType != value ) {
                    texels = null;
                    imageType = value;
                }
            }
        }
        public uint width { get; set; }
        public uint height { get; set; }
        public uint depth { get; set; }
        public uint layers { get; set; }
        public uint mipmaps { get; set; }
        public ITexel[] image( uint mipmap, uint layer ) {
            if ( texels == null ) {
                switch ( imageType ) {
                    case ImageTypes.Image1D:
                    case ImageTypes.Image2D:
                    case ImageTypes.Image3D:
                        layers = 1;
                        break;

                    case ImageTypes.Image1DArray:
                    case ImageTypes.Image2DArray:
                        break;

                    case ImageTypes.ImageCubeMap:
                        layers = 6;
                        break;

                    case ImageTypes.ImageCubeMapArray:
                        layers = ( layers / 6 ) * 6;
                        break;
                }
                texels = new CoreTextureLayer<Type>[layers];
                foreach ( CoreTextureLayer<Type> texLayer in texels ) {
                    texLayer.allocate( this, constructor );
                }
            }
            return texels[layer].image( mipmap );
        }
    }

    class CoreTextureFormatRGBA8 : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Unsigned Int 32 Bit: R8G8B8A8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelUnsignedNormalized8>(() => new CoreTexelUnsignedNormalized8(4, glName ) );
        }
        public uint glName {
            get {
                return 0x8058;
            }
        }
    }

    class CoreTextureFormatRGB8 : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Unsigned Int 24 Bit: R8G8B8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelUnsignedNormalized8>( () => new CoreTexelUnsignedNormalized8( 3, glName ) );
        }
        public uint glName {
            get {
                return 0x8051;
            }
        }
    }

    class CoreTextureFormatRG8 : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Unsigned Int 16 Bit: R8G8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelUnsignedNormalized8>( () => new CoreTexelUnsignedNormalized8( 2, glName ) );
        }
        public uint glName {
            get {
                return 0x822B;
            }
        }
    }

    class CoreTextureFormatR8 : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Unsigned Int 8 Bit: R8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelUnsignedNormalized8>( () => new CoreTexelUnsignedNormalized8( 1, glName ) );
        }
        public uint glName {
            get {
                return 0x8229;
            }
        }
    }

    class CoreTextureFormatRGBA8S : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Int 32 Bit: R8G8B8A8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelSignedNormalized8>( () => new CoreTexelSignedNormalized8( 4, glName ) );
        }
        public uint glName {
            get {
                return 0x8F97;
            }
        }
    }

    class CoreTextureFormatRGB8S : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Int 24 Bit: R8G8B8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelSignedNormalized8>( () => new CoreTexelSignedNormalized8( 3, glName ) );
        }
        public uint glName {
            get {
                return 0x8F96;
            }
        }
    }

    class CoreTextureFormatRG8S : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Int 16 Bit: R8G8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelSignedNormalized8>( () => new CoreTexelSignedNormalized8( 2, glName ) );
        }
        public uint glName {
            get {
                return 0x8F95;
            }
        }
    }

    class CoreTextureFormatR8S : ITextureFormat {
        public override string ToString() { return name; }
        public string name {
            get {
                return "Int 8 Bit: R8";
            }
        }
        public ITexture create() {
            return new CoreTexture<CoreTexelSignedNormalized8>( () => new CoreTexelSignedNormalized8( 1, glName ) );
        }
        public uint glName {
            get {
                return 0x8F94;
            }
        }
    }
    */
    /*GL_RED, GL_RG, GL_RGB, GL_BGR, GL_RGBA, GL_BGRA, GL_RED_INTEGER, GL_RG_INTEGER, GL_RGB_INTEGER, GL_BGR_INTEGER, GL_RGBA_INTEGER, GL_BGRA_INTEGER, GL_STENCIL_INDEX, GL_DEPTH_COMPONENT, GL_DEPTH_STENCIL*/
    /*GL_UNSIGNED_BYTE, GL_BYTE, GL_UNSIGNED_SHORT, GL_SHORT, GL_UNSIGNED_INT, GL_INT, GL_FLOAT, GL_UNSIGNED_BYTE_3_3_2, GL_UNSIGNED_BYTE_2_3_3_REV, GL_UNSIGNED_SHORT_5_6_5, GL_UNSIGNED_SHORT_5_6_5_REV, GL_UNSIGNED_SHORT_4_4_4_4, GL_UNSIGNED_SHORT_4_4_4_4_REV, GL_UNSIGNED_SHORT_5_5_5_1, GL_UNSIGNED_SHORT_1_5_5_5_REV, GL_UNSIGNED_INT_8_8_8_8, GL_UNSIGNED_INT_8_8_8_8_REV, GL_UNSIGNED_INT_10_10_10_2, and GL_UNSIGNED_INT_2_10_10_10_REV.*/
    /*Sized Internal Format	Base Internal Format	Red Bits	Green Bits	Blue Bits	Alpha Bits	Shared Bits
GL_R8	GL_RED	8	 	 	 	 
GL_R8_SNORM	GL_RED	s8	 	 	 	 
GL_R16	GL_RED	16	 	 	 	 
GL_R16_SNORM	GL_RED	s16	 	 	 	 
GL_RG8	GL_RG	8	8	 	 	 
GL_RG8_SNORM	GL_RG	s8	s8	 	 	 
GL_RG16	GL_RG	16	16	 	 	 
GL_RG16_SNORM	GL_RG	s16	s16	 	 	 
GL_R3_G3_B2	GL_RGB	3	3	2	 	 
GL_RGB4	GL_RGB	4	4	4	 	 
GL_RGB5	GL_RGB	5	5	5	 	 
GL_RGB8	GL_RGB	8	8	8	 	 
GL_RGB8_SNORM	GL_RGB	s8	s8	s8	 	 
GL_RGB10	GL_RGB	10	10	10	 	 
GL_RGB12	GL_RGB	12	12	12	 	 
GL_RGB16_SNORM	GL_RGB	16	16	16	 	 
GL_RGBA2	GL_RGB	2	2	2	2	 
GL_RGBA4	GL_RGB	4	4	4	4	 
GL_RGB5_A1	GL_RGBA	5	5	5	1	 
GL_RGBA8	GL_RGBA	8	8	8	8	 
GL_RGBA8_SNORM	GL_RGBA	s8	s8	s8	s8	 
GL_RGB10_A2	GL_RGBA	10	10	10	2	 
GL_RGB10_A2UI	GL_RGBA	ui10	ui10	ui10	ui2	 
GL_RGBA12	GL_RGBA	12	12	12	12	 
GL_RGBA16	GL_RGBA	16	16	16	16	 
GL_SRGB8	GL_RGB	8	8	8	 	 
GL_SRGB8_ALPHA8	GL_RGBA	8	8	8	8	 
GL_R16F	GL_RED	f16	 	 	 	 
GL_RG16F	GL_RG	f16	f16	 	 	 
GL_RGB16F	GL_RGB	f16	f16	f16	 	 
GL_RGBA16F	GL_RGBA	f16	f16	f16	f16	 
GL_R32F	GL_RED	f32	 	 	 	 
GL_RG32F	GL_RG	f32	f32	 	 	 
GL_RGB32F	GL_RGB	f32	f32	f32	 	 
GL_RGBA32F	GL_RGBA	f32	f32	f32	f32	 
GL_R11F_G11F_B10F	GL_RGB	f11	f11	f10	 	 
GL_RGB9_E5	GL_RGB	9	9	9	 	5
GL_R8I	GL_RED	i8	 	 	 	 
GL_R8UI	GL_RED	ui8	 	 	 	 
GL_R16I	GL_RED	i16	 	 	 	 
GL_R16UI	GL_RED	ui16	 	 	 	 
GL_R32I	GL_RED	i32	 	 	 	 
GL_R32UI	GL_RED	ui32	 	 	 	 
GL_RG8I	GL_RG	i8	i8	 	 	 
GL_RG8UI	GL_RG	ui8	ui8	 	 	 
GL_RG16I	GL_RG	i16	i16	 	 	 
GL_RG16UI	GL_RG	ui16	ui16	 	 	 
GL_RG32I	GL_RG	i32	i32	 	 	 
GL_RG32UI	GL_RG	ui32	ui32	 	 	 
GL_RGB8I	GL_RGB	i8	i8	i8	 	 
GL_RGB8UI	GL_RGB	ui8	ui8	ui8	 	 
GL_RGB16I	GL_RGB	i16	i16	i16	 	 
GL_RGB16UI	GL_RGB	ui16	ui16	ui16	 	 
GL_RGB32I	GL_RGB	i32	i32	i32	 	 
GL_RGB32UI	GL_RGB	ui32	ui32	ui32	 	 
GL_RGBA8I	GL_RGBA	i8	i8	i8	i8	 
GL_RGBA8UI	GL_RGBA	ui8	ui8	ui8	ui8	 
GL_RGBA16I	GL_RGBA	i16	i16	i16	i16	 
GL_RGBA16UI	GL_RGBA	ui16	ui16	ui16	ui16	 
GL_RGBA32I	GL_RGBA	i32	i32	i32	i32	 
GL_RGBA32UI	GL_RGBA	ui32	ui32	ui32	ui32	*/
    public abstract class FromatBase {
        private string name;
        public override string ToString() {
            return name;
        }
        public FromatBase(UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_) {
            glType = type;
            glFormat = format;
            glInternalFormat = intFormat;
            glBaseInternalFormat = baseIntFormat;
            channelCount = channelCount_;
            align = align_;
            name = name_;
        }
        public UInt32 glType { get; set; }
        public UInt32 glFormat { get; set; }
        public UInt32 glInternalFormat { get; set; }
        public UInt32 glBaseInternalFormat { get; set; }
        public uint channelCount;
        public uint align;

        protected abstract double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel );

        private void ToGenericImagePixel( CoreTexture texture, ref GenericImage image, UInt32 level, UInt32 pixel ) {
            for ( uint channel = 0; channel < channelCount; ++channel ) {
                image.mipmapLevels[level].pixels[pixel * channelCount + channel] = ToGenericImagePixelAtIndex( texture.mipmapLevels[level].pixels, pixel, channel );
            }
        }

        private void ToGenericImageLevel( CoreTexture texture, ref GenericImage image, UInt32 level ) {
            UInt32 x = (UInt32)( (int)image.width >> (int)level );
            UInt32 y = (UInt32)( (int)image.height >> (int)level );
            UInt32 z = (UInt32)( (int)image.depth >> (int)level );
            x = x < 1 ? 1 : x;
            y = y < 1 ? 1 : y;
            z = z < 1 ? 1 : z;
            UInt32 pixels = image.faces * image.arrays * x * y * z;
            image.mipmapLevels[level] = new GenericImageMipmapLevel();
            image.mipmapLevels[level].pixels = new double[pixels * channelCount];
            for ( UInt32 pixel = 0; pixel < pixels; ++pixel ) {
                ToGenericImagePixel( texture, ref image, level, pixel );
            }
        }

        public GenericImage ToGenericImage( CoreTexture texture ) {
            GenericImage result = new GenericImage();
            result.width = texture.pixelWidth;
            result.height = texture.pixelHeight;
            result.depth = texture.pixelDepth != 0 ? texture.pixelDepth : 1;
            result.arrays = texture.numberOfArrayElements != 0 ? texture.numberOfArrayElements : 1;
            result.faces = texture.numberOfFaces;
            result.channels = channelCount;
            result.mipmapLevels = new GenericImageMipmapLevel[texture.mipmapLevels.Length];
            for ( UInt32 level = 0; level < texture.mipmapLevels.Length; ++level ) {
                ToGenericImageLevel( texture, ref result, level );
            }
            return result;
        }
        public CoreTexture ToCoreTexture( GenericImage image ) {
            return null;
        }
    }

    public class Format8UN : FromatBase, ITextureFormat {
        protected override double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel ) {
            byte v = bytes[pixel * align + channel];
            return (double)v / 255.0;
        }

        public Format8UN( UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_ )
         : base(type, format, intFormat, baseIntFormat, channelCount_, align_, name_ ) {}
    }

    public class Format16UN : FromatBase, ITextureFormat {
        protected override double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel ) {
            UInt16 v = BitConverter.ToUInt16( bytes, (int)( pixel * align + channel * 2 ) );
            return (double)v / UInt16.MaxValue;
        }

        public Format16UN( UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_ )
         : base( type, format, intFormat, baseIntFormat, channelCount_, align_, name_ ) { }
    }

    public class Format32UN : FromatBase, ITextureFormat {
        protected override double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel ) {
            UInt32 v = BitConverter.ToUInt32( bytes, (int)( pixel * align + channel * 4 ) );
            return (double)v / UInt32.MaxValue;
        }

        public Format32UN( UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_ )
         : base( type, format, intFormat, baseIntFormat, channelCount_, align_, name_ ) { }
    }

    public class Format8SN : FromatBase, ITextureFormat {
        protected override double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel ) {
            byte v = bytes[pixel * align + channel];
            return ( (double)v / 255.0 - 0.5 ) * 2.0;
        }

        public Format8SN( UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_ )
         : base( type, format, intFormat, baseIntFormat, channelCount_, align_, name_ ) { }
    }

    public class Format16SN : FromatBase, ITextureFormat {
        protected override double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel ) {
            Int16 v = BitConverter.ToInt16( bytes, (int)( pixel * align + channel * 2 ) );
            return (double)v / Int16.MaxValue;
        }

        public Format16SN( UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_ )
         : base( type, format, intFormat, baseIntFormat, channelCount_, align_, name_ ) { }
    }

    public class Format32SN : FromatBase, ITextureFormat {
        protected override double ToGenericImagePixelAtIndex( byte[] bytes, UInt32 pixel, uint channel ) {
            Int32 v = BitConverter.ToInt32( bytes, (int)( pixel * align + channel * 2 ) );
            return (double)v / Int32.MaxValue;
        }

        public Format32SN( UInt32 type, UInt32 format, UInt32 intFormat, UInt32 baseIntFormat, uint channelCount_, uint align_, string name_ )
         : base( type, format, intFormat, baseIntFormat, channelCount_, align_, name_ ) { }
    }

    class GL {
        public static UInt32 GL_BYTE = 0x1400;
        public static UInt32 GL_UNSIGNED_BYTE = 0x1401;
        public static UInt32 GL_SHORT = 0x1402;
        public static UInt32 GL_UNSIGNED_SHORT = 0x1403;
        public static UInt32 GL_INT = 0x1404;
        public static UInt32 GL_UNSIGNED_INT = 0x1405;
        public static UInt32 GL_FLOAT = 0x1406;
        public static UInt32 GL_2_BYTES = 0x1407;
        public static UInt32 GL_3_BYTES = 0x1408;
        public static UInt32 GL_4_BYTES = 0x1409;
        public static UInt32 GL_DOUBLE = 0x140A;
        public static UInt32 GL_COLOR_INDEX = 0x1900;
        public static UInt32 GL_STENCIL_INDEX = 0x1901;
        public static UInt32 GL_DEPTH_COMPONENT = 0x1902;
        public static UInt32 GL_RED = 0x1903;
        public static UInt32 GL_GREEN = 0x1904;
        public static UInt32 GL_BLUE = 0x1905;
        public static UInt32 GL_ALPHA = 0x1906;
        public static UInt32 GL_RGB = 0x1907;
        public static UInt32 GL_RGBA = 0x1908;
        public static UInt32 GL_LUMINANCE = 0x1909;
        public static UInt32 GL_LUMINANCE_ALPHA = 0x190A;
        public static UInt32 GL_ALPHA4 = 0x803B;
        public static UInt32 GL_ALPHA8 = 0x803C;
        public static UInt32 GL_ALPHA12 = 0x803D;
        public static UInt32 GL_ALPHA16 = 0x803E;
        public static UInt32 GL_LUMINANCE4 = 0x803F;
        public static UInt32 GL_LUMINANCE8 = 0x8040;
        public static UInt32 GL_LUMINANCE12 = 0x8041;
        public static UInt32 GL_LUMINANCE16 = 0x8042;
        public static UInt32 GL_LUMINANCE4_ALPHA4 = 0x8043;
        public static UInt32 GL_LUMINANCE6_ALPHA2 = 0x8044;
        public static UInt32 GL_LUMINANCE8_ALPHA8 = 0x8045;
        public static UInt32 GL_LUMINANCE12_ALPHA4 = 0x8046;
        public static UInt32 GL_LUMINANCE12_ALPHA12 = 0x8047;
        public static UInt32 GL_LUMINANCE16_ALPHA16 = 0x8048;
        public static UInt32 GL_INTENSITY = 0x8049;
        public static UInt32 GL_INTENSITY4 = 0x804A;
        public static UInt32 GL_INTENSITY8 = 0x804B;
        public static UInt32 GL_INTENSITY12 = 0x804C;
        public static UInt32 GL_INTENSITY16 = 0x804D;
        public static UInt32 GL_R3_G3_B2 = 0x2A10;
        public static UInt32 GL_RGB4 = 0x804F;
        public static UInt32 GL_RGB5 = 0x8050;
        public static UInt32 GL_RGB8 = 0x8051;
        public static UInt32 GL_RGB10 = 0x8052;
        public static UInt32 GL_RGB12 = 0x8053;
        public static UInt32 GL_RGB16 = 0x8054;
        public static UInt32 GL_RGBA2 = 0x8055;
        public static UInt32 GL_RGBA4 = 0x8056;
        public static UInt32 GL_RGB5_A1 = 0x8057;
        public static UInt32 GL_RGBA8 = 0x8058;
        public static UInt32 GL_RGB10_A2 = 0x8059;
        public static UInt32 GL_RGBA12 = 0x805A;
        public static UInt32 GL_RGBA16 = 0x805B;
    }

    [Export(typeof( IPlugin ))]
    class CoreFormatsPlugin : IPlugin {
        public ITextureFormat[] TextureFormats {
            get {
                List<ITextureFormat> fmts = new List<ITextureFormat>();
                fmts.Add( new Format8UN( GL.GL_UNSIGNED_BYTE, GL.GL_RGBA, GL.GL_RGBA8, GL.GL_RGBA, 4, 4, "RGBA UNSIGNED BYTE" ) );
                fmts.Add( new Format8UN( GL.GL_UNSIGNED_BYTE, GL.GL_RGB, GL.GL_RGB8, GL.GL_RGB, 3, 3, "RGB UNSIGNED BYTE" ) );
                fmts.Add( new Format8UN( GL.GL_UNSIGNED_SHORT, GL.GL_RGBA, GL.GL_RGBA16, GL.GL_RGBA, 4, 8, "RGBA UNSIGNED SHORT" ) );
                fmts.Add( new Format8UN( GL.GL_UNSIGNED_SHORT, GL.GL_RGB, GL.GL_RGB16, GL.GL_RGB, 3, 6, "RGB UNSIGNED SHORT" ) );
                return fmts.ToArray();
            }
        }
        public ITextureContainer[] TextureContainer {
            get {
                return null;
            }
        }
    }
}