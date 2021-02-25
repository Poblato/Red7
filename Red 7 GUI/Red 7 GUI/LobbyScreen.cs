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
        private GameScreen game;
        bool serverStarted;
        public LobbyScreen(bool host, IPAddress ip, string username)
        {
            this.host = host;//initialises variables
            this.username = username;
            clientPlayers = new string[4];
            gameStarted = false;
            serverStarted = true;
            InitializeComponent();
            startButton.Enabled = false;

            if (!host)
            {
                this.ip = ip;
                startServerButton.Enabled = false;//disables host options
                actionCheckBox.Enabled = false;
                advancedCheckBox.Enabled = false;
                

                if (!ConnectToServer())//attempts to connect to server, closes if falied
                {
                    Close();
                    Program.Left();
                }

                ClientSend("0" + username);//sends join message with username

                receiver.RunWorkerAsync();//starts receiver
            }
            else
            {
                IPHostEntry hostInfo = Dns.GetHostEntry("localhost");//sets ip to local machine
                this.ip = hostInfo.AddressList[hostInfo.AddressList.Length - 1];
                player0Label.Text = username + " (host)";
            }

            //MessageBox.Show(this.ip.ToString());
        }
        public void Update(int set)
        {
            if (set < 0)//redraws the player hand
            {
                game.Invoke((MethodInvoker)delegate {
                    game.RedrawHand();
                });
            }
            else//redraws a palette
            {
                game.Invoke((MethodInvoker)delegate {
                    game.RedrawPalette(set);
                });
            }
        }
        public void RemovePlayer(int player, bool left)//removes a player from the lobby/game
        {
            game.Invoke((MethodInvoker)delegate {//removes the player from the game
                game.RemovePlayer(player);
            });
            if (player == FindPlayerNum())//if the player is leaving close the lobby
            {
                Close();
            }

            if (left)//displays the appropriate message
            {
                MessageBox.Show(clientPlayers[player] + " has left the game");
            }
            else
            {
                MessageBox.Show(clientPlayers[player] + " has lost");
            }
        }
        #region Lobby
        private void UpdateLabels()//updates the player names
        {
            //ensures that this is called from the main thread - winforms does not support multithreaded access to components
            this.player0Label.Invoke((MethodInvoker)delegate {
                this.player0Label.Text = clientPlayers[0] + " (host)";
            });
            this.player1Label.Invoke((MethodInvoker)delegate {
                this.player1Label.Text = clientPlayers[1];
            });
            this.player2Label.Invoke((MethodInvoker)delegate {
                this.player2Label.Text = clientPlayers[2];
            });
            this.player3Label.Invoke((MethodInvoker)delegate {
                this.player3Label.Text = clientPlayers[3];
            });
        }
        private void UpdateRules()//updates the rules
        {
            this.advancedCheckBox.Invoke((MethodInvoker)delegate {
                this.advancedCheckBox.Checked = advanced;
            });
            this.actionCheckBox.Invoke((MethodInvoker)delegate {
                this.actionCheckBox.Checked = actionRule;
            });
        }
        private void helpButton_Click(object sender, EventArgs e)//opens lobby help window
        {
            ClientSend("test");
            //open lobby help window
        }
        private void quitButton_Click(object sender, EventArgs e)//leaves the lobby
        {
            //MessageBox.Show("sending leave message");
            LeaveLobby();
        }
        public void LeaveLobby()
        {
            if (client.Connected)
            {
                ClientSend("1");//ensures the receivers exit properly

                serverStarted = false;

                client.Close();
                client = new TcpClient();
                try
                {
                    client.Connect(new IPEndPoint(ip, port));//allows the listener to close
                    client.Close();
                }
                catch { }
            }

            Program.Left();//closes the lobby and re-opens the menu
            Close();
        }
        private void startButton_Click(object sender, EventArgs e)//starts the game
        {
            if (numPlayers > 1)
            {
                Random rnd = new Random();
                int seed = rnd.Next(0, 2147483647);//generates a random seed for the game
                FindFirstPlayer(seed);//finds the starting player

                StartGame(seed);
            }
            else
            {
                MessageBox.Show("Looks like there's noone here to play with you (can't relate)", "Cannot start game");
            }
        }
        private void actionCheckBox_CheckedChanged(object sender, EventArgs e)//updates the game rules
        {
            actionRule = actionCheckBox.Checked;
            ServerDecode("2", 0);//tells the server that rules have changed
        }
        private void advancedCheckBox_CheckedChanged(object sender, EventArgs e)//updates the game rules
        {
            advanced = advancedCheckBox.Checked;
            ServerDecode("2", 0);//tells the server that rules have changed
        }
        #endregion
        #region Client
        private StreamReader STR;
        private StreamWriter STW;
        private TcpClient client;
        private string[] clientPlayers;
        private int numPlayers = 0;
        private bool ConnectToServer()//attempts to connect to the server
        {
            client = new TcpClient();
            IPEndPoint remoteEndpoint = new IPEndPoint(ip, port);

            try
            {
                client.Connect(remoteEndpoint);//tries to connect
                if (client.Connected)
                {
                    //MessageBox.Show("Connected to server");
                    STW = new StreamWriter(client.GetStream());//binds the client streamreader and streamwriter to the tcp stream
                    STR = new StreamReader(client.GetStream());
                    STW.AutoFlush = true;

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
        private void ClientSend(string data)//sends data to the server
        {
            if (client.Connected)
            {
                STW.WriteLine(data);
                //MessageBox.Show("Sent " + data);
            }
        }
        private void receiver_DoWork(object sender, DoWorkEventArgs e)//backgroundworker work function to receive messages from the server
        {
            bool cont = true;
            while (cont)
            {
                try
                {
                    if (client.Connected)
                    {
                        receive = STR.ReadLine();
                        //MessageBox.Show("Received " + receive);
                        if (gameStarted)
                        {
                            game.GameDecode(receive);
                        }
                        else
                        {
                            cont = ClientDecode(receive);
                        }
                    }
                    else
                    {
                        cont = false;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.ToString());
                    cont = false;
                }
            }
        }
        private bool ClientDecode(string data)//triggers when a player joins the lobby - returns whether or not to continue the connection
        {
            switch (data[0])
            {
                case '0'://join
                    clientPlayers = data.Substring(1).Split('~');//updates the username array
                    numPlayers = 0;
                    foreach (string s in clientPlayers)//updates the number of players
                    {
                        if (s != string.Empty)
                        {
                            numPlayers++;
                        }
                    }
                    UpdateLabels();//updates the GUI
                    return true;
                case '1'://leave
                    //MessageBox.Show("client received leave message");
                    try
                    {
                        return CPlayerLeft(int.Parse(data[1].ToString()));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.ToString());
                    }
                    return true;
                case '2'://rule update
                    if (data[1] == '0')//advanced
                    {
                        advanced = false;
                    }
                    else
                    {
                        advanced = true;
                    }
                    if (data[2] == '0')//action rule
                    {
                        actionRule = false;
                    }
                    else
                    {
                        actionRule = true;
                    }
                    UpdateRules();
                    return true;
                case '3'://end turn
                    return true;
                case '4'://start game
                    bool start;//whether the player is the starting player
                    int seed = -1;
                    if (data[1] == '0')
                    {
                        start = false;
                    }
                    else
                    {
                        start = true;
                    }
                    try
                    {
                        seed = int.Parse(data.Substring(2));
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Invalid seed: " + e.ToString());
                    }
                    //receiver.CancelAsync();

                    this.Invoke((MethodInvoker)delegate {
                        game = new GameScreen(numPlayers, FindPlayerNum(), clientPlayers, advanced, actionRule, seed, start, ref STW);
                        this.Hide();
                        game.Show();//starts the game
                    });

                    gameStarted = true;

                    return true;
                case '5'://
                    return true;
                default:
                    MessageBox.Show("Invalid transmission type at client");
                    return true;
            }
        }
        private bool CPlayerLeft(int player)//removes a player on the client
        {
            //MessageBox.Show(player.ToString());
            //MessageBox.Show(numPlayers.ToString());
            if (player == FindPlayerNum())//disconnects the client (the player leaving is the local client)
            {
                return false;
            }
            clientPlayers[player] = "";//removes player name
            for (int i = player; i < numPlayers - 1; i++)
            {
                clientPlayers[i] = clientPlayers[i + 1];//shuffles player names back one
            }
            numPlayers--;
            UpdateLabels();//updates GUI
            return true;
        }
        private int FindPlayerNum()//returns the index of the player, -1 if not found
        {
            for(int i = 0; i < 4; i++)
            {
                if (clientPlayers[i] == username)
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion
        #region Server
        private TcpClient[] clients;
        private StreamReader[] readers;
        private StreamWriter[] writers;
        private string[] serverPlayers;
        private Thread[] receivers;
        private Thread listener;
        private int numClients;
        private int currentPlayer;
        private List<int> alivePlayers;
        private void StartServer()
        {
            serverPlayers = new string[4];
            clients = new TcpClient[4];
            readers = new StreamReader[4];
            writers = new StreamWriter[4];
            receivers = new Thread[4];
            listener = new Thread(ConnectionListen);

            listener.Start();//listens for new connections

            ConnectToServer();//connects host to the server
            receiver.RunWorkerAsync();//starts the client receiver

            ClientSend("0" + username);//join message for host
        }
        private void startServerButton_Click(object sender, EventArgs e)//starts the server
        {
            numClients = 0;
            startButton.Enabled = true;
            startServerButton.Enabled = false;
            StartServer();
        }
        private void ConnectionListen()//listens for new connections
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            //MessageBox.Show("listening for new connections");

            while (numClients < 4 && !gameStarted && serverStarted)//stops listening when the lobby is full or the the game starts/server closes
            {
                //MessageBox.Show("next conn");
                clients[numClients] = listener.AcceptTcpClient();//the thread will hang on this line until a connection is received
                if (gameStarted)//if game was started while the listener was hanging
                {
                    clients[numClients].Close();//close the new connection
                    break;
                }
                readers[numClients] = new StreamReader(clients[numClients].GetStream());//binds the streamreader and streamwriter
                writers[numClients] = new StreamWriter(clients[numClients].GetStream());
                writers[numClients].AutoFlush = true;
                receivers[numClients] = new Thread(ServerReceive);//runs the receiver for the new client

                receivers[numClients].Start(numClients);
                //MessageBox.Show("Client " + numClients.ToString() + " connected");
                numClients++;
            }
            listener.Stop();
        }
        private bool ServerDecode(string data, int clientNum)//triggers when a client sends a message - returns whether or not to close the receiver
        {
            string msg;
            string msg2;
            switch (data[0])
            {
                case '0'://join (username)
                    serverPlayers[clientNum] = data.Substring(1);

                    msg = "0";//join

                    foreach(string name in serverPlayers)
                    {
                        msg += name + "~";
                    }
                    //msg = msg.Substring(0, msg.Length - 1);//removes the last ~

                    for (int i = 0; i < numClients; i++)
                    {
                        if (clients[i].Connected)
                        {
                            //MessageBox.Show("sending " + msg + " to " + i.ToString());
                            writers[i].WriteLine(msg);//sends data to client i
                        }
                    }

                    msg2 = "2";//update rules

                    if (advanced)
                    {
                        msg2 += "1";
                    }
                    else
                    {
                        msg2 += "0";
                    }
                    if (actionRule)
                    {
                        msg2 += "1";
                    }
                    else
                    {
                        msg2 += "0";
                    }

                    writers[clientNum].WriteLine(msg2);//sends to new player

                    return true;
                case '1'://leave

                    //MessageBox.Show("server received leave message");
                    SPlayerLeft(clientNum);
                    msg = "1" + clientNum.ToString();

                    for (int i = 0; i < numClients; i++)//sends to all cients
                    {
                        if (clients[i].Connected)
                        {
                            //MessageBox.Show("sending " + msg + " to " + i.ToString());
                            writers[i].WriteLine(msg);//sends data to client i
                        }
                    }

                    return false;//closes the connection
                case '2'://rule update
                    msg = "2";

                    if (advanced)
                    {
                        msg += "1";
                    }
                    else
                    {
                        msg += "0";
                    }
                    if (actionRule)
                    {
                        msg += "1";
                    }
                    else
                    {
                        msg += "0";
                    }

                    for (int i = 1; i < numClients; i++)//send to all except host
                    {
                        if (clients[i].Connected)
                        {
                            //MessageBox.Show("sending " + msg2 + " to " + i.ToString());
                            writers[i].WriteLine(msg);//sends data to client i
                        }
                    }

                    return true;
                case '3'://end turn
                    //end turn; player; winning; your turn; actions

                    msg = "3" + clientNum.ToString() + data[1] + "0" + data.Substring(2);//all other players
                    msg2 = "3" + clientNum.ToString() + data[1] + "1" + data.Substring(2);//next player

                    if (data[1] == '0')//player not winning
                    {
                        alivePlayers.Remove(clientNum);
                    }
                    if (alivePlayers.Count == 1)//if only one player left, game ends with them winning
                    {
                        int winner = alivePlayers[0];

                        for (int i = 0; i < numClients; i++)
                        {
                            if (clients[i].Connected)
                            {
                                //MessageBox.Show("sending " + msg + " to " + i.ToString());
                                writers[i].WriteLine("5" + winner.ToString()); ;//sends data to client i
                            }
                        }
                        return true;
                        //player wins
                    }
                    
                    for (int i = 0; i < alivePlayers.Count; i++)//increments the player turn
                    {
                        if (currentPlayer == alivePlayers[i])
                        {
                            currentPlayer = alivePlayers[(i + 1) % alivePlayers.Count];
                            break;
                        }
                    }

                    foreach (int i in alivePlayers)
                    {
                        if (i == currentPlayer)
                        {
                            //MessageBox.Show("sent: " + msg2 + " to " + i.ToString());
                            writers[i].WriteLine(msg2);
                        }
                        else if (i != clientNum)
                        {
                            //MessageBox.Show("sent: " + msg + " to " + i.ToString());
                            writers[i].WriteLine(msg);
                        }
                    }

                    return true;
                case '4'://
                    return true;
                case '-'://ack

                    return true;
                default:
                    MessageBox.Show("Invalid transmission type at server");
                    return true;
            }

        }
        private void ServerReceive(Object obj)//receiver for server
        {
            int client;
            bool cont = true;
            try
            {
                client = (int)obj;//parses in the client to listen to
            }
            catch
            {
                MessageBox.Show("Non-integer parsed to server receiver");
                return;
            }
            while (cont && serverStarted)
            {
                if (clients[client].Connected)
                {
                    //MessageBox.Show("Client " + client.ToString() + " is connected");
                    try
                    {
                        receive = readers[client].ReadLine();
                        if (!char.IsWhiteSpace(receive[0]))
                        {
                            //MessageBox.Show(receive + " from " + client.ToString());
                            cont = ServerDecode(receive, client);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
            }
        }
        private void SPlayerLeft(int player)//removes a players connection server-side
        {
            numClients--;
            for (int i = player; i < numClients; i++)//moves all items after the player back one
            {
                clients[i] = clients[i + 1];
                readers[i] = readers[i + 1];
                writers[i] = writers[i + 1];
            }
        }
        private void FindFirstPlayer(int seed)//finds the first player to take their turn
        {
            Deck deck = new Deck();
            deck.Reset(seed);//sets up a deck using the game seed
            Card lowestCard = new Card(7, 7);
            int startingPlayer = -1;

            for (int i = 0; i < numClients; i++)//draws a card for each player (these will go onto the palette)
            {
                if (deck.DrawCard().GetScore() < lowestCard.GetScore())
                {
                    lowestCard = deck.GetCard(i);//the player with the lowerst card goes first
                    startingPlayer = i;
                }
            }

            if (startingPlayer == -1)
            {
                throw new Exception("lowest card not found - no card lower than red 7");
            }

            currentPlayer = startingPlayer;
        }
        private void StartGame(int seed)//starts a game with a given seed
        {
            alivePlayers = new List<int>();

            //MessageBox.Show(numClients.ToString());

            for (int i = 0; i < numClients; i++)
            {
                alivePlayers.Add(i);//sets up aliveplayers list
            }

            for (int i = 0; i < numClients; i++)//sends start messages
            {
                if (i == currentPlayer)
                {
                    writers[i].WriteLine("41" + seed);//starting player
                }
                else
                {
                    writers[i].WriteLine("40" + seed);//all other player
                }
            }
        }
        #endregion

        public void Display(string msg)//displays a message
        {
            try
            {
                game.Display(msg);
            }
            catch (Exception)
            {
                MessageBox.Show("display called before game started");
            }
        }
        public void EndGame(int winner)//displays win message and returns to lobby
        {
            MessageBox.Show(clientPlayers[winner] + " wins!");
            game.Invoke((MethodInvoker)delegate {
                game.Close();
            });
            this.Invoke((MethodInvoker)delegate {
                this.Show();
            });
            gameStarted = false;
        }
    }
}
