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
        public List<Card> CardsOnHand { get { return _cardsOnHand; } }
        public Gamer(int id, string name, Table table)
        {
            _id = id;
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
            if (_table._cardsOnTheTable.Count == 0)
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
        public void AllowMaxCardsForTable(CardColor cardColorOfMaxCard, Card trumpCard)
        {
            if (ContainsColorOfCardOnHand(cardColorOfMaxCard))
            {
                AllowMaxCards(cardColorOfMaxCard,false);
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

        readonly List<int> cardValues = new List<int>();
        private int Sort(int CardValue)
        {
            cardValues.Add(CardValue);
            if(cardValues.Count > 1)
            {
                for (int j = 1; j < cardValues.Count; j++)
                {
                    int value = cardValues[j];
                    int i = j - 1;
                    while (i >= 0 && cardValues[i] > value)
                    {
                        cardValues[i + 1] = cardValues[j];
                        i -= 1;
                    }
                    cardValues[i + 1] = value;
                }
                return cardValues[cardValues.Count - 1];
            }
            return cardValues[0];
        }


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
                int number = 0;
                int index = 0;
                foreach (Card card in CardsOnHand)
                {
                    index++;

                    if (card.ColorOfCard == colorOfCard && !card.CardIsJoker())
                    {
                        number = Sort(Convert.ToInt16(card.ValueOfCard));
                    }

                    if (CardsOnHand.Count == index)
                    {
                        //CardsOnHand.ElementAt(number).AllowsCardOnTheTable = false;
                        foreach (var cardo in CardsOnHand)
                        {
                            if (cardo.ColorOfCard == colorOfCard)
                            {
                                if (Convert.ToInt16(cardo.ValueOfCard) == number)
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

        //private void AllowMaxCard(CardColor colorOfCard)
        //{
          //  int minValue = 0;
            //int cardIdOfMaxCard = -1;
            //foreach (Card card in CardsOnHand)
            //{
              //  if (Convert.ToInt16(card.ValueOfCard) > minValue)
                //{
                  //  if (cardIdOfMaxCard != -1)
                    //{
                      //  CardsOnHand.ElementAt(cardIdOfMaxCard).AllowsCardOnTheTable = false;
                    //}
                    //minValue = Convert.ToInt16(card.ValueOfCard);
                    //card.AllowsCardOnTheTable = true;
                    //cardIdOfMaxCard = card.CardId;
                //}
            //}
            //foreach (Card card in CardsOnHand)
            //{
            //    if (card.ColorOfCard == colorOfCard)
            //    {
            //        card.AllowsCardOnTheTable = true;
            //    }
            //}
        //}

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
