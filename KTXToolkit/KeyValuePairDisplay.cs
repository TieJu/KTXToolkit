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
    public partial class KeyValuePairDisplay : Form {
        public List<CoreTextureKeyValuePair> KeyValuePairs { get;set; }
        public KeyValuePairDisplay( CoreTextureKeyValuePair[] keyValuePairs ) {
            InitializeComponent();

            KeyValuePairs = new List<CoreTextureKeyValuePair>();
            KeyValuePairs.AddRange( keyValuePairs );

            foreach ( CoreTextureKeyValuePair pair in KeyValuePairs ) {
                dataGridViewKeyValuePairs.Rows.Add( pair.key, pair.value );
            }
        }

        private void buttonOK_Click( object sender, EventArgs e ) {
            KeyValuePairs.Clear();
            foreach ( DataGridViewRow row in dataGridViewKeyValuePairs.Rows ) {
                KeyValuePairs.Add( new CoreTextureKeyValuePair( (string)row.Cells[0].Value, (string)row.Cells[1].Value ) ); ;
            }
            Close();
        }

        private void buttonCancel_Click( object sender, EventArgs e ) {
            Close();
        }
    }
}
