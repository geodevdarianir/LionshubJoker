using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class DeckOfCardCreator : IDeckOfCardCreator
    {
        public IList<Card> CreateDeckOfCards()
        {
            var deckOfCards = new List<Card>();
            var allColorOfCard = Enum.GetValues(typeof(CardColor));
            var allValueOfCard = Enum.GetValues(typeof(CardValue));
            int index = 0;
            foreach (CardColor color in allColorOfCard)
            {
                foreach (CardValue value in allValueOfCard)
                {
                    index++;
                    deckOfCards.Add(new Card(color, value, index));
                }
            }
            return deckOfCards;
        }
    }
}
