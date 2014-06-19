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
                comboBoxInternalFormat.Items.AddRange( plugin.InternalPixelFormats );
                comboBoxDataType.Items.AddRange( plugin.DataFormats );
                comboBoxDataFormat.Items.AddRange( plugin.PixelFormats );
            }

            if ( comboBoxInternalFormat.Items.Count > 0 ) {
                comboBoxInternalFormat.SelectedIndex = 0;
            }

            if ( comboBoxDataType.Items.Count > 0 ) {
                comboBoxDataType.SelectedIndex = 0;
            }

            if ( comboBoxDataFormat.Items.Count > 0 ) {
                comboBoxDataFormat.SelectedIndex = 0;
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

        private void buttonCalculateMipLength_Click( object sender, EventArgs e ) {
            int max = textBoxWidth.IntValue;
            if ( textBoxHeight.Enabled ) {
                max = Math.Max( max, textBoxHeight.IntValue );
            }
            if ( textBoxDepth.Enabled ) {
                max = Math.Max( max, textBoxDepth.IntValue );
            }

            int level = 1;
            for ( ; max >> level > 0; ++level ) {

            }
            textBoxMipMaps.Text = level.ToString();
        }
    }
}
