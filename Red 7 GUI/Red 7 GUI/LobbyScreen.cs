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
using System.Net.Sockets;
using System.IO;

namespace Red_7_GUI
{
    public partial class LobbyScreen : Form
    {
        private bool host;
        private string username;
        private IPAddress ip;

        public LobbyScreen(bool host, IPAddress ip, string username)
        {
            this.host = host;
            this.username = username;
            this.ip = ip;
            players = new string[4];
            InitializeComponent();
            startButton.Enabled = false;

            if (!host)
            {
                startServerButton.Enabled = false;
                actionCheckBox.Enabled = false;
                advancedCheckBox.Enabled = false;
                

                if (!ConnectToServer(ip, username))
                {
                    Close();
                }

                send = "hi";

                sender.RunWorkerAsync();

                send = "";
            }
            else
            {
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
            //open lobby help window
        }
        private void quitButton_Click(object sender, EventArgs e)
        {
            if (host)
            {
                //close server
            }
            else
            {
                //send disconnect message
            }
            Close();
        }
        private void startButton_Click(object sender, EventArgs e)
        {
            //start game
        }
        private void DecodeJoin(string data)
        {
            if (data[0] == 1)
            {
                MessageBox.Show("Unable to join - Game already started");
            }
            else if (data[0] == 2)
            {
                MessageBox.Show("Unable to join - Lobby full");
            }
            else
            {
                if (data[1] == '0')
                {
                    advanced = true;
                    advancedCheckBox.Checked = true;
                }
                else
                {
                    advanced = false;
                    advancedCheckBox.Checked = false;
                }
                if (data[2] == '0')
                {
                    actionRule = true;
                    actionCheckBox.Checked = true;
                }
                else
                {
                    actionRule = false;
                    actionCheckBox.Checked = false;
                }

                players = data.Substring(3).Split(',');
            }
        }
        #region Server
        private bool advanced;
        private bool actionRule;
        private string[] players;
        private int port = 56565;
        public string receive;
        public string send = "";
        private int numPlayers;
        StreamReader STR;
        StreamWriter STW;
        TcpClient client;
        TcpClient[] clients;
        StreamReader[] readers;
        StreamWriter[] writers;
        BackgroundWorker[] threads;
        private bool ConnectToServer(IPAddress ip, string username)
        {
            client = new TcpClient();
            IPEndPoint remoteEndpoint = new IPEndPoint(ip, port);

            try
            {
                client.Connect(remoteEndpoint);
                if (client.Connected)
                {
                    MessageBox.Show("Connected to server");
                    STW = new StreamWriter(client.GetStream());
                    STR = new StreamReader(client.GetStream());
                    STW.AutoFlush = true;

                    receiver.RunWorkerAsync();
                    sender.WorkerSupportsCancellation = true;
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }

            return false;
        }
        private void StartServer()
        {
            clients = new TcpClient[4];
            readers = new StreamReader[4];
            writers = new StreamWriter[4];
            threads = new BackgroundWorker[4];

            listener.RunWorkerAsync();
        }
        private void startServerButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            startServerButton.Enabled = false;
            StartServer();
        }
        private void sender_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                STW.WriteLine(send);
            }
            else
            {
                MessageBox.Show("Sending failed - no connection");
            }
            this.sender.CancelAsync();
        }
        private void receiver_DoWork(object sender, DoWorkEventArgs e)
        {
            while (client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    if (receive != "")
                    {
                        MessageBox.Show(receive);
                        receive = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void listener_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            while (true)
            {
                clients[numPlayers] = listener.AcceptTcpClient();
                readers[numPlayers] = new StreamReader(clients[numPlayers].GetStream());
                writers[numPlayers] = new StreamWriter(clients[numPlayers].GetStream());
                writers[numPlayers].AutoFlush = true;

                threads[numPlayers] = new BackgroundWorker();
                threads[numPlayers].WorkerSupportsCancellation = true;
                threads[numPlayers].DoWork += new DoWorkEventHandler(cReceiver);

                numPlayers++;
            }
        }
        private void cReceiver(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                for (int i = 0; i < clients.Length; i++)
                {
                    if (clients[i].Connected)
                    {
                        try
                        {
                            receive = readers[i].ReadLine();
                            if (receive != "")
                            {
                                MessageBox.Show(receive + " from " + i.ToString());
                                receive = "";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }
            }
        }
        #endregion
    }
}
