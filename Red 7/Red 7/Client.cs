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
        private Card canvas;
        private Scorer scorer;
        private bool advanced;
        private bool actionRule;
        public Client(int numPlayers, bool advanced, bool actionRule)
        {
            palettes = new List<Palette>();
            hands = new List<Hand>();
            deck = new Deck();
            scorer = new Scorer();
            canvas = new Card(0, 7);
            players = numPlayers;
            deck.Reset();
            this.advanced = advanced;
            this.actionRule = actionRule;

            for (int i = 0; i < numPlayers; i++)
            {
                palettes.Add(new Palette());
                hands.Add(new Hand());
            }
        }
        private bool CheckWinner(int currentPlayer)
        {
            return scorer.Score(palettes, currentPlayer, canvas.Colour);
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
                    Console.WriteLine("Enter (1) to play to palette, or (2) to discard to canvas, or (3) to end turn");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            PlayToPalette(player, ref playedToPalette);
                            playedToPalette = true;
                            break;
                        case "2":
                            DiscardToCanvas(player);
                            cont = false;
                            break;
                        case "3":
                            cont = false;
                            break;
                        default:
                            Console.WriteLine("No");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Enter (1) to discard to canvas, or (2) to end turn");
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
                        //reset
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

            //play to palette

            if (actionRule && card.Rank % 2 == 1)
            {
                switch (card.Rank)
                {
                    case 1:
                        //discard a card from other players' palette (that player must have more or same cards in palette than current player)
                        break;
                    case 3:
                        hands[player].AddCard(deck.DrawCard());//draw a card
                        break;
                    case 5:
                        playedToPalette = false;//allows the player to play another card 
                        break;
                    case 7:
                        //discard a card from player's palette
                        break;
                    default:
                        throw new Exception("Invalid odd card rank in palette " + player.ToString());
                }
            }
            Console.WriteLine("played to palette");
        }
        public void DiscardToCanvas(int player)
        {
            Card card = new Card(0, 0);

            //discard to canvas

            if (advanced == true)
            {
                if (card.Rank > palettes[player].Size)
                {
                    hands[player].AddCard(deck.DrawCard());//draws a card
                }
            }
            Console.WriteLine("discarded to canvas");
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

            canvas = new Card(0, 6);

            PlayTurn(0);
        }
    }
}