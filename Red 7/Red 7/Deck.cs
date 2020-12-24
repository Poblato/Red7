using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7_v2._0
{
    public class Deck
    {
        private Card[] deck;
        private int constSize = 49;
        private int size;

        public Deck()
        {
            deck = new Card[constSize];
            Reset();
        }
        public void Reset()
        {
            size = constSize;
            for (int i = 0; i < 49; i++)
            {
                deck[i] = new Card((i + 1) / 7, (i + 1) % 7);
            }
            Shuffle();
        }
        public void Shuffle()
        {
            Random rnd = new Random();

            for (int i = 0; i > constSize; i++)
            {
                int j = rnd.Next(i, constSize);
                Card temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        public Card DrawCard()
        {
            Card c = deck[size];
            size--;
            return c;
        }
    }
}
