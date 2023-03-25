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

        public GameLogic(List<Player> players)
        {
            this.players = players;
            Deck = new();
            Deck.Full();
            Deck.CutTo(36);
        }

        public Player Current { get; set; }

        public Player Human { get; set; }

        public CardSet Deck { get; set; } = new CardSet();

        public int winner;

        public void Start()
        {
            Deck.Shuffle();

            for(int i = 0; i < players.Count; i++)
            {
                players[i].Hand.Add(Deck.Deal(Deck.Count / players.Count));
            }
            

            Current = Human = players[0];
        }

        public void PickCard(Card card)
        {
            if (!Current.Hand.Contains(card)) return;
            TableDeck.Add(Current.Hand.Pull(card));
            CompTurn();
            //NextPlayerMove();
        }

        //public void PickCard(int n)
        //{
        //    TableDeck.Add(Current.Hand.Pull(n));
        //    NextPlayerMove();
        //}

        //private void NextPlayerMove();
        //{
        //    int newCurrPlayer = 0;
        //    for (int i = 0; i < 3; i++)
        //    {
        //        if (Current == players[i])
        //        {
        //            newCurrPlayer = i + 1;
        //        }
        //        else
        //        {
        //            newCurrPlayer = 0;
        //        }
        //    }
        //    Current = players[newCurrPlayer];
        //    if(newCurrPlayer != 0 || newCurrPlayer != 3)
        //    {
        //        PickCard(random.Next(Current.Hand.Count));
        //    }
        //    else if(newCurrPlayer == 3)
        //    {
        //        PickCard(random.Next(Current.Hand.Count));
        //        MoveResult(TableDeck);
        //    }
        //    else
        //    {
        //        return;
        //    }


        //}

        public void CompTurn()
        {
            for (int i = 1; i < players.Count; i++)
            {
                players[i].Hand.Shuffle();
                TableDeck.Add(players[i].Hand.Pull(0));
            }
            MoveResult();
        }


        private void MoveResult()
        {
            CardSet cards = TableDeck;
            int MaxCard = (int)cards[0].Rank;
            winner = 0;

            for (int i = 1; i <= cards.Count; i++)
            {
                if (MaxCard < (int)cards[i].Rank)
                {
                    MaxCard = (int)cards[i].Rank;
                    winner = i;
                }
            }

            bool firsttime = true;
            bool IsDispute = false;

            int[] disputeplayers = new int[players.Count];
            int j = 0;
                
                for (int i = 0; i <= cards.Count; i++)
                {
                    if(MaxCard == (int)cards[i].Rank)
                    {
                        if (cards[winner] != cards[i])
                        {
                            IsDispute = true;

                            if (firsttime)
                            {
                                disputeplayers[j] = winner;
                                firsttime = false;
                            }
                            j++;
                            disputeplayers[j] = i;
                        }
                    }
                }

            if(IsDispute)
            {
                TableDeck.Clear();
                Dispute(disputeplayers);
            }
            
            players[winner].Hand.Add(cards.Deal(cards.Count));
        }

        private void Dispute(int[] disputeplayers)
        { 
            for(int i = 0; i <= disputeplayers.Count(); i++)
            {
                TableDeck.Add(players[disputeplayers[i]].Hand.Pull(0));
            }
            MoveResult();
        }

        private string GameResult()
        {
            throw new Exception();
        }
    }
}
