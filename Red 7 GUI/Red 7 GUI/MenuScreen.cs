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
                Hide();
                LobbyScreen lobby = new LobbyScreen(true, "-1", usernameTextBox.Text);
                lobby.ShowDialog(this);
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
                string ip = ipPopup.ip;
                ipPopup.Dispose();

                if (ip != string.Empty)
                {
                    Hide();
                    LobbyScreen lobby = new LobbyScreen(false, ip, usernameTextBox.Text);
                    lobby.ShowDialog(this);
                    Show();
                }
            }
        }
    }
}
