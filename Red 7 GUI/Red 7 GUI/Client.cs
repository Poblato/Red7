﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Red_7_GUI
{
    public class Client
    {
        private int players;
        private List<Palette> palettes;
        private List<Hand> hands;
        private Deck deck;
        private Stack<Card> canvas;
        private Scorer scorer;
        private bool advanced;
        private bool actionRule;
        private Stack<Action> actions;
        private List<int> alivePlayers;
        private bool canEnd; // if the turn can be ended or actions must be undone beforehand (to prevent a losing player changing the outcome of the game)

        private int gameState; /*defines the state of the client
                                * -1: other player's turn
                                * 0: awaiting player action (play or discard)
                                * 1: awaiting player action (discard only)
                                * 2: awaiting player action (end turn or undo, i.e. no more valid actions can be taken this turn)
                                * 3: select card to discard from other player palette (triggers when 1 played and action rule is enabled)
                                * 4: select card to discard from own palette (triggers when 7 played and action rule is enabled)
                                */

        TcpClient client;
        StreamWriter STW;
        StreamReader STR;
        Thread receiver;
        public int GameState { get { return gameState; } set { gameState = value; } }
        public List<Hand> Hands { get { return hands; } }
        public List<Palette> Palettes { get { return palettes; } }
        public Deck Deck { get { return deck; } }
        public Card Canvas { get { return canvas.Peek(); } }
        public bool CanUndo { get { if (actions.Count == 0) return false; else return true; } }
        public Client(int numPlayers, bool advanced, bool actionRule, int seed, ref TcpClient tcpClient, ref StreamWriter STW, ref StreamReader STR)
        {
            palettes = new List<Palette>();
            hands = new List<Hand>();
            deck = new Deck();
            scorer = new Scorer();
            canvas = new Stack<Card>();
            actions = new Stack<Action>();
            client = tcpClient;
            this.STW = STW;
            this.STR = STR;
            alivePlayers = new List<int>();
            players = numPlayers;
            canEnd = true;
            deck.Reset(seed);
            this.advanced = advanced;
            this.actionRule = actionRule;

            canvas.Push(new Card(0, 7));

            for (int i = 0; i < numPlayers; i++)
            {
                alivePlayers.Add(i);
                palettes.Add(new Palette());
                hands.Add(new Hand());
            }

            receiver = new Thread(Receive);
            receiver.Start();

            Setup();
        }
        public bool CheckWinner(int currentPlayer)
        {
            return scorer.Score(palettes, currentPlayer, canvas.Peek().Colour, alivePlayers);
        }
        private void Setup()
        {
            for (int i = 0; i < players; i++)//add a card to each players' palette
            {
                palettes[i].AddCard(deck.DrawCard());
            }
            for (int i = 0; i < 7; i++)//each player draws 7 cards
            {
                for (int x = 0; x < players; x++)
                {
                    hands[x].AddCard(deck.DrawCard());
                }
            }
        }
        public void PlayToPalette(int player, int startIndex)
        {
            Card card = hands[player].GetCard(startIndex);
            palettes[player].AddCard(card);
            hands[player].RemoveCardByIndex(startIndex);

            int[] endPos = new int[3];

            endPos[0] = 1;
            endPos[1] = player;
            endPos[2] = palettes[player].Size;

            Action action = new Action("playToPalette", gameState);
            action.StartPos = new int[] { 0, player, startIndex };
            action.EndPos = endPos;

            actions.Push(action);

            gameState = 1;

            if (actionRule)
            {
                switch (card.Rank)
                {
                    case 1:
                        //discard a card from other players' palette (that player must have more or same cards in palette than current player)
                        gameState = 3;

                        bool largestPalette = true;//if the player's palette is largest, they are unable to discard from another player
                        for (int i = 1; i < players; i++)
                        {
                            if (palettes[i].Size >= palettes[0].Size)
                            {
                                largestPalette = false;
                            }
                        }
                        if (largestPalette == true)
                        {
                            gameState = 1;
                        }
                        break;
                    case 3:
                        action = new Action("drawCard", gameState);
                        action.StartPos = new int[] { 0, -2, deck.Size - 1 };
                        action.EndPos = new int[] { 0, player, hands[player].Size };
                        action.End = false;
                        actions.Push(action);

                        card = deck.DrawCard();
                        hands[player].AddCard(card);//draw a card
                        break;
                    case 5:
                        gameState = 0;//allows the player to play another card 
                        break;
                    case 7:
                        //discard a card from player's palette
                        gameState = 4;
                        break;
                }
            }
        }
        public void DiscardToCanvas(int player, int index)
        {
            Card card = hands[player].GetCard(index);
            int[] startPos = new int[3];
            startPos[0] = 0;//discard from hand
            startPos[1] = player;//discard from current player
            startPos[2] = index;//which specific card to discard

            Action action = DiscardCard(startPos, -1);// -1 to discard to canvas
            action.End = true;

            actions.Push(action);

            gameState = 2;
            canEnd = false;

            if (advanced == true)
            {
                if (card.Rank > palettes[player].Size)
                {

                    action = new Action("drawCard", gameState);
                    action.StartPos = new int[] { 0, -2, deck.Size - 1 };
                    action.EndPos = new int[] { 0, player, hands[player].Size };
                    action.End = false;
                    actions.Push(action);

                    hands[player].AddCard(deck.DrawCard());//draws a card

                    Program.Update(-1);
                }
            }
        }
        public void DiscardPaletteCard(int player, int index, int target)//target: -1 for canvas, -2 for deck
        {
            Card card = hands[player].GetCard(index);
            int[] startPos = new int[3];
            startPos[0] = 1;//discard from hand
            startPos[1] = player;//discard from current player
            startPos[2] = index;//which specific card to discard

            Action action = DiscardCard(startPos, target);

            action.End = false;
            actions.Push(action);

            gameState = 1;
            if (target != player)
            {
                canEnd = false;
            }
        }
        private Action DiscardCard(int[] startPos, int target)
        {
            Card card = new Card(0, 0);
            int targetPos = 0;
            if (startPos[0] == 0)
            {
                card = hands[startPos[1]].GetCard(startPos[2]);
                hands[startPos[1]].RemoveCardByIndex(startPos[2]);
            }
            else if (startPos[0] == 1)
            {
                card = palettes[startPos[1]].GetCard(startPos[2]);
                palettes[startPos[1]].RemoveCardByIndex(startPos[2]);
            }
            Action action = new Action("discardCard", gameState);
            action.StartPos = startPos;


            switch (target)
            {
                case -1://canvas
                    canvas.Push(card);
                    break;
                case -2://deck
                    targetPos = deck.Size;
                    deck.AddCard(card);
                    break;
                default://player
                    targetPos = palettes[target].Size;
                    palettes[target].AddCard(card);
                    break;
            }
            action.EndPos = new int[] { 1, target, targetPos };
            return action;
        }
        public bool TryUndo()
        {
            Action action = actions.Peek();
            if (action.Type == "drawCard")
            {
                return false;
            }
            else
            {
                Undo(actions.Pop());
                return true;
            }
        }
        private void Undo(Action action)
        {
            //Console.WriteLine(actions.Count);

            MoveCard(action.EndPos, action.StartPos);
            gameState = action.PrevGameState;

            if (action.End == false)
            {
                Undo(actions.Pop());
            }

            Program.Update(action.StartPos[1]);//updates start player
            Program.Update(action.EndPos[1]);//updates end player
            Program.Update(-1);
        }
        private void DoActions(Queue<Action> actions)
        {
            Action action;
            while (actions.Count > 0)
            {
                action = actions.Dequeue();
                Program.Display(action.Type);
                Program.Display(action.StartPos[0].ToString() + " " + action.StartPos[1].ToString() + " " + action.StartPos[2].ToString());
                Program.Display(action.EndPos[0].ToString() + " " + action.EndPos[1].ToString() + " " + action.EndPos[2].ToString());
                MoveCard(action.StartPos, action.EndPos);
            }

            for (int i = 0; i < players; i++)
            {
                Program.Update(i);
            }
            Program.Update(-1);
        }
        private void MoveCard(int[] startPos, int[] endPos)
        {
            Card card;
            switch (startPos[1])
            {
                case -1://canvas
                    card = canvas.Pop();
                    Program.Display("Moving from canvas");
                    break;
                case -2://deck
                    card = deck.DrawCard();
                    Program.Display("Moving from deck");
                    break;
                default://player
                    if (startPos[0] == 0)
                    {
                        Program.Display("Moving from hand " + startPos[1].ToString());
                        card = hands[startPos[1]].RemoveCardByIndex(startPos[2]);
                    }
                    else
                    {
                        Program.Display("Moving from palette " + startPos[1].ToString());
                        card = palettes[startPos[1]].RemoveCardByIndex(startPos[2]);
                    }
                    break;
            }

            switch (endPos[1])
            {
                case -1://canvas
                    Program.Display("to canvas");
                    canvas.Push(card);
                    break;
                case -2://deck
                    Program.Display("to deck");
                    deck.AddCard(card);
                    break;
                default://player
                    if (endPos[0] == 0)
                    {
                        Program.Display("to hand " + endPos[1].ToString());
                        hands[endPos[1]].InsertCard(endPos[2], card);
                    }
                    else
                    {
                        Program.Display("to palette " + endPos[1].ToString());
                        palettes[endPos[1]].InsertCard(endPos[2], card);
                    }
                    break;
            }
        }
        public void EndTurn(bool winning)
        {

            Program.Display(actions.Count.ToString());

            if (!winning && !canEnd)
            {
                while (actions.Count != 0)
                {
                    Undo(actions.Pop());
                }
                // remove players' cards from play
            }

            gameState = -1;
            canEnd = true;
            Program.Update(-1);
            UpdateServer(winning);
        }
        public void UpdateServer(bool winning)
        {
            Queue<Action> actionQueue = new Queue<Action>();
            actions.Reverse();

            while (actions.Count > 0)//empties the action stack and puts it into a queue
            {
                actionQueue.Enqueue(actions.Pop());
            }

            string msg = "3";

            if (winning)
            {
                msg += "1";
            }
            else
            {
                msg += "0";
            }

            msg += ActionEncode(actionQueue);

            Send(msg);
            //send server winning, actionQueue
        }
        private void UpdateClient(Queue<Action> actionQueue, List<int> alivePlayers ,bool playerTurn) //triggers when queue of actions received from the server
        {
            Action action;
            this.alivePlayers = alivePlayers;

            for (int i = 0; i < actionQueue.Count; i++)
            {
                action = actionQueue.Dequeue();

                MoveCard(action.StartPos, action.EndPos);
            }

            if (playerTurn)
            {
                gameState = 0;
            }

            foreach (int i in alivePlayers)//updates the form
            {
                Program.Update(i);
            }
        }
        private void Receive()
        {
            string receive;
            while (client.Connected)
            {
                try
                {
                    receive = STR.ReadLine();
                    Program.Display("Received " + receive);
                    Decode(receive);
                }
                catch (Exception e)
                {
                    Program.Display(e.ToString());
                }
            }
        }
        private void Send(string data)
        {
            if (client.Connected)
            {
                STW.WriteLine(data);
                //MessageBox.Show("Sent " + data);
            }
        }
        private void Decode(string data)
        {
            switch (data[0])
            {
                case '0'://join
                    Program.Display("Join request during game");
                    break;
                case '1'://leave
                         //MessageBox.Show("client received leave message");
                    try
                    {
                        PlayerLeft(int.Parse(data[1].ToString()));
                    }
                    catch (Exception e)
                    {
                        Program.Display(e.ToString());
                    }
                    break;
                case '2'://rule update
                    Program.Display("Rule change request during game");
                    break;
                case '3'://end turn
                    int prevPlayer = int.Parse(data[1].ToString());
                    bool won;
                    if (data[2] == '0')
                    {
                        won = false;
                    }
                    else
                    {
                        won = true;
                    }

                    if (!won)
                    {
                        alivePlayers.Remove(prevPlayer);
                        Program.RemovePlayer(prevPlayer);
                    }

                    Queue<Action> actions = ActionDecode(data.Substring(4));

                    DoActions(actions);

                    if (won != CheckWinner(prevPlayer))
                    {
                        Program.Display("Desync detected");
                    }

                    if (data[3] == '1')
                    {
                        gameState = 0;
                    }
                    Program.Update(-1);
                    break;
                case '4'://start game
                    Program.Display("Start game request during game");
                    break;
                case '5'://
                    break;
                default:
                    Program.Display("Invalid transmission type at client");
                    break;
            }
        }
        private string ActionEncode(Queue<Action> actions)
        {
            /* Format:
             * end(0,1),type,prevGameState,[int,int,int],[int,int,int];nextAction
             */
            string encoded = "";
            Action action;

            while (actions.Count > 0)
            {
                action = actions.Dequeue();

                if (action.End)
                {
                    encoded += '1';
                }
                else
                {
                    encoded += '0';
                }
                encoded += ',';

                encoded += action.Type;
                encoded += ',';

                encoded += action.PrevGameState.ToString();
                encoded += ',';

                encoded += action.StartPos[0].ToString() + "|"+ action.StartPos[1].ToString()  + "|"+ action.StartPos[2].ToString();
                encoded += ',';

                encoded += action.EndPos[0].ToString() + "|" + action.EndPos[1].ToString() + "|" + action.EndPos[2].ToString();
                encoded += ';';
            }
            return encoded;
        }
        private Queue<Action> ActionDecode(string encoded)
        {
            Queue<Action> actions = new Queue<Action>();
            Action action;

            string[] temp = encoded.Split(';');
            string[][] actionStrings = new string[temp.Length - 1][];//split will add an empty string at the end as every ation ends in a ;
            
            for (int i = 0; i < temp.Length - 1; i++)
            {
                actionStrings[i] = temp[i].Split(',');
            }

            bool end;
            int prevGameState = -1;
            string type;
            int[] startPos = new int[3];
            int[] endPos = new int[3];
            string[] posArray;

            for (int i = 0; i > actionStrings.Length; i++)
            {
                string[] a = actionStrings[i];
                //Program.Display(a[0]);
                if (a[0] == "0")
                {
                    end = false;
                }
                else
                {
                    end = true;
                }

                //Program.Display(a[1]);
                type = a[1];

                //Program.Display(a[2]);
                try
                {
                    prevGameState = int.Parse(a[2]);
                }
                catch
                {
                    Program.Display("non-integer gamestate in action decode");
                }

                Program.Display(a[3]);
                posArray = a[3].Split('|');
                try
                {
                    startPos[0] = int.Parse(posArray[0]);
                    startPos[1] = int.Parse(posArray[1]);
                    startPos[2] = int.Parse(posArray[2]);
                }
                catch
                {
                    Program.Display("non-integer startpos in action decode");
                }

                //Program.Display(startPos[0].ToString() + " " + startPos[1].ToString() + " " + startPos[2].ToString());

                Program.Display(a[4]);
                posArray = a[4].Split('|');
                try
                {
                    endPos[0] = int.Parse(posArray[0]);
                    endPos[1] = int.Parse(posArray[1]);
                    endPos[2] = int.Parse(posArray[2]);
                }
                catch
                {
                    Program.Display("non-integer endpos in action decode");
                }

                action = new Action(type, prevGameState);
                action.End = end;
                action.StartPos = startPos;
                action.EndPos = endPos;

                actions.Enqueue(action);

                Queue<Action> debug1 = new Queue<Action>();
                Action debug2;
                while (actions.Count > 0)
                {
                    debug2 = actions.Dequeue();
                    Program.Display(debug2.Type);
                    debug1.Enqueue(debug2);
                }
                actions = debug1;
            }

            //Program.Display(actions.Count.ToString());

            return actions;
        }
        private void PlayerLeft(int player)
        {
            //MessageBox.Show(numPlayers.ToString());
            alivePlayers.Remove(player);
            Program.RemovePlayer(player);
        }
        public void Debug()
        {
            alivePlayers.Remove(1);
            Program.RemovePlayer(1);
        }
    }
}
