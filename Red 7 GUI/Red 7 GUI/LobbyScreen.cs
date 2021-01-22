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
        string[] players;
        public LobbyScreen(bool host, string ip, string username)
        {
            this.host = host;
            players = new string[4];
            InitializeComponent();

            //test connection - if no connection/ no host at that ip, display error message and close form

            if (!host)
            {
                actionCheckBox.Enabled = false;
                advancedCheckBox.Enabled = false;
                startButton.Enabled = false;

                //request player + rule info from host
            }
            else
            {
                Server server = new Server();
                players[0] = username;
            }

            UpdateLabels();
        }
        private void UpdateLabels()
        {
            player0Label.Text = players[0];
            player1Label.Text = players[1];
            player2Label.Text = players[2];
            player3Label.Text = players[3];
        }
        private void helpButton_Click(object sender, EventArgs e)
        {
            //open rules window
        }
        private void quitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            //start game
        }
    }
}
