﻿using System;
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
            if ( newLayer < genericTexture.arrays ) {
                layer = newLayer;
            } else {
                layer = genericTexture.arrays - 1;
            }
            Bitmap drawImage = new Bitmap( (int)genericTexture.width, (int)genericTexture.height );
            long layerSize = genericTexture.width * genericTexture.height * genericTexture.channels;
            long layerOffset = layerSize * layer;
            int[] rgba = new int[4] { 0, 0, 0, 255 };
            for ( int x = 0; x < drawImage.Width; ++x ) {
                for (int y = 0; y < drawImage.Height; ++y ) {
                    long offset = layerOffset + ( x + y * genericTexture.width ) * genericTexture.channels;
                    for ( int c = 0; c < genericTexture.channels; ++c ) {
                        rgba[c] = (int)( genericTexture.mipmapLevels[0].pixels[offset + c] * 255 );
                        rgba[c] = rgba[c] > 255 ? 255 : rgba[c] < 0 ? 0 : rgba[c];
                    }
                    drawImage.SetPixel( x, y, Color.FromArgb( rgba[3], rgba[0], rgba[1], rgba[2] ) );
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
