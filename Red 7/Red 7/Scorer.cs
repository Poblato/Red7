using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7_v2._0
{
	public class Scorer
	{
		public Scorer()
		{
			
		}
		public bool Score(List<Palette> palettes, int currentPlayer, int colour)
        {
			switch (colour)
            {
				case 1:
					return false;
				case 2:
					return false;
				case 3:
					return false;
				case 4:
					return false;
				case 5:
					return false;
				case 6:
					return false;
				case 7:
					return HighestCard(palettes, currentPlayer);
				default:
					throw new Exception("Invalid colour in scoring");
			}
        }
		private Card FindHighestCard(List<Card> cards)
        {
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
		private bool HighestCard(List<Palette> palettes, int currentPlayer)
        {
			Card highestPlayerCard = new Card(0, 0);
			Card highestOpponentCard = new Card(0, 0);
			Card c;
			for (int i = 0; i < palettes.Count; i++)
            {
				if (i == currentPlayer)
                {
					highestPlayerCard = FindHighestCard(palettes[i].hand);
                }
                else
                {
					c = FindHighestCard(palettes[i].hand);
					if (c.GetScore() > highestOpponentCard.GetScore())
                    {
						highestOpponentCard = c;
                    }
                }
            }
			if (highestPlayerCard.GetScore() > highestOpponentCard.GetScore())
            {
				return true;
            }
            else
            {
				return false;
            }
        }
	}
}
