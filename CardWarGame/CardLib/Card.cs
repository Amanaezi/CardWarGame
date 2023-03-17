using System;
using System.Collections.Generic;

namespace CardLib
{
    public class Card
    {
        private static Dictionary<CardRank, string> strRanks = new Dictionary<CardRank, string>();
        private static Dictionary<CardSuit, string> strSuits = new Dictionary<CardSuit, string>()
        {
            [CardSuit.Club] = "♣",
            [CardSuit.Diamond] = "♦",
            [CardSuit.Spade] = "♠",
            [CardSuit.Heart] = "♥"
        };
        static Card()
        {
            for(int i = 2; i <= 10; i++)
            {
                strRanks[(CardRank)i] = i.ToString();
            }
            strRanks[CardRank.Jack] = "J";
            strRanks[CardRank.Queen] = "Q";
            strRanks[CardRank.King] = "K";
            strRanks[CardRank.Ace] = "A";
        }
        public Card(CardRank rank, CardSuit suit)
        {
            Rank = rank;
            Suit = suit;
        }

        public CardRank Rank { get; set; }
        public CardSuit Suit { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Card card &&
                   Rank == card.Rank &&
                   Suit == card.Suit;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Rank, Suit);
        }

        public override string ToString()
        {
            return $"{strRanks[Rank]}{strSuits[Suit]}";
        }  
    }
}
