using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class Table
    {
        public readonly List<CardsOnTheTable> _cardsOnTheTable = new List<CardsOnTheTable>();

        public void PlaceCardsOnTheTable(Card card, Gamer gamer)
        {
            _cardsOnTheTable.Add(new CardsOnTheTable()
            {
                Card = card,
                Gamer = gamer
            });
        }

        //public Gamer ChoosWinnerOnTheTable()
        //{
            
        //}
    }

    public class CardsOnTheTable
    {
        public Gamer Gamer { get; set; }
        public Card Card { get; set; }
    }
}
