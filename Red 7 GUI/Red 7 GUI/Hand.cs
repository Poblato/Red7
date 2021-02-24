using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red_7_GUI
{
    public class Hand
    {
        protected List<Card> cards;

        public Hand()
        {
            cards = new List<Card>();
        }
        public List<Card> Cards { get { return cards; } }
        public int Size { get { return cards.Count; } }
        public Card GetCard(int index)
        {
            if (index > cards.Count)
            {
                return new Card(0, 0);
            }
            else
            {
                return cards[index];
            }
        }
        public int FindCard(Card card)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                Card c = cards[i];
                if (c.GetScore() == card.GetScore())
                {
                    return i;
                }
            }
            return -1;
        }
        public void AddCard(Card card)
        {
            cards.Add(card);
        }
        public void InsertCard(int index, Card card)
        {
            cards.Insert(index, card);
        }
        public void RemoveCard(Card card)
        {
            RemoveCardByIndex(FindCard(card));
        }
        public Card RemoveCardByIndex(int index)
        {
            Card c = GetCard(index);
            cards.RemoveAt(index);
            return c;
        }
    }
}
