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

        private List<Player> DisputePlayers = new();

        public GameLogic(List<Player> players, Action showState)
        {
            this.players = players;
            Deck = new();
            Deck.Full();
            Deck.CutTo(36);
            ShowState = showState;
        }

        public Action ShowState { get; set; } 

        public Player Current { get; set; }

        public Player Human { get; set; }

        public CardSet Deck { get; set; } = new CardSet();

        private Card FirstCard = null;

        public int winner;

        private bool IsDispute = false;

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
            if (IsDispute)
            {
                if (FirstCard == null)
                {
                    FirstCard = card;
                    TableDeck.Add(Current.Hand.Pull(card));
                    Human.Last = TableDeck.Last();
                    return;
                }
                FirstCard = null;
            }

            TableDeck.Add(Current.Hand.Pull(card));
            Human.Last = TableDeck.Last();
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
            if (!IsDispute)
            {
                for (int i = 1; i < players.Count; i++)
                {
                    players[i].Hand.Shuffle();
                    TableDeck.Add(players[i].Hand.Pull());
                }
                MoveResult();
            }
            else
            {
                /*foreach (int i = 0; i < DisputePlayers.Count; i++)
                {
                    DisputePlayers[i].Hand.Shuffle();
                    TableDeck.Add(DisputePlayers[i].Hand.Pull());
                    TableDeck.Add(DisputePlayers[i].Hand.Pull());
                }*/

                foreach (var player in DisputePlayers)
                {
                    if (player == Human) continue;
                    TableDeck.Add(player.Hand.Pull());
                    TableDeck.Add(player.Hand.Pull());
                    player.Last = TableDeck.Last();
                }
                MoveResult();
            }
        }


        private void MoveResult()
        {
            if (IsDispute)
            {
                Card[] cards = new Card[DisputePlayers.Count];

                for (int i = 0; i <= DisputePlayers.Count; i++)
                {
                    cards[i] = players[i].Last;
                }

                CardSet cards2 = new CardSet();
                cards2.Add(cards);
                int MaxCard = (int)cards[0].Rank;
                winner = 0;

                for (int i = 1; i <= DisputePlayers.Count; i++)
                {
                    if (MaxCard < (int)cards[i].Rank)
                    {
                        MaxCard = (int)cards[i].Rank;
                        winner = i;
                    }
                }

                bool firsttime = true;

                int[] disputeplayers = new int[players.Count];
                int j = 0;

                for (int i = 0; i < cards.Count(); i++)
                {
                    if (MaxCard == (int)cards[i].Rank)
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

                if (IsDispute)
                {
                    CardSet cards1 = TableDeck;
                    Dispute(disputeplayers);
                    players[winner].Hand.Add(cards1.Deal(cards1.Count));
                }

                
                players[winner].Hand.Add(cards2.Deal(cards2.Count));
            }
            else
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

                int[] disputeplayers = new int[players.Count];
                int j = 0;

                for (int i = 0; i < cards.Count; i++)
                {
                    if (MaxCard == (int)cards[i].Rank)
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

                if (IsDispute)
                {
                    CardSet cards1 = TableDeck;
                    Dispute(disputeplayers);
                    players[winner].Hand.Add(cards1.Deal(cards1.Count));
                }

                players[winner].Hand.Add(cards.Deal(cards.Count));
            }
        }

        private void Dispute(int[] disputeplayers)
        {
            TableDeck.RemoveRange(0, TableDeck.Count);
            for (int i = 0; i <= disputeplayers.Count(); i++)
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
