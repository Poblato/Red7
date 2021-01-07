using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7._0
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
        }
        private bool CheckWinner(int currentPlayer)
        {
            return scorer.Score(palettes, currentPlayer, canvas.Peek().Colour);
        }
        public void PlayTurn(int player)
        {
            bool cont = true;
            bool playedToPalette = false;
Turn:
            while (cont == true)
            {
                if (playedToPalette == false)
                {
                    Console.WriteLine("Enter (1) to play to palette, or (2) to discard to canvas, or (3) to end turn, or (4) to undo");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            playedToPalette = true;
                            PlayToPalette(player, ref playedToPalette);
                            break;
                        case "2":
                            DiscardToCanvas(player);
                            cont = false;
                            break;
                        case "3":
                            cont = false;
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
                            DiscardToCanvas(player);
                            cont = false;
                            break;
                        case "2":
                            cont = false;
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
                        cont = true;
                        playedToPalette = false;
                        goto Turn;
                    case "3":
                        break;
                    default:
                        Console.WriteLine("no");
                        goto LossConfirmation;
                }
            }
        }
        public void Undo()
        {
            //undoes the last action taken
        }
        public void PlayToPalette(int player, ref bool playedToPalette)
        {
            Card card = new Card(0, 0);
            int startIndex = 0;
            int[] endPos = new int[2];
            int index;
            int target;

            Console.WriteLine("Choose card to play, cards in hand: " + hands[player].Size);
            startIndex = int.Parse(Console.ReadLine());
            endPos[0] = 1;
            endPos[1] = player;
            endPos[2] = palettes[player].Size;

            Action action = new Action("playToPalette", card);
            action.Start = new int[] { 0, player, startIndex };
            action.Endpoint = endPos;

            actions.Push(action);

            if (actionRule && card.Rank % 2 == 1)
            {
                switch (card.Rank)
                {
                    case 1:
                        //discard a card from other players' palette (that player must have more or same cards in palette than current player)
                        Console.WriteLine("Choose a player to discard from (0 - {0})", (players - 1).ToString());
                        target = int.Parse(Console.ReadLine());
                        Console.WriteLine("Choose a card to discard, cards in palette: " + palettes[target].Size);
                        index = int.Parse(Console.ReadLine());
                        Console.WriteLine("Choose whether to discard to canvas (-1) or deck (-2)");
                        int choice = int.Parse(Console.ReadLine());
                        action = DiscardCard(new int[] { 1, target, index }, choice );
                        action.End = false;
                        actions.Push(action);
                        break;
                    case 3:
                        card = deck.DrawCard();
                        hands[player].AddCard(card);//draw a card
                        action = new Action("drawCard", card);
                        action.Endpoint = new int[] { 0, player, hands[player].Size - 1 };
                        actions.Push(action);
                        break;
                    case 5:
                        playedToPalette = false;//allows the player to play another card 
                        break;
                    case 7:
                        //discard a card from player's palette
                        Console.WriteLine("Choose a card to discard, cards in palette: " + palettes[player].Size);
                        index = int.Parse(Console.ReadLine());
                        Console.WriteLine("Choose whether to discard to canvas (-1) or deck (-2)");
                        target = int.Parse(Console.ReadLine());
                        action = DiscardCard(new int[] { 1, player, index }, target);
                        action.End = false;
                        actions.Push(action);
                        break;
                    default:
                        throw new Exception("Invalid odd card rank in palette " + player.ToString());
                }
            }
            Console.WriteLine("played to palette");
        }
        private void DiscardToCanvas(int player)
        {
            Card card = new Card(0, 0);
            int[] startPos = new int[3];
            startPos[0] = 0;//discard from hand
            startPos[1] = player;//discard from current player

            Console.WriteLine("Choose card to discard, cards in hand: " + hands[player].Size);
            int index = int.Parse(Console.ReadLine());

            startPos[2] = index;//which specific card to discard

            Action action = DiscardCard(startPos, -1);// -1 to discard to canvas

            action.End = true;

            actions.Push(action);

            if (advanced == true)
            {
                if (card.Rank > palettes[player].Size)
                {
                    hands[player].AddCard(deck.DrawCard());//draws a card
                }
            }
            Console.WriteLine("discarded to canvas");
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
            action.Start = startPos;


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
            action.Endpoint = new int[] { 1, target, targetPos };
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