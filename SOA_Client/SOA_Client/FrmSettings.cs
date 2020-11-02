using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SOA_Client
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();

            rb_xmlrpc.Checked = MySettings.Default.ProtocolXmlRpc;
            rb_soap.Checked = MySettings.Default.ProtocolSoap;
        }

        private void btn_Apply_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(rb_rest.Checked.ToString());
            MySettings.Default.ProtocolXmlRpc = rb_xmlrpc.Checked;
            MySettings.Default.ProtocolSoap = rb_soap.Checked;
            MySettings.Default.ProtocolRest = rb_rest.Checked;
            MySettings.Default.Save();

            this.Close();
        }
    }
}
