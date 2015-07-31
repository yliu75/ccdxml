using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XMLExplorer {
    public partial class Form2:Form {
        string url = null;
        public Form2() {
            InitializeComponent();
        }

        private void button_comfirm_Click(object sender,EventArgs e) {
            url=textBox_url.Text;
            Hide();
            DialogResult=DialogResult.OK;
        }

        private void button_cancel_Click(object sender,EventArgs e) {
            Hide();
        }
    }
}
