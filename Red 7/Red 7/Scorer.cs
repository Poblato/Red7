using System;
using System.Collections.Generic;
using System.Text;

namespace Red_7._0
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
					return MostCardsBelowFour(palettes, currentPlayer);
				case 2:
					return MostCardsInARow(palettes, currentPlayer);
				case 3:
					return MostDifferentColours(palettes, currentPlayer);
				case 4:
					return MostEvenCards(palettes, currentPlayer);
				case 5:
					return MostCardsOfOneColour(palettes, currentPlayer);
				case 6:
					return MostCardsOfOneNumber(palettes, currentPlayer);
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
		private bool MostCardsOfOneNumber(List<Palette> palettes, int currentPlayer)
        {
			Card playerHighestCard = new Card(0, 0);
			Card opponentHighestCard = new Card(0, 0);
			int playerMostCards = 0;
			int opponentMostCards = 0;
			List<Card> cards;

			for (int i = 0; i < palettes.Count; i++)
			{
				for (int x = 1; x < 8; x++)
                {
					cards = palettes[i].FindCardsByNumber(x);
					if (currentPlayer == i)
                    {
						if (cards.Count > playerMostCards || (cards.Count == playerMostCards && FindHighestCard(cards).GetScore() > playerHighestCard.GetScore()))
                        {
							playerMostCards = cards.Count;
							playerHighestCard = FindHighestCard(cards);
                        }
                    }
                    else
                    {
						if (cards.Count > opponentMostCards || (cards.Count == opponentMostCards && FindHighestCard(cards).GetScore() > opponentHighestCard.GetScore()))
						{
							opponentMostCards = cards.Count;
							opponentHighestCard = FindHighestCard(cards);
						}
					}
                }
			}
			if (playerMostCards > opponentMostCards || (playerMostCards == opponentMostCards && playerHighestCard.GetScore() > opponentHighestCard.GetScore()))
            {
				return true;
            }
            else
            {
				return false;
            }
		}
		private bool MostCardsOfOneColour(List<Palette> palettes, int currentPlayer)
		{
			Card playerHighestCard = new Card(0, 0);
			Card opponentHighestCard = new Card(0, 0);
			int playerMostCards = 0;
			int opponentMostCards = 0;
			List<Card> cards;

			for (int i = 0; i < palettes.Count; i++)
			{
				for (int x = 1; x < 8; x++)
				{
					cards = palettes[i].FindCardsByColour(x);
					if (currentPlayer == i)
					{
						if (cards.Count > playerMostCards || (cards.Count == playerMostCards && FindHighestCard(cards).GetScore() > playerHighestCard.GetScore()))
						{
							playerMostCards = cards.Count;
							playerHighestCard = FindHighestCard(cards);
						}
					}
					else
					{
						if (cards.Count > opponentMostCards || (cards.Count == opponentMostCards && FindHighestCard(cards).GetScore() > opponentHighestCard.GetScore()))
						{
							opponentMostCards = cards.Count;
							opponentHighestCard = FindHighestCard(cards);
						}
					}
				}
			}
			if (playerMostCards > opponentMostCards || (playerMostCards == opponentMostCards && playerHighestCard.GetScore() > opponentHighestCard.GetScore()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MostEvenCards(List<Palette> palettes, int currentPlayer)
        {
			Card playerHighestCard = new Card(0, 0);
			Card opponentHighestCard = new Card(0, 0);
			List<Card> cards;
			int playerEvenCards = 0;
			int opponentEvenCards = 0;

			for (int i = 0; i < palettes.Count; i++)
            {
				cards = palettes[i].GetEvenCards();
				if (currentPlayer == i)
                {
					playerEvenCards = cards.Count;
					playerHighestCard = FindHighestCard(cards);
                }
                else
                {
					if (cards.Count > opponentEvenCards || (cards.Count == opponentEvenCards && FindHighestCard(cards).GetScore() > opponentHighestCard.GetScore()))
                    {
						opponentEvenCards = cards.Count;
						opponentHighestCard = FindHighestCard(cards);
                    }
                }
            }
			if (playerEvenCards > opponentEvenCards || (playerEvenCards == opponentEvenCards && playerHighestCard.GetScore() > opponentHighestCard.GetScore()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MostDifferentColours(List<Palette> palettes, int currentPlayer)
        {
			Card playerHighestCard = new Card(0, 0);
			Card opponentHighestCard = new Card(0, 0);
			int playerColours = 0;
			int opponentColours = 0;
			List<int> colours = new List<int>();
			for (int i = 0; i < palettes.Count; i++)
            {
				if (i == currentPlayer)
                {
					for (int x = 0; x < palettes[i].Size; i++)
                    {
						if (!colours.Contains(palettes[i].GetCard(x).Colour))
						{
							colours.Add(palettes[i].GetCard(x).Colour);
						}
                    }
					playerColours = colours.Count;
					playerHighestCard = FindHighestCard(palettes[i].hand);
                }
                else
                {
					for (int x = 0; x < palettes[i].Size; i++)
					{
						if (!colours.Contains(palettes[i].GetCard(x).Colour))
						{
							colours.Add(palettes[i].GetCard(x).Colour);
						}
					}
					if (colours.Count > opponentColours || ( colours.Count == opponentColours && FindHighestCard(palettes[i].hand).GetScore() > opponentHighestCard.GetScore()))
                    {
						opponentColours = colours.Count;
						opponentHighestCard = FindHighestCard(palettes[i].hand);
                    }
				}
				colours.Clear();
            }
			if (playerColours > opponentColours || (playerColours == opponentColours && playerHighestCard.GetScore() > opponentHighestCard.GetScore()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MostCardsInARow(List<Palette> palettes, int currentPlayer)
        {
			Card playerHighestCard = new Card(0, 0);
			Card opponentHighestCard = new Card(0, 0);
			List<Card> cards;
			int playerCardsInARow = 0;
			int opponentCardsInARow = 0;

			for (int i = 0; i < palettes.Count; i++)
            {
				cards = palettes[i].FindLongestRun();
				if (i == currentPlayer)
                {
					playerCardsInARow = cards.Count;
					playerHighestCard = cards[cards.Count - 1];
                }
                else
                {
					if (cards.Count > opponentCardsInARow || (cards.Count == opponentCardsInARow && cards[cards.Count - 1].GetScore() > opponentHighestCard.GetScore()))
                    {
						opponentCardsInARow = cards.Count;
						opponentHighestCard = cards[cards.Count - 1];
                    }
                }
            }
			if (playerCardsInARow > opponentCardsInARow || (playerCardsInARow == opponentCardsInARow && playerHighestCard.GetScore() > opponentHighestCard.GetScore()))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		private bool MostCardsBelowFour(List<Palette> palettes, int currentPlayer)
        {
			Card playerHighestCard = new Card(0, 0);
			Card opponentHighestCard = new Card(0, 0);
			List<Card> cards;
			int playerCards = 0;
			int opponentCards = 0;

			for (int i = 0; i < palettes.Count; i++)
            {
				cards = palettes[i].GetCardsBelowFour();
				if (currentPlayer == i)
                {
					playerCards = cards.Count;
					playerHighestCard = FindHighestCard(cards);
                }
                else
                {
					if (cards.Count > opponentCards || (cards.Count == opponentCards && FindHighestCard(cards).GetScore() > opponentHighestCard.GetScore()))
                    {
						opponentCards = cards.Count;
						opponentHighestCard = FindHighestCard(cards);
                    }
                }
            }
			if (playerCards > opponentCards || (playerCards == opponentCards && playerHighestCard.GetScore() > opponentHighestCard.GetScore()))
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
