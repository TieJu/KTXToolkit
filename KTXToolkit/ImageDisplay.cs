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
        private uint mipmap = 0;

        private void UpdateGenericImage() {
            genericTexture = null;
            foreach ( IPlugin plugin in PluginList ) {
                ITextureFormat[] fmtList = plugin.TextureFormats;
                if ( null == fmtList ) {
                    continue;
                }

                foreach ( ITextureFormat fmt in fmtList ) {
                    if ( texture.glType == 0 && texture.glFormat == 0 ) {
                        if ( fmt.glInternalFormat != texture.glInternalFormat ) {
                            continue;
                        }
                    } else {
                        if ( fmt.glType != texture.glType ) {
                            continue;
                        }

                        if ( fmt.glFormat != texture.glFormat ) {
                            continue;
                        }
                    }

                    genericTexture = fmt.ToGenericImage( texture );
                    if ( null != genericTexture ) {
                        return;
                    }
                }
            }

            MessageBox.Show( this, "Unsupported format ( 0x" 
                                   + texture.glType.ToString("X4") 
                                   + ", 0x" 
                                   + texture.glFormat.ToString("X4")
                                   + ", 0x"
                                   + texture.glInternalFormat.ToString("X4")
                                   + ")", "Unsupported Format", MessageBoxButtons.OK, MessageBoxIcon.Error );
        }

        private void BuildMipmaps() {
            List<IMipmapGenerator> mipmapGens = new List<IMipmapGenerator>();

            foreach ( IPlugin plugin in PluginList ) {
                if ( null == plugin.MipmapGenerators ) {
                    continue;
                }

                mipmapGens.AddRange( plugin.MipmapGenerators );
            }

            if ( mipmapGens.Count < 1 ) {
                MessageBox.Show(this, "No mipmap generators found", "Missing Mipmap Generators", MessageBoxButtons.OK,MessageBoxIcon.Error );
                return;
            }

            MipmapGen dlg = new MipmapGen( mipmapGens.ToArray() );
            dlg.ShowDialog(this);
            if ( null != dlg.Selected ) {
                dlg.Selected.BuildMipmaps( ref genericTexture );
                DisplayImageLayer( mipmap, layer );
            }
        }

        private void UpdateMenuItems() {
            if ( null != genericTexture ) {
                if ( genericTexture.mipmapLevels.Length > 1 ) {
                    if ( mipmapLevelToolStripMenuItem.DropDownItems.Count == genericTexture.mipmapLevels.Length ) {
                        return;
                    }

                    mipmapLevelToolStripMenuItem.Enabled = true;
                    for ( uint m = 0; m < genericTexture.mipmapLevels.Length; ++m ) {
                        ToolStripItem item = new ToolStripMenuItem();
                        item.AutoSize = true;
                        item.Text = m.ToString();
                        uint v = m;
                        item.Click += ( x, y ) => DisplayImageLayer( v, layer );
                        mipmapLevelToolStripMenuItem.DropDownItems.Add( item );
                    }
                } else {
                    mipmapLevelToolStripMenuItem.Enabled = false;
                    mipmapLevelToolStripMenuItem.DropDownItems.Clear();
                }

                if ( genericTexture.arrays > 1 ) {
                    if ( layerToolStripMenuItem.DropDownItems.Count == genericTexture.arrays ) {
                        return;
                    }

                    layerToolStripMenuItem.Enabled = true;
                    for ( uint l = 0; l < genericTexture.arrays; ++l ) {
                        ToolStripItem item = new ToolStripMenuItem();
                        item.AutoSize = true;
                        item.Text = l.ToString();
                        uint v = l;
                        item.Click += ( x, y ) => DisplayImageLayer( mipmap, v );
                        mipmapLevelToolStripMenuItem.DropDownItems.Add( item );
                    }
                } else {
                    layerToolStripMenuItem.Enabled = false;
                    layerToolStripMenuItem.DropDownItems.Clear();
                }
            } else {
                mipmapLevelToolStripMenuItem.Enabled = false;
                mipmapLevelToolStripMenuItem.DropDownItems.Clear();

                layerToolStripMenuItem.Enabled = false;
                layerToolStripMenuItem.DropDownItems.Clear();
            }
        }

        private void DisplayImageLayer( uint newMipmap, uint newLayer ) {
            if ( null == genericTexture ) {
                pictureBox.Hide();
                imageFail.Show();
                pictureBox.Image = null;
                UpdateMenuItems();
                return;
            }
            if ( newLayer < genericTexture.arrays ) {
                layer = newLayer;
            } else {
                layer = genericTexture.arrays - 1;
            }

            if ( newMipmap < genericTexture.mipmapLevels.Length ) {
                mipmap = newMipmap;
            } else {
                mipmap = (uint)genericTexture.mipmapLevels.Length;
            }

            int mW = Math.Max((int)genericTexture.width >> (int)mipmap, 1);
            int mH = Math.Max((int)genericTexture.height >> (int)mipmap, 1);
            Bitmap drawImage = new Bitmap( mW, mH );
            long layerSize = mW * mH * genericTexture.channels;
            long layerOffset = layerSize * layer;
            int[] rgba = new int[4] { 0, 0, 0, 255 };
            for ( int x = 0; x < drawImage.Width; ++x ) {
                for (int y = 0; y < drawImage.Height; ++y ) {
                    long offset = layerOffset + ( x + y * mW ) * genericTexture.channels;
                    for ( int c = 0; c < genericTexture.channels; ++c ) {
                        rgba[c] = (int)( genericTexture.mipmapLevels[mipmap].pixels[offset + c] * 255 );
                        rgba[c] = Math.Max( 0, Math.Min( 255, rgba[c] ) );
                    }
                    drawImage.SetPixel( x, y, Color.FromArgb( rgba[3], rgba[0], rgba[1], rgba[2] ) );
                }
            }
            pictureBox.Image = drawImage;
            pictureBox.Show();
            imageFail.Hide();
            UpdateMenuItems();
        }

        private void UpdateImageDisplay() {
            UpdateGenericImage();
            DisplayImageLayer( mipmap, layer );
        }

        public ImageDisplay( IPlugin[] plugins, CoreTexture texture_) {
            InitializeComponent();
            PluginList = plugins;
            texture = texture_;
            AutoSize = true;
            MinimumSize = new Size( 158, 98 );
            UpdateImageDisplay();
        }

        private void exitToolStripMenuItem_Click( object sender, EventArgs e ) {
            Close();
        }

        private void generateMipmapLevelsToolStripMenuItem_Click( object sender, EventArgs e ) {
            BuildMipmaps();
        }

        private void replaceImageToolStripMenuItem_Click( object sender, EventArgs e ) {

        }
    }
}
