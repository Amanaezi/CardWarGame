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
        public List<Player> players = new();

        public CardSet TableDeck = new CardSet();

        public GameLogic(List<Player> players, Action showState, Action<string> showInfo)
        {
            this.players = players;
            Deck = new();
            Deck.Full();
            Deck.CutTo(36);
            ShowState = showState;
            ShowInfo = showInfo;
        }

        public Action ShowState { get; set; }
        public Action<string> ShowInfo { get; set; }
        public Player Human { get; set; }

        public CardSet Deck { get; set; } = new CardSet();

        public int winner;

        private bool IsDispute = false;

        public void Start()
        {
            Deck.Shuffle();

            for(int i = 0; i < players.Count; i++)
            {
                players[i].Hand.Add(Deck.Deal(Deck.Count / players.Count));
            }
            

            Human = players[0];
            ShowState();
        }

        public void Turn(Card card)
        {
            PickCard(card, Human);
        }

        private void PickCard(Card card, Player player)
        {
            if (!player.Hand.Contains(card)) return;

            card.Closed = IsDispute && !player.Firstcard;
            player.Firstcard = !card.Closed; 

            TableDeck.Add(player.Hand.Pull(card));
            player.Last = card;
            AfterTurn(player);
            ShowState();
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

        public void AfterTurn(Player player)
        {
            //player.IsInRound = player.Hand.Count > 0;
            
            if (player != Human) return;

            for (int i = 1; i < players.Count; i++)
            {
                if (!players[i].IsInRound) continue;
                    players[i].Hand.Shuffle();
                    PickCard(players[i].Hand.LastCard, players[i]);
            }

            if(!player.Last.Closed)
            {
                MoveResult();
            }
        }


        private void MoveResult()
        {
            List<Player> PlayersWithMaxCard = MaxPlayers();

            if (PlayersWithMaxCard.Count == 1)
            {
                TakeCards(PlayersWithMaxCard[0]);
                return;
            }

            IsDispute = true;

            foreach (var player in players.Where(p => p.IsInRound))
            {
                player.IsInRound = PlayersWithMaxCard.Contains(player);
            }

            ShowState();

            if (!PlayersWithMaxCard.Contains(Human))
            {
                foreach (var player in PlayersWithMaxCard)
                {
                    player.Hand.Shuffle();
                    PickCard(player.Hand.LastCard, player);
                    if(player.Hand.Count > 0)
                    {
                        PickCard(player.Hand.LastCard, player);
                    }
                    else
                    {
                        player.IsInRound = false;
                    }
                }
            }
        }
        private void TakeCards(Player roundWinner)
        {
            roundWinner.Hand.Add(TableDeck.Deal(TableDeck.Count));

            foreach(var player in players)
            {
                player.IsInRound = player.Hand.Count > 0;
            }

            if(players.Count(p => p.IsInRound) == 1)
            {
                Player winner = players.FirstOrDefault(p => p.IsInRound);
                ShowInfo(winner.Name + "win!");
            }
        }

        private List<Player> MaxPlayers()
        {
            Card MaxCard = null;
            List<Player> maxPlayers = new List<Player>();

            foreach (var player in players)
            {
                if (!player.IsInRound) continue;

                
                if (MaxCard == null || player.Last.Rank > MaxCard.Rank)
                {
                    MaxCard = player.Last;
                }
            }

            foreach (var player in players)
            {
                if (!player.IsInRound) continue;

                if(player.Last.Rank == MaxCard.Rank)
                {
                    maxPlayers.Add(player); 
                }
            }

            return maxPlayers;
        }
    }
}
