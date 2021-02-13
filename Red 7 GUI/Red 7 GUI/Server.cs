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
        private List<IPEndPoint> iPAddresses;
        private List<int> alivePlayers;
        private int currentPlayerIndex;
        private bool gameStarted;
        private int port = 56565;
        bool actionRule;
        bool advanced;
        public Server(string hostName)
        {
            playerNames = new string[4];
            iPAddresses = new List<IPEndPoint>();
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
        private void StartServer()
        {
            // Get Host IP Address that is used to establish a connection  
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, port);
            iPAddresses.Add(localEndPoint);
            

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
                        Decode(data, handler);
                        bytes = new byte[1024];
                        data = string.Empty;
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        private void Decode(string raw, Socket handler)
        {
            char[] chars = raw.ToCharArray();

            switch (chars[0])
            {
                case '0'://join lobby
                    PlayerJoined(handler, chars);
                    break;
                case '1'://leave lobbby
                    PlayerLeft(chars);
                    break;
                case '2'://update rule
                    UpdateRule(chars);
                    break;
                case '3'://end turn
                    break;
                case '4':
                    break;
                default:
                    throw new Exception("");
            }
        }
        private void PlayerJoined(Socket handler, char[] chars)//triggers when a player joins the lobby
        {
            if (!gameStarted)
            {
                IPEndPoint newUser = (IPEndPoint)handler.RemoteEndPoint;
                iPAddresses.Add(newUser);
                string username = string.Empty;

                for (int i = 1; i < chars.Length; i++)
                {
                    username += chars[i];
                }

                playerNames[players] = username;
                players++;
            }
            else
            {
                //send error message
            }
            //send all players player list
            
            //send new player rules
            byte[] data = new byte[1024];
            string msg = "2"; //rule update

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

            data = Encoding.ASCII.GetBytes(msg);

            handler.Send(data);
        }
        private void PlayerLeft(char[] chars)
        {

        }
        private void UpdateRule(char[] chars)
        {
            if (!gameStarted)
            {
                if (chars[1] == '0')//advanced
                {
                    advanced = false;
                }
                else
                {
                    advanced = true;
                }
                if (chars[2] == '0')//action rule
                {
                    actionRule = false;
                }
                else
                {
                    actionRule = true;
                }

                string data = string.Empty;

                foreach (char c in chars)
                {
                    data += c;
                }

                for (int i = 1; i < players; i++)//sends rule update to every client except the host
                {
                    SendToClient(data, i);
                }
            }
        }
        private void EndTurn(Queue<Action> actionQueue, bool winning, int sender)//triggers when update received from a client
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
        private void SendToClient(string data, int client)
        {
            byte[] bytes = new byte[1024];

            try
            {
                IPEndPoint remoteEP = iPAddresses[client];
                // Create a TCP/IP  socket
                Socket sender = new Socket(remoteEP.Address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors
                try
                {
                    sender.Connect(remoteEP);

                    // Encode the data string into a byte array 
                    byte[] msg = Encoding.ASCII.GetBytes(data + "<EOF>");

                    // Send the data through the socket 
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device
                    int bytesRec = sender.Receive(bytes);

                    // Close the socket
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    if (bytesRec != default)
                    {
                        //might be useful later
                    }
                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
