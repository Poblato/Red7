using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_7_GUI
{
    public class Hand
    {
        protected List<Card> hand;

        public Hand()
        {
            hand = new List<Card>();
        }
        public List<Card> Cards { get { return hand; } }
        public int Size { get { return hand.Count; } }
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
            for (int i = 0; i < hand.Count; i++)
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
        }
        public void InsertCard(int index, Card card)
        {
            hand.Insert(index, card);
        }
        public void RemoveCard(Card c)
        {
            RemoveCardByIndex(FindCard(c));
        }
        public Card RemoveCardByIndex(int index)
        {
            Card c = GetCard(index);
            hand.RemoveAt(index);
            return c;
        }
    }
}
