using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_7_GUI
{
    public class Server
    {
        private int players;
        private string[] playerNames;
        private List<int> alivePlayers;
        private int currentPlayerIndex;
        bool actionRule;
        bool advanced;
        public Server(string hostName)
        {
            playerNames = new string[4];
            alivePlayers = new List<int>();
            actionRule = false;
            advanced = false;

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
    }
}
