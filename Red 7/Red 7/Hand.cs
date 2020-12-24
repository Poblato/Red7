using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7_v2._0
{
    class Hand
    {
        protected List<Card> hand;
        protected int size;

        public Hand()
        {
            hand = new List<Card>();
        }
        public int Size() {return size;}
        public Card GetCard(int index)
        {
            if (index > hand.Count)
            {
                return -1;
            }
            else
            {
                return hand[index];
            }
        }
        public int FindCard(Card card)
        {
            int index = hand.Find(card);
            return index;
        }
        public void AddCard(int r, int c)
        {
            hand.Add(new Card(r, c));
            size++;
        }
        public void RemoveCard(Card c)
        {
           RemoveCardByIndex(FindCard(c));
        }
        public Card RemoveCardByIndex(int index)
        {
            Card c = GetCard(index);
            hand.RemoveAt(index);
            size--;
            return c;
        }
    }
}
