﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7._0
{
    public class Palette : Hand
    {
        private Card FindHighestCard(List<Card> cards)
        {
            if (cards.Count == 1)
            {
                return cards[0];
            }

            Card highest = new Card(0, 0);
            foreach (Card c in cards)
            {
                if (c.GetScore() > highest.GetScore())
                {
                    highest = c;
                }
            }
            return highest;
        }
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
        public List<Card> GetCardsBelowFour()
        {
            List<Card> cards = new List<Card>();
            Card c;

            for (int i = 0; i < size; i++)
            {
                c = GetCard(i);
                if (c.Rank < 4)
                {
                    cards.Add(c);
                }
            }
            return cards;
        }
        public List<Card> FindLongestRun()
        {
            List<Card> longestRun = new List<Card>();
            List<Card> currentRun = new List<Card>();
            List<Card> cards = new List<Card>();

            for (int i = 1; i < 8; i++)
            {
                cards = FindCardsByNumber(i);
                if (cards.Count > 0)
                {
                    currentRun.Add(FindHighestCard(cards));
                }
                else
                {
                    if (currentRun.Count > longestRun.Count || (currentRun.Count == longestRun.Count && currentRun[currentRun.Count - 1].GetScore() > longestRun[longestRun.Count - 1].GetScore()))
                    {
                        longestRun = currentRun;
                        currentRun.Clear();
                    }
                }
                cards.Clear();
            }

            return longestRun;
        }
    }
}