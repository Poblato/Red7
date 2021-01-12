using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_7_GUI
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
        public void Reset(int seed)
        {
            size = constSize;
            for (int i = 0; i < 49; i++)
            {
                deck[i] = new Card((i % 7) + 1, (i / 7) + 1);
            }
            Shuffle(seed);
        }
        public void Shuffle(int seed)
        {
            Random rnd = new Random(seed);

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
        public Card GetCard(int index)
        {
            return deck[index];
        }
        public void AddCard(Card card)
        {
            deck[size] = card;
            size++;
        }
    }
}
