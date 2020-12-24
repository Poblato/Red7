using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7_v2._0
{
    public class Palette : Hand
    {
        public List<Card> GetEvenCards()
        {
            List<Card> cards = new List<Card>();
            Card c;

            for (int i = 0; i < size; i++)
            {
                c = GetCard(i);
                if (c.Rank % 2 == 0)
                {
                    cards.Add(c);
                }
            }
            return cards;
        }
        public List<Card> FindCardsByNumber(int num)
        {
            List<Card> cards = new List<Card>();
            Card c;

            for (int i = 0; i < size; i++)
            {
                c = GetCard(i);
                if (c.Rank == num)
                {
                    cards.Add(c);
                }
            }
            return cards;
        }
        public List<Card> FindCardsByColour(int col)
        {
            List<Card> cards = new List<Card>();
            Card c;

            for (int i = 0; i < size; i++)
            {
                c = GetCard(i);
                if (c.Colour == col)
                {
                    cards.Add(c);
                }
            }
            return cards;
        }
    }
}
