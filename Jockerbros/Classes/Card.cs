using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class Card : IEquatable<Card>
    {
        private readonly CardColor _cardColor;
        private readonly CardValue _cardValue;
        private readonly int _cardId;
        public CardColor ColorOfCard { get { return _cardColor; } }
        public CardValue ValueOfCard { get { return _cardValue; } }

        public Card(CardColor color, CardValue value, int id)
        {
            _cardColor = color;
            _cardValue = value;
            _cardId = id;
        }

        public bool Equals(Card otherCard)
        {
            if (ReferenceEquals(otherCard, null))
            {
                return false;
            }
            return _cardColor == otherCard._cardColor && _cardValue == otherCard._cardValue;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj as Card);
        }

        public override int GetHashCode()
        {
            return _cardValue.GetHashCode() ^ _cardColor.GetHashCode();
        }

        public override string ToString()
        {
            return $"{_cardId} => {_cardValue.ToString()} - {_cardColor.ToString()}";
        }

        public static bool operator ==(Card firstCard, Card secondCard)
        {
            if (ReferenceEquals(null, firstCard))
            {
                return ReferenceEquals(null, secondCard);
            }
            return firstCard.Equals(secondCard);
        }

        public static bool operator !=(Card firstCard, Card secondCard)
        {
            return !(firstCard == secondCard);
        }
    }
}
