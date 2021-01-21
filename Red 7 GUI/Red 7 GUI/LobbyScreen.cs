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
    public partial class LobbyScreen : Form
    {
        bool host;
        public LobbyScreen(bool host, string ip)
        {
            this.host = host;
            InitializeComponent();

            if (!host)
            {
                actionCheckBox.Enabled = false;
                advancedCheckBox.Enabled = false;
                startButton.Enabled = false;
                //request player + rule info from host
            }
        }
    }
}
