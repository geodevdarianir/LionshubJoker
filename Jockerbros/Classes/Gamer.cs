using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class Gamer
    {
        private readonly Table _table;
        public readonly string _name;
        public readonly List<Card> _cardsOnHand = new List<Card>();

        public string Name { get { return _name; } }
        public List<Card> CardsOnHand { get { return _cardsOnHand; } }
        public Gamer(string name, Table table)
        {
            _name = name;
            _table = table;
        }

        public void PutCardAway(Card card)
        {
            _cardsOnHand.Remove(card);
            _table.PlaceCardsOnTheTable(card, this);
        }
    }
}
