using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Red_7_GUI
{
    public partial class IPPopup : Form
    {
        public string ip;
        public IPPopup()
        {
            InitializeComponent();
        }
        private void OkButton_Click(object sender, EventArgs e)
        {
            if (ipAdressTextBox.Text == string.Empty)
            {
                MessageBox.Show("Enter an IP adress");
            }
            else
            {
                ip = ipAdressTextBox.Text;
                Close();
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
