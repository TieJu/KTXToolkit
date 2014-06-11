using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KTXToolkit {
    public partial class ImageDisplay : Form {
        private IPlugin[] PluginList;
        private CoreTexture texture;
        private GenericImage genericTexture;
        private uint layer = 0;

        private void UpdateGenericImage() {
            genericTexture = null;
            foreach ( IPlugin plugin in PluginList ) {
                ITextureFormat[] fmtList = plugin.TextureFormats;
                if ( null == fmtList ) {
                    continue;
                }

                foreach ( ITextureFormat fmt in fmtList ) {
                    if ( fmt.glType != texture.glType ) {
                        continue;
                    }

                    if ( fmt.glFormat != texture.glFormat ) {
                        continue;
                    }

                    genericTexture = fmt.ToGenericImage( texture );
                    if ( null != genericTexture ) {
                        return;
                    }
                }
            }
        }

        private void BuildMipmaps() {

        }

        private void DisplayImageLayer( uint newLayer ) {
            if ( null == genericTexture ) {
                pictureBox.Hide();
                imageFail.Show();
                pictureBox.Image = null;
                return;
            }
            layer = newLayer;
            Bitmap drawImage = new Bitmap( (int)genericTexture.width, (int)genericTexture.height );
            int[] rgba = new int[4] { 0, 0, 0, 255 };
            for ( int x = 0; x < drawImage.Width; ++x ) {
                for (int y = 0; y < drawImage.Height; ++y ) {
                    long offset = ( x + y * genericTexture.width ) * genericTexture.channels;
                    for ( int c = 0; c < genericTexture.channels; ++c ) {
                        rgba[c] = (int)( genericTexture.mipmapLevels[0].pixels[offset + c] * 255 );
                    }
                    int r = rgba[0] > 255 ? 255 : rgba[0] < 0 ? 0 : rgba[0];
                    int g = rgba[1] > 255 ? 255 : rgba[1] < 0 ? 0 : rgba[1];
                    int b = rgba[2] > 255 ? 255 : rgba[2] < 0 ? 0 : rgba[2];
                    int a = rgba[3] > 255 ? 255 : rgba[3] < 0 ? 0 : rgba[3];
                    drawImage.SetPixel( x, y, Color.FromArgb( a, r, g, b ) );
                }
            }
            pictureBox.Image = drawImage;
            pictureBox.Show();
            imageFail.Hide();
        }

        private void UpdateImageDisplay() {
            UpdateGenericImage();
            DisplayImageLayer( layer );
        }

        public ImageDisplay( IPlugin[] plugins, CoreTexture texture_) {
            InitializeComponent();
            PluginList = plugins;
            texture = texture_;
            AutoSize = true;
            UpdateImageDisplay();
        }
    }
}
