using System;
using System.Reflection;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace KTXToolkit
{
    public partial class Form1 : Form
    {
        private ImageDisplay imageDisplay;
        private CoreTexture texture;
        private IPlugin[] PluginList;
        public Form1(IPlugin[] plugins)
        {
            PluginList = plugins;
            InitializeComponent();
        }

        private void DisplayTexture(string name) {
            if ( null != imageDisplay ) {
                imageDisplay.Close();
            }
            imageDisplay = new ImageDisplay(PluginList, texture);
            imageDisplay.Width = (int)texture.pixelWidth;
            imageDisplay.Height = (int)texture.pixelHeight;
            imageDisplay.Text = name;
            imageDisplay.Show( this );
        }

        private ITextureContainer GetContainerFromExtension(string extension) {
            foreach ( IPlugin plugin in PluginList ) {
                ITextureContainer[] containers = plugin.TextureContainer;
                if ( null == containers ) {
                    continue;
                }
                foreach ( ITextureContainer container in containers ) {
                    foreach ( string ext in container.extensions ) {
                        if ( ext == extension ) {
                            return container;
                        }
                    }
                }
            }
            return null;
        }

        private string BuildFileContainerFilter() {
            string filter = "";
            foreach ( IPlugin plugin in PluginList ) {
                ITextureContainer[] containers = plugin.TextureContainer;
                if ( null == containers ) {
                    continue;
                }
                foreach ( ITextureContainer container in containers ) {
                    string mask = "";
                    filter += container.ToString();
                    filter += "(";
                    foreach ( string ext in container.extensions ) {
                        filter += "*" + ext + ", ";
                        mask += "*" + ext + ";";
                    }
                    filter = filter.TrimEnd( ',', ' ' ) + ")|";
                    filter += mask.TrimEnd( ';' );
                }
            }
            return filter;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewTextureForm form = new CreateNewTextureForm(PluginList);
            form.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void openToolStripMenuItem1_Click( object sender, EventArgs e ) {
            OpenFileDialog ofdlg = new OpenFileDialog();

            ofdlg.Filter = BuildFileContainerFilter();

            if ( ofdlg.ShowDialog() == DialogResult.OK ) {
                string name = ofdlg.FileName;
                string ext = Path.GetExtension( name );
                ITextureContainer container = GetContainerFromExtension( ext );
                if ( container != null ) {
                    texture = container.Load( name );
                    DisplayTexture(name);
                }
            }
        }
    }
}
