using CardLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardWarGame
{
    class Player
    {
        public Player() : this("Noname") { }
        public Player(string name) : this(name, new()) { }
        public Player(string name, CardSet hand)
        {
            Name = name;
            Hand = hand;
        }

        public bool IsInRound { get; set; } = true;
        public bool FirstCardInDisputeOnTable { get; set; } = false;
        public Card Last { get; set; }
        public string Name { get; set; }
        public CardSet Hand { get; set; }
    }
}
