using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LionshubJoker.Joker
{
    public class DeckOfCardCreator : IDeckOfCardCreator
    {
        /// <summary>
        /// ქმნის კარტის დასტას
        /// </summary>
        /// <returns>კარტის დასტა</returns>
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
                        SetStrengthOfCard(card);
                        deckOfCards.Add(card);
                    }
                }
            }

            return deckOfCards;
        }
        private void SetStrengthOfCard(Card card)
        {
            switch (card.ValueOfCard)
            {
                case CardValue.Six:
                    if (card.CardIsJoker())
                    {
                        card.Strength = StrengthOfCard.Joker;
                    }
                    else
                    {
                        card.Strength = StrengthOfCard.One;
                    }
                    break;
                case CardValue.Seven:
                    card.Strength = StrengthOfCard.Two;
                    break;
                case CardValue.Eight:
                    card.Strength = StrengthOfCard.Three;
                    break;
                case CardValue.Nine:
                    card.Strength = StrengthOfCard.Four;
                    break;
                case CardValue.Ten:
                    card.Strength = StrengthOfCard.Five;
                    break;
                case CardValue.Jack:
                    card.Strength = StrengthOfCard.Six;
                    break;
                case CardValue.Queen:
                    card.Strength = StrengthOfCard.Seven;
                    break;
                case CardValue.King:
                    card.Strength = StrengthOfCard.Eight;
                    break;
                case CardValue.Ace:
                    card.Strength = StrengthOfCard.Nine;
                    break;
                default:
                    break;
            }
        }
    }
}
