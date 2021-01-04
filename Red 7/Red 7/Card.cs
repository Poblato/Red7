using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7._0
{
    public class Card
    {
        private int rank;
        private int colour;

        public Card(int r, int c)
        {
            rank = r;
            colour = c;
        }
        public int Rank { get { return rank; } }
        public int Colour { get { return colour; } }
        public string GetRankAsString()
        {
            return rank.ToString();
        }
        public string GetColourAsString()
        {
            string[] colours = { "Violet", "Indigo", "Blue", "Green", "Yellow", "Orange", "Red" };
            return colours[colour - 1];
        }
        public string GetName()
        {
            return (GetColourAsString() + " " + GetRankAsString());
        }
        public int GetScore()
        {
            return rank * 7 + colour;
        }
    }
}