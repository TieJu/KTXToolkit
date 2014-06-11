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

namespace KTXToolkit
{
    public partial class CreateNewTextureForm : Form
    {
        private IPlugin[] PluginList;
        private void load_texture_format_list()
        {
            if ( PluginList == null)
            {
                return;
            }
            foreach (IPlugin plugin in PluginList)
            {
                ITextureFormat[] formats = plugin.TextureFormats;
                if ( null == formats ) {
                    continue;
                }
                foreach (ITextureFormat format in formats )
                {
                    comboBoxTextureFormats.Items.Add(format);
                }
            }
        }
        public CreateNewTextureForm(IPlugin[] plugins)
        {
            PluginList = plugins;
            InitializeComponent();
            load_texture_format_list();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void comboBoxTextureFormats_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void radioButtontexture1D_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = false;
            labelDepth.Enabled = false;
            labelArrayLayers.Enabled = false;
            textBoxHeight.Enabled = false;
            textBoxDepth.Enabled = false;
            textBoxArrayLayers.Enabled = false;
        }

        private void radioButtonTexture1DArray_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = false;
            labelDepth.Enabled = false;
            labelArrayLayers.Enabled = true;
            textBoxHeight.Enabled = false;
            textBoxDepth.Enabled = false;
            textBoxArrayLayers.Enabled = true;
        }

        private void radioButtonTexture2D_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = true;
            labelDepth.Enabled = false;
            labelArrayLayers.Enabled = false;
            textBoxHeight.Enabled = true;
            textBoxDepth.Enabled = false;
            textBoxArrayLayers.Enabled = false;
        }

        private void radioButtonTexture2DArray_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = true;
            labelDepth.Enabled = false;
            labelArrayLayers.Enabled = true;
            textBoxHeight.Enabled = true;
            textBoxDepth.Enabled = false;
            textBoxArrayLayers.Enabled = true;
        }

        private void radioButtonTexture3D_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = true;
            labelDepth.Enabled = true;
            labelArrayLayers.Enabled = false;
            textBoxHeight.Enabled = true;
            textBoxDepth.Enabled = true;
            textBoxArrayLayers.Enabled = false;
        }

        private void radioButtonTextureCubeMap_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = true;
            labelDepth.Enabled = false;
            labelArrayLayers.Enabled = false;
            textBoxHeight.Enabled = true;
            textBoxDepth.Enabled = false;
            textBoxArrayLayers.Enabled = false;
        }

        private void radioButtonTextureCubeMapArray_CheckedChanged(object sender, EventArgs e)
        {
            labelHeight.Enabled = true;
            labelDepth.Enabled = false;
            labelArrayLayers.Enabled = true;
            textBoxHeight.Enabled = true;
            textBoxDepth.Enabled = false;
            textBoxArrayLayers.Enabled = true;
        }
    }
}
