using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;

namespace Red_7_GUI
{
    public partial class IPPopup : Form
    {
        public IPAddress ip;
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
                try
                {
                    ip = IPAddress.Parse(ipAdressTextBox.Text);
                    Close();
                }
                catch (Exception)
                {
                    MessageBox.Show("Invalid IP address");
                }
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
