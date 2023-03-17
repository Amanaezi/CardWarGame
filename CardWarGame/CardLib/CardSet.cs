using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardLib
{
    public class CardSet : IEnumerable<Card>
    {
        private readonly Random random = new();

        private List<Card> cards = new();

        public int Count { get => cards.Count; }

        public Card LastCard { get => cards[Count - 1]; }

        public CardSet()
        {
        }

        public CardSet(params Card[] cards)
        {
            this.cards = new List<Card>(cards);
        }

        public CardSet(List<Card> cards)
        {
            this.cards = cards;
        }

        public Card this[int i]
        {
            get => cards[i];
            set => cards[i] = value;
        }

        public void Shuffle()
        { 
            for (int i = 0; i <= 5; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    int randNum = random.Next(Count);
                    Card card = cards[j];
                    cards[j] = cards[randNum];
                    cards[randNum] = card;
                }
            }
        }

        public Card? Pull(Card equalsCard)
        {
            Card foundCard = cards.FirstOrDefault(x => x.Equals(equalsCard));
            if (foundCard != null) Remove(foundCard);
            return foundCard;
        }
        public Card? Pull(int index = 0)
        {
            if (index < 0 || index >= Count)
            {
                return null;
            }

            Card card = cards[index];
            Remove(index);
            return card;
        }

        public CardSet Deal(int countOfCards)
        {
            CardSet newCardSet = new();

            if (countOfCards < 1)
                throw new Exception("Counts of card to deal must be greater than zero");

            if (countOfCards > Count) countOfCards = Count;

            for (int i = 0; i < countOfCards; i++)
            {
                newCardSet.Add(Pull());
            }

            return newCardSet;
        }

        public void Sort()
        {
            cards.Sort((card1, card2) => card1.Rank.CompareTo(card2.Rank) == 0 ?
                                        card1.Suit.CompareTo(card2.Suit) :
                                        card1.Rank.CompareTo(card2.Rank));
        }

        public void Add(CardSet Cards)
        {
           foreach(var card in Cards)
            {
                cards.Add(card);
            }
        }

        public void Add(List<Card> cards)
        {
            Add(cards.ToArray());
        }

        public void Add(params Card[] cards)
        {
            this.cards.AddRange(cards);
        }

        public void RemoveRange(int startIndex, int length)
        {
            for(int i = startIndex; i < startIndex + length; i++)
            {
                Remove(cards[i]);
            }
           
        }

        public virtual void Remove(Card card)
        {
            cards.Remove(card);
        }

        public void Remove(int index)
        {
            if(index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("Incorrect index");
            Remove(cards[index]);
        }

        public void CutTo(int countOfCards)
        {
            while (Count > countOfCards)
                Remove(0);
        }
        public void Clear()
        {
            CutTo(0);
        }

        public void Full()
        {
            foreach(CardRank rank in Enum.GetValues(typeof(CardRank)))
            {
                foreach (CardSuit suit in Enum.GetValues(typeof(CardSuit)))
                {
                    Add(new Card(rank, suit));
                }    
            }
        }

        public IEnumerator<Card> GetEnumerator() => cards.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => cards.GetEnumerator();
        
    }
}
