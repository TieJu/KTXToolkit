using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.IO;

namespace KTXToolkit {
    public class TGAContainer : ITextureContainer {
        public override string ToString() {
            return "Truevision TGA";
        }
        public TGAContainer() {
            extensions = new string[] { ".tga" };
        }
        public string[] extensions { get; set; }

        private CoreTexture ReadFromBinaryReader( BinaryReader reader ) {
            return null;
        }

        public CoreTexture Load( string path ) {
            FileStream file = new FileStream( path, FileMode.Open, FileAccess.Read );
            if ( file == null ) {
                return null;
            }

            BinaryReader fileReader = new BinaryReader( file );
            CoreTexture tex = ReadFromBinaryReader( fileReader );
            file.Dispose();
            return tex;
        }
        // texture and image contain the same image, for some containers its easyer to use image instead of texture
        public void Store( string path, CoreTexture texture, GenericImage image ) {

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
