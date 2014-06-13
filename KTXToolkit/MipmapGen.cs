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
    public partial class MipmapGen : Form {
        public IMipmapGenerator Selected { get; set; }
        private IMipmapGenerator[] generators;
        public MipmapGen( IMipmapGenerator[] Generators ) {
            generators = Generators;
            InitializeComponent();

            comboBoxFilters.Items.AddRange( Generators );
            comboBoxFilters.SelectedIndex = 0;
        }

        private void buttonCancel_Click( object sender, EventArgs e ) {
            Close();
        }

        private void buttonOk_Click( object sender, EventArgs e ) {
            Selected = (IMipmapGenerator)comboBoxFilters.SelectedItem;
            Close();
        }
    }
}
