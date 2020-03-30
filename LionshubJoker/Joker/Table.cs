﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LionshubJoker.Joker
{
    public class Table
    {
        public readonly List<CardsOnTheTable> _cardsOnTheTable = new List<CardsOnTheTable>();

        public void PlaceCardsOnTheTable(Card card, Gamer gamer)
        {
            _cardsOnTheTable.Add(new CardsOnTheTable()
            {
                Card = card,
                Gamer = gamer,
            });
        }

      

        public class CardsOnTheTable
        {
            public int Index { get; set; }
            public Gamer Gamer { get; set; }
            public Card Card { get; set; }
        }
    }
}