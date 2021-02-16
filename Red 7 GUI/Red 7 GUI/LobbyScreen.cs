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
using System.Threading;

namespace Red_7_GUI
{
    public partial class LobbyScreen : Form
    {
        private bool host;
        private string username;
        private IPAddress ip;
        private bool advanced;
        private bool actionRule;
        private int port = 56565;
        public string receive;
        public string send = "";
        private bool gameStarted;
        public LobbyScreen(bool host, IPAddress ip, string username)
        {
            this.host = host;
            this.username = username;
            clientPlayers = new string[4];
            gameStarted = false;
            InitializeComponent();
            startButton.Enabled = false;
            UpdateLabels();

            if (!host)
            {
                this.ip = ip;
                startServerButton.Enabled = false;
                actionCheckBox.Enabled = false;
                advancedCheckBox.Enabled = false;
                

                if (!ConnectToServer())
                {
                    Close();
                }

                send = "0" + username;
                sender.RunWorkerAsync();
                Thread.Sleep(200);
                send = "";
            }
            else
            {
                IPHostEntry hostInfo = Dns.GetHostEntry("localhost");
                this.ip = hostInfo.AddressList[hostInfo.AddressList.Length - 1];
                player0Label.Text = username;
            }

            //MessageBox.Show(this.ip.ToString());
        }
        #region Lobby
        private void UpdateLabels()
        {
            player0Label.Text = clientPlayers[0];
            player1Label.Text = clientPlayers[1];
            player2Label.Text = clientPlayers[2];
            player3Label.Text = clientPlayers[3];
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
        #endregion
        #region Client
        private StreamReader STR;
        private StreamWriter STW;
        private TcpClient client;
        private string[] clientPlayers;
        private int numPlayers = 0;
        private bool ConnectToServer()
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
        private void sender_DoWork(object sender, DoWorkEventArgs e)
        {
            if (client.Connected)
            {
                STW.WriteLine(send);
                MessageBox.Show("Sent " + send);
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
                        MessageBox.Show("Received " + receive);
                        ClientDecode(receive);
                        receive = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
        private void ClientDecode(string data)//triggers when a player joins the lobby
        {
            switch (data[0])
            {
                case '0'://join (username)
                    clientPlayers[numPlayers] = data.Substring(1);
                    numPlayers++;
                    UpdateLabels();
                    break;
                case '1'://leave
                    break;
                case '2'://rule update
                    break;
                case '3'://end turn
                    break;
                case '4'://
                    break;
                default:
                    MessageBox.Show("Invalid transmission type at server");
                    break;
            }
        }
        #endregion
        #region Server
        private TcpClient[] clients;
        private StreamReader[] readers;
        private StreamWriter[] writers;
        private BackgroundWorker[] threads;
        private string[] serverPlayers;
        private int numClients;
        private void StartServer()
        {
            serverPlayers = new string[4];
            clients = new TcpClient[4];
            readers = new StreamReader[4];
            writers = new StreamWriter[4];
            threads = new BackgroundWorker[4];
            numClients = 0;

            listener.RunWorkerAsync();
            serverReceiver.RunWorkerAsync();

            ConnectToServer();//connects host to the server

            send = "0" + username;
            sender.RunWorkerAsync();
            Thread.Sleep(200);
            send = "";
        }
        private void startServerButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = true;
            startServerButton.Enabled = false;
            StartServer();
        }
        private void listener_DoWork(object sender, DoWorkEventArgs e)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            while (numClients < 4 && !gameStarted)
            {
                clients[numClients] = listener.AcceptTcpClient();
                readers[numClients] = new StreamReader(clients[numClients].GetStream());
                writers[numClients] = new StreamWriter(clients[numClients].GetStream());
                writers[numClients].AutoFlush = true;

                //threads[numClients] = new BackgroundWorker();
                //threads[numClients].WorkerSupportsCancellation = true;
                //threads[numClients].DoWork += new DoWorkEventHandler(cReceiver);
                //threads[numClients].RunWorkerAsync();

                numClients++;
            }
            listener.Stop();
            this.listener.CancelAsync();
        }
        private void cReceiver(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                for (int i = 0; i < clients.Length; i++)
                {
                    if (clients[i].Connected)
                    {
                        MessageBox.Show("client {0} is connected", i.ToString());
                        try
                        {
                            receive = readers[i].ReadLine();
                            if (receive != "")
                            {
                                MessageBox.Show(receive + " from " + i.ToString());
                                ServerDecode(receive, i);
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
        private void ServerDecode(string data, int clientNum)//triggers when a player joins the lobby
        {
            switch (data[0])
            {
                case '0'://join (username)
                    serverPlayers[clientNum] = data.Substring(1);

                    for (int i = 0; i < numClients; i++)
                    {
                        if (i != clientNum)
                        {
                            if (clients[i].Connected)
                            {
                                writers[i].Write(data);
                            }
                        }
                    }
                    break;
                case '1'://leave
                    break;
                case '2'://rule update
                    break;
                case '3'://end turn
                    break;
                case '4'://
                    break;
                default:
                    MessageBox.Show("Invalid transmission type at server");
                    break;
            }
        }
        private void serverReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                MessageBox.Show("looping yeet");
                for (int i = 0; i < numClients; i++)
                {
                    if (clients[i].Connected)
                    {
                        //MessageBox.Show("client {0} is connected", i.ToString());
                        try
                        {
                            receive = readers[i].ReadLine();
                            if (receive != "")
                            {
                                MessageBox.Show(receive + " from " + i.ToString());
                                ServerDecode(receive, i);
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
