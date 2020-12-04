using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7_v2._0
{
    class Hand
    {
        protected List<Card> hand;
        public Hand()
        {
            hand = new List<Card>();
        }
        public Card GetCardByIndex(int index)
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
        public void AddCard(int r, int c)
        {
            hand.Add(new Card(r, c));
        }
    }
}
