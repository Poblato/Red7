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
        public Client(int numPlayers)
        {
            palettes = new List<Palette>();
            hands = new List<Hand>();
            deck = new Deck();
            scorer = new Scorer();
            canvas = new Card(0, 7);
            players = numPlayers;
            deck.Reset();

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
            while (cont == true)
            {
                if (playedToPalette == false)
                {
                    Console.WriteLine("Enter (1) to play to palette, or (2) to discard to canvas, or (3) to end turn");
                    string input = Console.ReadLine();

                    switch (input)
                    {
                        case "1":
                            PlayToPalette(player);
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
        }
        public void PlayToPalette(int player)
        {
            Console.WriteLine("played to palette");
        }
        public void DiscardToCanvas(int player)
        {
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