using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Red_7_GUI
{
    public partial class MenuScreen : Form
    {
        public MenuScreen()
        {
            InitializeComponent();
        }
        private void exitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void hostGameButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a username");
            }
            else
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];

                Hide();
                LobbyScreen lobby = new LobbyScreen(true, ipAddress, usernameTextBox.Text);
                lobby.ShowDialog(this);
                lobby.Dispose();
                Show();
            }
        }
        private void joinGameButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a username");
            }
            else
            {
                var ipPopup = new IPPopup();
                ipPopup.ShowDialog(this);
                IPAddress ip = ipPopup.ip;
                ipPopup.Dispose();

                if (ip != default)
                {
                    Hide();
                    LobbyScreen lobby = new LobbyScreen(false, ip, usernameTextBox.Text);
                    try
                    {
                        lobby.ShowDialog(this);
                    }
                    catch (Exception) { };
                    lobby.Dispose();
                    Show();
                }
            }
        }
    }
}
