using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Red_7_GUI
{
    public class Server
    {
        private int players;
        private string[] playerNames;
        private List<int> alivePlayers;
        private int currentPlayerIndex;
        private bool gameStarted;
        bool actionRule;
        bool advanced;
        public Server(string hostName)
        {
            playerNames = new string[4];
            alivePlayers = new List<int>();
            actionRule = false;
            advanced = false;
            gameStarted = false;

            playerNames[0] = hostName;
            players++;
        }
        public void SetupGame(int players, int seed)
        {
            this.players = players;
            for (int i = 0; i < players; i++)
            {
                alivePlayers.Add(i);
            }

            FindFirstPlayer(seed);
            CreateClients(seed);
        }
        private void FindFirstPlayer(int seed)
        {
            Deck deck = new Deck();
            deck.Reset(seed);
            Card lowestCard = new Card(7, 7);
            int startingPlayer = -1;

            for (int i = 0; i < players; i++)
            {
                if (deck.GetCard(i).GetScore() < lowestCard.GetScore())
                {
                    lowestCard = deck.GetCard(i);
                    startingPlayer = i;
                }
            }

            if (startingPlayer == -1)
            {
                throw new Exception("lowest card not found - no card lower than red 7");
            }

            currentPlayerIndex = startingPlayer;
        }
        private void CreateClients(int seed)
        {
            //send to lobby files to create client
            //tell first player to take turn
        }
        private void UpdateClients(Queue<Action> actionQueue, bool winning, int sender)//triggers when update received from a client
        {
            if (!winning)
            {
                alivePlayers.Remove(sender);//removes a player that has lost from the turn rotation
                currentPlayerIndex = currentPlayerIndex % alivePlayers.Count;//playerindex will have already increased by removing a player
            }
            else
            {
                currentPlayerIndex = (currentPlayerIndex + 1) % alivePlayers.Count;//moves to the next player
            }

            for (int i = 0; i < players; i++)
            {
                if (i == alivePlayers[currentPlayerIndex])
                {
                    //send actionQueue, players remaining, tell their turn
                }
                else
                {
                    //send actionQueue, players remaining
                }
            }
        }
        private void PlayerJoined(string username)//triggers when a player joins the lobby
        {
            playerNames[players] = username;
            players++;
            //send all players player list
            //send new player rules
        }
        private void RulesChanged(bool newActionRule, bool newAdvanced)//triggers when host changed lobby rules
        {
            actionRule = newActionRule;
            advanced = newAdvanced;

            //update lobbies
        }
        private void StartServer()
        {
            // Get Host IP Address that is used to establish a connection  
            // In this case, we get one IP address of localhost that is IP : 127.0.0.1  
            // If a host has multiple addresses, you will get a list of addresses  
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);


            try
            {
                // Create a Socket that will use Tcp protocol      
                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // A Socket must be associated with an endpoint using the Bind method  
                listener.Bind(localEndPoint);
                // Specify how many requests a Socket can listen before it gives Server busy response.  
                // We will listen 10 requests at a time  
                listener.Listen(10);

                Socket handler = listener.Accept();

                // Incoming data from the client.    
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data.IndexOf("<EOF>") > -1)
                    {
                        Decode(data);
                        bytes = new byte[1024];
                        data = string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        private void Decode(string raw)
        {
            char[] chars = raw.ToCharArray();

            switch (chars[0])
            {
                case '0'://join lobby
                    if (!gameStarted)
                    {
                        string username = string.Empty;
                        for (int i = 1; i < chars.Length; i++)
                        {
                            username += chars[i];
                        }

                        PlayerJoined(username);
                    }
                    break;
                case '1'://leave lobbby
                    break;
                case '2'://update rule
                    if (!gameStarted)
                    {
                        if (chars[1] == '0')//advanced
                        {
                            advanced = !advanced;
                        }
                        else if (chars[1] == '1')//action rule
                        {
                            actionRule = !actionRule;
                        }
                    }
                    break;
                case '3'://end turn
                    break;
                case '4':
                    break;
                default:
                    throw new Exception("");
            }
        }
        private void Encode()
        {

        }
    }
}
