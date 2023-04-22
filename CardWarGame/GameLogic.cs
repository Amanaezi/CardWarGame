using CardLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        public Player Winner { get; set; }
        public CardSet Deck { get; set; } = new CardSet();
        public bool MoveResultEnabled { get; private set; }

        private bool IsDispute = false;

        private int disputeCards = 0;

        public void Start()
        {
            Deck.Shuffle();
            int count = Deck.Count;
            for(int i = 0; i < players.Count; i++)
            {
                players[i].Hand.Add(Deck.Deal(count / players.Count));
            }
            

            Human = players[0];
            ShowState();
            ShowInfo("put the card on the table");
        }

        public void Turn(Card card)
        {
            if (!Human.IsInRound) return;
            if (MoveResultEnabled) return;

            if(IsDispute)
            {
                
                if (disputeCards == 2) return;
                disputeCards++;
            }
            else
            {
                disputeCards = 0;
            }
            PickCard(card, Human);
        }

        private void PickCard(Card card, Player player)
        {
            if (!player.Hand.Contains(card)) return;

            card.Closed = IsDispute && !player.FirstCardInDisputeOnTable;
            player.FirstCardInDisputeOnTable = card.Closed; 

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
            player.IsInRound = player.Hand.Count > 0;
            
            if (player != Human) return;

            for (int i = 1; i < players.Count; i++)
            {
                if (!players[i].IsInRound) continue;
                    players[i].Hand.Shuffle();
                    PickCard(players[i].Hand.LastCard, players[i]);
            }
            
            ShowState();
            MoveResultEnabled = !player.Last.Closed;
            
        }


        public void MoveResult()
        {
            List<Player> PlayersWithMaxCard = MaxPlayers();

            if (PlayersWithMaxCard.Count == 1)
            {
                TakeCards(PlayersWithMaxCard[0]);
                IsDispute = false;
                MoveResultEnabled = false;
                ShowState();
                return;
            }

            IsDispute = true;
            string str = "";

            foreach (var player in players.Where(p => p.IsInRound))
            {
                player.IsInRound = PlayersWithMaxCard.Contains(player);
            }

            for(int i = 0; i < PlayersWithMaxCard.Count; i++)
            {
                str += $"{PlayersWithMaxCard[i].Name}, ";
            }
            ShowInfo($"{str} in dispute");
            str = "";
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
                MoveResultEnabled = true;
            }
            else
            {
                MoveResultEnabled = false;
            }
            ShowState();
        }
        private void TakeCards(Player roundWinner)
        {
            roundWinner.Hand.Add(TableDeck.Deal(TableDeck.Count));

            foreach(var player in players)
            {
                player.IsInRound = player.Hand.Count > 0;
            }
            ShowInfo($"{roundWinner.Name} won the battle, put the card on the table");
            if(players.Count(p => p.IsInRound) == 1 || !Human.IsInRound)
            {
                ShowInfo(Human.IsInRound ? "Congratulations, you win!" : "You lose!");
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
