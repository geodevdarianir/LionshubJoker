using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LionshubJoker.Joker
{
    public class Gamer
    {
        private readonly Table _table;
        public readonly int _id;
        public readonly string _name;
        public readonly List<Card> _cardsOnHand = new List<Card>();

        public string Name { get { return _name; } }
        public int Id { get { return _id; } }
        public List<Card> CardsOnHand
        {
            get
            {
                //_cardsOnHand = _cardsOnHand.OrderByDescending(p => p.ColorOfCard).OrderByDescending(p => p.Strength).ToList();
                return _cardsOnHand;
                /*_cardsOnHand.OrderByDescending(p => p.ColorOfCard).OrderByDescending(p => p.Strength).ToList();*/
            }
        }
        public bool CurrentGamerAfterOneRound { get; set; }
        public List<Table.FourCardsAndGamersOnTable> TakenFourCardAndGamerFromTable { get; set; }
        public Gamer(int id, string name, Table table)
        {
            _id = id;
            _name = name;
            _table = table;
            TakenFourCardAndGamerFromTable = new List<Table.FourCardsAndGamersOnTable>();
        }

        public bool PutCardAway(Card card)
        {
            bool result = false;
            if (card.AllowsCardOnTheTable)
            {
                _cardsOnHand.Remove(card);
                _table.PlaceCardsOnTheTable(card, this);
                result = true;
            }
            return result;
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
            CardColor colorOfCard = _table._fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Count == 0 ? CardColor.None : _table._fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable[0].Card.ColorOfCard;
            if (_table._fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Count == 0)
            {
                foreach (Card item in _cardsOnHand)
                {
                    item.AllowsCardOnTheTable = true;
                }
            }
            else
            {
                foreach (Card item in _cardsOnHand)
                {
                    if (ContainsColorOfCardOnHand(colorOfCard))
                    {
                        AllowMusterCards(colorOfCard);
                    }
                    else
                    {
                        if (ContainsColorOfCardOnHand(trumpCard.ColorOfCard))
                        {
                            AllowMusterCards(trumpCard.ColorOfCard);
                        }
                        else
                        {
                            item.AllowsCardOnTheTable = true;
                        }
                    }
                    if (item.CardIsJoker())
                    {
                        item.AllowsCardOnTheTable = true;
                    }
                }
            }
        }

        /// <summary>
        /// ააქტიურებს ჯოკრის მიერ მოთხოვნილ მაქსიმალურ კარტს. (ვიში ჯვარი, ყვავი, აგური, გული)
        /// </summary>
        /// <param name="cardColorOfMaxCard">მაღალი "ცვეტი"</param>
        //public void AllowMaxCardsForTable(CardColor cardColorOfMaxCard, Card trumpCard)
        //{
        //    if (ContainsColorOfCardOnHand(cardColorOfMaxCard))
        //    {
        //        AllowMaxCards(cardColorOfMaxCard);
        //        AllowJoker();
        //    }
        //    else if (ContainsColorOfCardOnHand(trumpCard.ColorOfCard))
        //    {
        //        AllowMaxCards(trumpCard.ColorOfCard);
        //        AllowJoker();
        //    }
        //    else
        //    {
        //        foreach (Card item in _cardsOnHand)
        //        {
        //            item.AllowsCardOnTheTable = true;
        //        }
        //    }
        //}

        /// <summary>
        /// ააქტიურებს ჯოკერს
        /// </summary>
        private void AllowJoker()
        {
            foreach (Card item in _cardsOnHand)
            {
                if (item.CardIsJoker())
                {
                    item.AllowsCardOnTheTable = true;
                }
            }
        }

        /// <summary>
        /// ცვეტს ააქტიურებს
        /// </summary>
        /// <param name="colorOfCard">კარტის ფერი</param>
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

        /// <summary>
        /// ააქტიურებს ჯოკრის მიერ მოთხოვნილ მაქსიმალურ კარტს. (ვიში ჯვარი, ყვავი, აგური, გული)
        /// </summary>
        /// <param name="cardColorOfMaxCard">მაღალი "ცვეტი"</param>
        public void AllowMaxCardsForTable(CardColor cardColorOfMaxCard, Card trumpCard)
        {
            if (ContainsColorOfCardOnHand(cardColorOfMaxCard))
            {
                AllowMaxCards(cardColorOfMaxCard, false);
                AllowJoker();
            }
            else if (ContainsColorOfCardOnHand(trumpCard.ColorOfCard))
            {
                AllowMaxCards(trumpCard.ColorOfCard, true);
                AllowJoker();
            }
            else
            {
                foreach (Card item in _cardsOnHand)
                {
                    item.AllowsCardOnTheTable = true;
                }
            }
        }



        // private int Sort(int CardValue)
        //{
        //  cardValues.Add(CardValue);
        //if (cardValues.Count > 1)
        //{
        //  for (int j = 1; j < cardValues.Count; j++)
        //{
        //  int value = cardValues[j];
        //int i = j - 1;
        //while (i >= 0 && cardValues[i] > value)
        //{
        //  cardValues[i + 1] = cardValues[j];
        //  i -= 1;
        //}
        //cardValues[i + 1] = value;
        //}
        //return cardValues[cardValues.Count - 1];
        //}
        //return cardValues[0];
        //}

        readonly List<Card> cardValues = new List<Card>();
        private void AllowMaxCards(CardColor colorOfCard, bool trump)
        {
            if (trump)
            {
                foreach (var item in CardsOnHand)
                {
                    if (item.ColorOfCard == colorOfCard)
                    {
                        item.AllowsCardOnTheTable = true;
                    }
                    else
                    {
                        item.AllowsCardOnTheTable = false;
                    }
                }
            }
            else
            {
                StrengthOfCard strength = default;
                int index = 0;
                foreach (Card card in CardsOnHand)
                {
                    index++;

                    if (card.ColorOfCard == colorOfCard && !card.CardIsJoker())
                    {
                        cardValues.Add(card);
                        strength = cardValues.Max(p => p.Strength);
                    }

                    if (CardsOnHand.Count == index)
                    {
                        foreach (var cardo in CardsOnHand)
                        {
                            if (cardo.ColorOfCard == colorOfCard)
                            {
                                if (cardo.Strength == strength)
                                {
                                    cardo.AllowsCardOnTheTable = true;
                                }
                                else
                                {
                                    cardo.AllowsCardOnTheTable = false;
                                }
                            }
                            else
                            {
                                //jokers zevit mainc trued vxdi!
                                cardo.AllowsCardOnTheTable = false;
                            }

                        }
                    }
                }
            }

        }


        //private void AllowMaxCards(CardColor colorOfCard)
        //{
        //    int minValue = 0;
        //    int cardIdOfMaxCard = -1;
        //    foreach (Card card in CardsOnHand)
        //    {
        //        if (Convert.ToInt16(card.ValueOfCard) > minValue)
        //        {
        //            if (cardIdOfMaxCard != -1)
        //            {
        //                CardsOnHand.ElementAt(cardIdOfMaxCard).AllowsCardOnTheTable = false;
        //            }
        //            minValue = Convert.ToInt16(card.ValueOfCard);
        //            card.AllowsCardOnTheTable = true;
        //            cardIdOfMaxCard = card.CardId;
        //        }
        //    }
        //}
    }
}
