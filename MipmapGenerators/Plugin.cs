using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;

namespace KTXToolkit {
    [Export(typeof(IPlugin))]
    public class Plugin : IPlugin {
        public abstract class MipmapGenBase {
            protected abstract double ApplyFilter( uint x, uint y, uint z, uint mipmap, uint layer, uint channel, ref GenericImage image );
            public uint MipmapCount( GenericImage image ) {
                uint max = Math.Max( image.width, Math.Max( image.height, image.depth ) );
                uint level = 1;
                while ( ( (int)max >> (int)level ) > 0 ) {
                    ++level;
                }
                return level;
            }

            public void BuildMipmap( ref GenericImage image, uint mipmap ) {
                uint x = (uint)Math.Max( 1, ( (int)image.width >> (int)mipmap ) );
                uint y = (uint)Math.Max( 1, ( (int)image.height >> (int)mipmap ) );
                uint z = (uint)Math.Max( 1, ( (int)image.depth >> (int)mipmap ) );

                uint x2 = (uint)Math.Max( 1, ( (int)image.width >> (int)( mipmap - 1 ) ) );
                uint y2 = (uint)Math.Max( 1, ( (int)image.height >> (int)( mipmap - 1 ) ) );
                uint z2 = (uint)Math.Max( 1, ( (int)image.depth >> (int)( mipmap - 1 ) ) );

                uint dX = x2 / x;
                uint dY = y2 / y;
                uint dZ = z2 / z;

                uint valueCount = x * y * z * image.arrays * image.channels;
                image.mipmapLevels[mipmap].pixels = new double[valueCount];

                for ( uint l = 0; l < image.arrays; ++l ) {
                    for ( uint d = 0; d < z; ++d ) {
                        for ( uint h = 0; h < y; ++h ) {
                            for ( uint w = 0; w < x; ++w ) {
                                for ( uint c = 0; c < image.channels; ++c ) {
                                    image.SetPixelChannel( mipmap, l, d, h, w, c, ApplyFilter( w, h, d, mipmap, l, c, ref image ) );
                                }
                            }
                        }
                    }
                }
            }

            public void BuildMipmaps( ref GenericImage image ) {
                uint levels = MipmapCount( image );
                if ( image.mipmapLevels.Length != levels ) {
                    GenericImageMipmapLevel[] newLevels = new GenericImageMipmapLevel[levels];
                    newLevels[0] = image.mipmapLevels[0];
                    image.mipmapLevels = newLevels;
                }

                for ( uint m = 1; m < image.mipmapLevels.Length; ++m ) {
                    image.mipmapLevels[m] = new GenericImageMipmapLevel();
                    BuildMipmap( ref image, m );
                }
            }
        }

        public class MipmapGenBox : MipmapGenBase, IMipmapGenerator {
            public override string ToString() {
                return "Box Filter";
            }

            protected override double ApplyFilter( uint x_, uint y_, uint z_, uint mipmap, uint layer, uint channel, ref GenericImage image ) {
                uint x = (uint)Math.Max( 1, ( (int)image.width >> (int)mipmap ) );
                uint y = (uint)Math.Max( 1, ( (int)image.height >> (int)mipmap ) );
                uint z = (uint)Math.Max( 1, ( (int)image.depth >> (int)mipmap ) );

                uint x2 = (uint)Math.Max( 1, ( (int)image.width >> (int)( mipmap - 1 ) ) );
                uint y2 = (uint)Math.Max( 1, ( (int)image.height >> (int)( mipmap - 1 ) ) );
                uint z2 = (uint)Math.Max( 1, ( (int)image.depth >> (int)( mipmap - 1 ) ) );

                uint dX = x2 / x;
                uint dY = y2 / y;
                uint dZ = z2 / z;

                double value = 0;
                uint count = 0;

                for ( uint d = 0; d < dZ; ++d ) {
                    for ( uint h = 0; h < dY; ++h ) {
                        for ( uint w = 0; w < dX; ++w ) {
                            ++count;
                            value += image.GetPixelChannel( mipmap - 1, layer, z_ * dZ + d, y_ * dY + h, x_ * dX + w, channel );
                        }
                    }
                }
                if ( count > 0 ) {
                    return value / count;
                } else {
                    return 0;
                }
            }
        }

        public ITextureFormat[] TextureFormats {
            get {
                return null;
            }
        }
        public ITextureContainer[] TextureContainer {
            get {
                return null;
            }
        }
        public IMipmapGenerator[] MipmapGenerators {
            get {
                return new IMipmapGenerator[1] { new MipmapGenBox() };
            }
        }
    }
}
