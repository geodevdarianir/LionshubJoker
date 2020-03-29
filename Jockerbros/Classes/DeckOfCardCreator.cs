using System;
using System.Collections.Generic;
using System.Linq;
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
                if (color != CardColor.None)
                {
                    foreach (CardValue value in allValueOfCard)
                    {
                        index++;
                        Card card = new Card(color, value, index);
                        if (card.CardIsJoker())
                        {
                            card.AllowsCardOnTheTable = true;
                        }
                        deckOfCards.Add(card);
                    }
                }
            }
            return deckOfCards;
        }
    }
}
