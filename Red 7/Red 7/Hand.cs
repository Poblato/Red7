using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7._0
{
    public class Hand
    {
        protected List<Card> hand;
        protected int size;

        public Hand()
        {
            hand = new List<Card>();
        }
        public List<Card> Cards { get { return hand; } }
        public int Size { get { return size; } }
        public Card GetCard(int index)
        {
            if (index > hand.Count)
            {
                return new Card(0, 0);
            }
            else
            {
                return hand[index];
            }
        }
        public int FindCard(Card card)
        {
            for (int i = 0; i < size; i++)
            {
                Card c = hand[i];
                if (c.GetScore() == card.GetScore())
                {
                    return i;
                }
            }
            return -1;
        }
        public void AddCard(Card card)
        {
            hand.Add(card);
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