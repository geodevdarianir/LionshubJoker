using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LionshubJoker.Joker
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
            if (card.AllowsCardOnTheTable)
            {
                _cardsOnHand.Remove(card);
                _table.PlaceCardsOnTheTable(card, this);
            }
        }

        /// <summary>
        /// ამოწმებს მოთამაშეს აქვს თუ არა ე.წ. ცვეტი ხელში
        /// </summary>
        /// <param name="trumpCard">ე.წ. ცვეტი </param>
        /// <returns></returns>
        public bool ContainsColorOfCardOnHand(CardColor musterCard)
        {
            foreach (Card card in _cardsOnHand)
            {
                if (card.ColorOfCard == musterCard)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// ააქტიურებს შესაბამის მოთამაშის კარტებს. კოზირი, ცვეტი. ჯოკერი ყოველთვის აქტიურია
        /// </summary>
        /// <param name="trumpCard">კოზირი</param>
        public void AllowCardsForTable(Card trumpCard)
        {
            CardColor colorOfCard = _table._cardsOnTheTable.Count == 0 ? CardColor.None : _table._cardsOnTheTable[0].Card.ColorOfCard;
            if (ContainsColorOfCardOnHand(colorOfCard))
            {
                AllowMusterCards(colorOfCard);
            }
            else if (ContainsColorOfCardOnHand(trumpCard.ColorOfCard))
            {
                AllowMusterCards(trumpCard.ColorOfCard);
            }
            else
            {
                foreach (Card item in _cardsOnHand)
                {
                    item.AllowsCardOnTheTable = true;
                }
            }
        }

        /// <summary>
        /// ააქტიურებს ჯოკრის მიერ მოთხოვნილ მაქსიმალურ კარტს. (ვიში ჯვარი, ყვავი, აგური, გული)
        /// </summary>
        /// <param name="cardColorOfMaxCard">მაღალი "ცვეტი"</param>
        public void AllowMaxCardsForTable(CardColor cardColorOfMaxCard, Card trumpCard)
        {
            if (ContainsColorOfCardOnHand(cardColorOfMaxCard))
            {
                AllowMaxCards(cardColorOfMaxCard);
            }
            else if (ContainsColorOfCardOnHand(trumpCard.ColorOfCard))
            {
                AllowMaxCards(trumpCard.ColorOfCard);
            }
            else
            {
                foreach (Card item in _cardsOnHand)
                {
                    item.AllowsCardOnTheTable = true;
                }
            }
        }

        private void AllowMusterCards(CardColor colorOfCard)
        {
            foreach (Card card in CardsOnHand)
            {
                if (card.ColorOfCard == colorOfCard)
                {
                    card.AllowsCardOnTheTable = true;
                }
            }
        }


        private void AllowMaxCards(CardColor colorOfCard)
        {
            int minValue = 0;
            int cardIdOfMaxCard = -1;
            foreach (Card card in CardsOnHand)
            {
                if (Convert.ToInt16(card.ValueOfCard) > minValue)
                {
                    if (cardIdOfMaxCard != -1)
                    {
                        CardsOnHand.ElementAt(cardIdOfMaxCard).AllowsCardOnTheTable = false;
                    }
                    minValue = Convert.ToInt16(card.ValueOfCard);
                    card.AllowsCardOnTheTable = true;
                    cardIdOfMaxCard = card.CardId;
                }
            }


            //foreach (Card card in CardsOnHand)
            //{
            //    if (card.ColorOfCard == colorOfCard)
            //    {
            //        card.AllowsCardOnTheTable = true;
            //    }
            //}
        }

        //public void CheckAllowedCardsForTable(Card trumpCard)
        //{
        //    CardColor colorOfCard = _table._cardsOnTheTable[0].Card.ColorOfCard;
        //    foreach (Card item in _cardsOnHand)
        //    {
        //        if (item.ColorOfCard == colorOfCard || (item.ColorOfCard == CardColor.Clubs && item.ValueOfCard == CardValue.Six) || (item.ColorOfCard == CardColor.Spades && item.ValueOfCard == CardValue.Six) || (item.ColorOfCard == trumpCard.ColorOfCard))
        //        {
        //            item.AllowsCardOnTheTable = true;
        //        }
        //    }
        //}
    }
}
