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
        LobbyScreen lobby;
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
            else if (usernameTextBox.Text.Contains("~"))
            {
                MessageBox.Show("Names cannot contain ~");
            }
            else
            {
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];

                Hide();
                lobby = new LobbyScreen(true, ipAddress, usernameTextBox.Text);
                lobby.Show();
            }
        }
        private void joinGameButton_Click(object sender, EventArgs e)
        {
            if (usernameTextBox.Text == string.Empty)
            {
                MessageBox.Show("Please enter a username");
            }
            else if (usernameTextBox.Text.Contains("~"))
            {
                MessageBox.Show("Names cannot contain ~");
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
                    lobby = new LobbyScreen(false, ip, usernameTextBox.Text);
                    try
                    {
                        lobby.Show();
                    }
                    catch (Exception) { };
                }
            }
        }

        public void Update(int set)
        {
            lobby.Update(set);
        }
        public void RemovePlayer(int player, bool left)
        {
            lobby.RemovePlayer(player, left);
        }
        public void LeaveGame()
        {
            lobby.LeaveLobby();
        }
        public void Display(string msg)
        {
            lobby.Display(msg);
        }
        public void EndGame(int winner)
        {
            lobby.EndGame(winner);
        }
    }
}
