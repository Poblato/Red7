using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private int gameState; /*defines the state of the client
                                * -1: other player's turn
                                * 0: awaiting player action (play or discard)
                                * 1: awaiting player action (discard only)
                                * 2: awaiting player action (end turn or undo, i.e. no more valid actions can be taken this turn)
                                * 3: select card to discard from other player palette (triggers when 1 played and action rule is enabled)
                                * 4: select card to discard from own palette (triggers when 7 played and action rule is enabled)
                                */
        public int GameState { get { return gameState; } }
        public List<Hand> Hands { get { return hands; } }
        public List<Palette> Palettes { get { return palettes; } }
        public Deck Deck { get { return deck; } }
        public Card Canvas { get { return canvas.Peek(); } }
        public Client(int numPlayers, bool advanced, bool actionRule)
        {
            palettes = new List<Palette>();
            hands = new List<Hand>();
            deck = new Deck();
            scorer = new Scorer();
            canvas = new Stack<Card>();
            actions = new Stack<Action>();
            players = numPlayers;
            deck.Reset();
            this.advanced = advanced;
            this.actionRule = actionRule;

            canvas.Push(new Card(0, 7));

            for (int i = 0; i < numPlayers; i++)
            {
                palettes.Add(new Palette());
                hands.Add(new Hand());
            }
            Setup();
        }
        private bool CheckWinner(int currentPlayer)
        {
            return scorer.Score(palettes, currentPlayer, canvas.Peek().Colour);
        }
        private void Setup()
        {
            for (int i = 0; i < players; i++)
            {
                palettes[i].AddCard(deck.DrawCard());
            }
            for (int i = 0; i < 7; i++)
            {
                for (int x = 0; x < players; x++)
                {
                    hands[x].AddCard(deck.DrawCard());
                }
            }
        }
        public void PlayTurn(int player)
        {
            gameState = 0;
        Turn:
            while (gameState != 2)
            {
                if (gameState == 0)
                {
                    Console.WriteLine("Enter (1) to play to palette, or (2) to discard to canvas, or (3) to end turn, or (4) to undo");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            gameState = 1;
                            PlayToPalette(player, 0);
                            break;
                        case "2":
                            DiscardToCanvas(player, 0);
                            gameState = 2;
                            break;
                        case "3":
                            gameState = 2;
                            break;
                        case "4":
                            Undo();
                            break;
                        default:
                            Console.WriteLine("No");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter (1) to discard to canvas, or (2) to end turn, or (3) to undo");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            DiscardToCanvas(player, 0);
                            gameState = 2;
                            break;
                        case "2":
                            gameState = 2;
                            break;
                        case "3":
                            Undo();
                            break;
                        default:
                            Console.WriteLine("No");
                            break;
                    }
                }
            }
            Console.WriteLine("Turn ended");

            bool winning = CheckWinner(player);

            if (winning != true)
            {
            LossConfirmation:
                Console.WriteLine("You are not winning press (1) to undo, or (2) to reset, or (3) to end turn and lose");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Undo();
                        goto Turn;
                    case "2":
                        while (actions.Count != 0)
                        {
                            Undo();
                        }
                        gameState = 0;
                        goto Turn;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("no");
                        goto LossConfirmation;
                }
            }
            gameState = -1;
        }
        public void Undo()
        {
            //undoes the last action taken
        }
        public void PlayToPalette(int player, int startIndex)
        {
            Card card = hands[player].GetCard(startIndex);
            palettes[player].AddCard(card);
            hands[player].RemoveCardByIndex(startIndex);

            int[] endPos = new int[3];
            gameState = 1;

            endPos[0] = 1;
            endPos[1] = player;
            endPos[2] = palettes[player].Size - 1;

            Action action = new Action("playToPalette", card);
            action.StartPos = new int[] { 0, player, startIndex };
            action.EndPos = endPos;

            actions.Push(action);

            switch (card.Rank)
            {
                case 1:
                    //discard a card from other players' palette (that player must have more or same cards in palette than current player)
                    gameState = 3;
                    break;
                case 3:
                    card = deck.DrawCard();
                    hands[player].AddCard(card);//draw a card
                    action = new Action("drawCard", card);
                    action.EndPos = new int[] { 0, player, hands[player].Size - 1 };
                    actions.Push(action);
                    Program.Update(-1);
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
        public void DiscardToCanvas(int player, int index)
        {
            Card card = hands[player].GetCard(index);
            int[] startPos = new int[3];
            startPos[0] = 0;//discard from hand
            startPos[1] = player;//discard from current player
            gameState = 2;

            startPos[2] = index;//which specific card to discard

            Action action = DiscardCard(startPos, -1);// -1 to discard to canvas

            action.End = true;

            actions.Push(action);

            if (advanced == true)
            {
                if (card.Rank > palettes[player].Size)
                {
                    hands[player].AddCard(deck.DrawCard());//draws a card
                    Program.Update(-1);
                }
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
            Action action = new Action("discardCard", card);
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
        public void Debug()
        {
            palettes[0].AddCard(new Card(7, 7));
            palettes[0].AddCard(new Card(2, 7));
            palettes[0].AddCard(new Card(3, 4));
            palettes[1].AddCard(new Card(4, 6));
            palettes[1].AddCard(new Card(4, 7));
            palettes[1].AddCard(new Card(2, 1));
            palettes[2].AddCard(new Card(3, 5));
            palettes[2].AddCard(new Card(6, 4));

            canvas.Push(new Card(0, 6));

            PlayTurn(0);
        }
    }
}
