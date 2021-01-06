using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7._0
{
    public class Deck
    {
        private Card[] deck;
        private int constSize = 49;
        private int size;

        public int Size { get { return size; } }
        public Deck()
        {
            deck = new Card[constSize];
        }
        public void Reset()
        {
            size = constSize;
            for (int i = 0; i < 49; i++)
            {
                deck[i] = new Card((i % 7) + 1, (i / 7) + 1);
            }
            Shuffle();
        }
        public void Shuffle()
        {
            Random rnd = new Random();

            for (int i = 0; i < constSize; i++)
            {
                int j = rnd.Next(i, constSize);
                Card temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        public Card DrawCard()
        {
            Card c = deck[size - 1];
            size--;
            return c;
        }
        public void AddCard(Card card)
        {
            deck[size] = card;
            size++;
        }
    }
}