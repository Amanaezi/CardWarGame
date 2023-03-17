using CardLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardWarGame
{
    class GameLogic
    {
        private List<Player> players = new();

        private readonly Random random = new();

        private CardSet TableDeck = new CardSet();

        public Player Current { get; set; }

        public CardSet Deck { get; set; } = new CardSet();

        public void Start()
        {
            Deck.Shuffle();

            for(int i = 0; i < 4; i++)
            {
                players[i].Hand.Add(Deck.Deal(9));
            }
            

            Current = players[0];
        }

        public void PickCard(Card card)
        {
            TableDeck.Add(card);
            Current.Hand.Remove(card);
            NextPlayerMove();
        }

        public void PickCard(int n)
        {
            TableDeck.Add(Current.Hand[n]);
            Current.Hand.Remove(n);
            NextPlayerMove();
        }

        private void NextPlayerMove()
        {
            int newCurrPlayer = 0;
            for (int i = 0; i < 3; i++)
            {
                if (Current == players[i])
                {
                    newCurrPlayer = i + 1;
                }
                else
                {
                    newCurrPlayer = 0;
                }
            }
            Current = players[newCurrPlayer];
            if(newCurrPlayer != 0 || newCurrPlayer != 3)
            {
                PickCard(random.Next(Current.Hand.Count));
            }
            else if(newCurrPlayer == 3)
            {
                PickCard(random.Next(Current.Hand.Count));
                MoveResult(TableDeck);
            }
            else
            {
                return;
            }
        }

        private void MoveResult(CardSet cards)
        {
            int MaxCard = (int)cards[0].Rank;
            for(int i = 1; i <= 3; i++)
            {
                if (MaxCard < (int)cards[i].Rank)
                {
                    players[i].Hand.Add(cards);
                    cards.Clear();
                }
                else if(//проверка наличия равных карт, при совпадении, нужно делать спор между игроками, чьи карты совпали)
                {
                    int winner = Dispute();
                    players[winner].Hand.Add(cards);
                    cards.Clear();
                }
                else
                {
                    players[0].Hand.Add(cards);
                    cards.Clear();
                }
            }

            NextPlayerMove();

        }

        private int Dispute()
        {
            /*
             * по правилам игры нужно с колоды вытянуть одну карту рубашкой вверх, вторая идет на войну, 
             игроки в споре делают тоже самое, в случае совпадения сделать еще один спор и так по кругу,
            в случае недостачи карт, игрок забирает все карты на войне.
            Второй вариант, чтобы облегчить жизнь просто первую вытянутую карту не скрывать, но она в бое не считаеться
            Третий вариант, первая же вытянутая карта играеться в споре
            */

            return 0;
        }

        private string GameResult()
        {
            for(int i = 0; i < 4; i++)
            {
                if (Current == players[i])
                {
                    return $"Player {i} Win!";
                }
                else
                {
                    return " ";
                }
            }
        }
    }
}
