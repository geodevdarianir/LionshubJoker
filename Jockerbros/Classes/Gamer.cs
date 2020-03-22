using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class Gamer
    {
        public readonly string _name;
        public readonly List<Card> _cardsOnHand = new List<Card>();
        public string Name { get { return _name; } }
        public List<Card> CardsOnHand { get { return _cardsOnHand; } }
        public Gamer(string name)
        {
            _name = name;
        }
    }
}
