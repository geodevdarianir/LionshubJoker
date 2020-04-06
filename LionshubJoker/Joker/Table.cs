using System;
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

        public void TakeCardsFromTable()
        {
            Card firstCardOnTable = _cardsOnTheTable[0].Card;

            List<CardsOnTheTable> cardsOnTheTableSameColor = _cardsOnTheTable.Where(p => p.Card.ColorOfCard == firstCardOnTable.ColorOfCard).ToList();
            StrengthOfCard strengthOfSameColorCard = cardsOnTheTableSameColor.Count == 0 ? _cardsOnTheTable[0].Card.Strength : cardsOnTheTableSameColor.Where(p => p.Card.CardIsJoker() == false).Max(p => p.Card.Strength);
            if (firstCardOnTable.CardIsJoker() == false)
            {
                if (cardsOnTheTableSameColor.Count == _cardsOnTheTable.Count && _cardsOnTheTable.Where(p => p.Card.CardIsJoker()).ToList().Count == 0)
                {
                    // თუ მაგიდაზე ყველა ერთი ცვეტია და არ არის ჯოკერი, მაშინ წაიყვანოს ყველაზე მაღალმა ცვეტის მატარებელმა მოთამაშემ...
                    CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == strengthOfSameColorCard);
                    card.Gamer.TookenCardFromTable.Add(card);
                }
                else if (cardsOnTheTableSameColor.Count != _cardsOnTheTable.Count && _cardsOnTheTable.Where(p => p.Card.CardIsJoker()).ToList().Count == 0)
                {
                    //
                    List<CardsOnTheTable> trumpCardsOnTheTable = _cardsOnTheTable.Where(p => p.Card.IsTrump).ToList();
                    if (trumpCardsOnTheTable.Count == 0)
                    {
                        CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == strengthOfSameColorCard);
                        card.Gamer.TookenCardFromTable.Add(card);
                    }
                    else
                    {
                        StrengthOfCard maxTrampCard = trumpCardsOnTheTable.Max(p => p.Card.Strength);
                        CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxTrampCard);
                        card.Gamer.TookenCardFromTable.Add(card);
                    }
                }
                else if (_cardsOnTheTable.Where(p => p.Card.CardIsJoker()).ToList().Count != 0)
                {
                    List<CardsOnTheTable> jokersOnTable = _cardsOnTheTable.Where(p => p.Card.CardIsJoker()).ToList();
                    if (jokersOnTable.Count == 1)
                    {
                        CardsOnTheTable jokerCard = jokersOnTable[0];
                        if (jokerCard.Card.Strength == StrengthOfCard.Joker)
                        {
                            jokerCard.Gamer.TookenCardFromTable.Add(jokerCard);
                        }
                        else if (jokerCard.Card.Strength == StrengthOfCard.LowJoker)
                        {
                            if (cardsOnTheTableSameColor.Count == _cardsOnTheTable.Count)
                            {
                                // თუ მაგიდაზე ყველა ერთი ცვეტია და არ არის ჯოკერი, მაშინ წაიყვანოს ყველაზე მაღალმა ცვეტის მატარებელმა მოთამაშემ
                                CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == strengthOfSameColorCard);
                                card.Gamer.TookenCardFromTable.Add(card);
                            }
                            else
                            {
                                //
                                List<CardsOnTheTable> trumpCardsOnTheTable = _cardsOnTheTable.Where(p => p.Card.IsTrump).ToList();
                                if (trumpCardsOnTheTable.Count == 0)
                                {
                                    CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == strengthOfSameColorCard);
                                    card.Gamer.TookenCardFromTable.Add(card);
                                }
                                else
                                {
                                    StrengthOfCard maxTrampCard = trumpCardsOnTheTable.Max(p => p.Card.Strength);
                                    CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxTrampCard);
                                    card.Gamer.TookenCardFromTable.Add(card);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (jokersOnTable.Count > 1)
                        {
                            CardsOnTheTable jokerCard1 = jokersOnTable[0];
                            CardsOnTheTable jokerCard2 = jokersOnTable[1];

                            if (jokerCard1.Card.Strength == StrengthOfCard.Joker && jokerCard2.Card.Strength == StrengthOfCard.Joker)
                            {
                                jokerCard2.Gamer.TookenCardFromTable.Add(jokerCard2);
                            }
                            else if (jokerCard1.Card.Strength == StrengthOfCard.LowJoker && jokerCard2.Card.Strength == StrengthOfCard.Joker)
                            {
                                jokerCard2.Gamer.TookenCardFromTable.Add(jokerCard2);
                            }
                            else if (jokerCard1.Card.Strength == StrengthOfCard.Joker && jokerCard2.Card.Strength == StrengthOfCard.LowJoker)
                            {
                                jokerCard1.Gamer.TookenCardFromTable.Add(jokerCard1);
                            }
                            else if (jokerCard1.Card.Strength == StrengthOfCard.LowJoker && jokerCard2.Card.Strength == StrengthOfCard.LowJoker)
                            {
                                if (cardsOnTheTableSameColor.Count == _cardsOnTheTable.Count)
                                {
                                    // თუ მაგიდაზე ყველა ერთი ცვეტია და არ არის ჯოკერი, მაშინ წაიყვანოს ყველაზე მაღალმა ცვეტის მატარებელმა მოთამაშემ
                                    CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == strengthOfSameColorCard);
                                    card.Gamer.TookenCardFromTable.Add(card);
                                }
                                else
                                {
                                    //
                                    List<CardsOnTheTable> trumpCardsOnTheTable = _cardsOnTheTable.Where(p => p.Card.IsTrump).ToList();
                                    if (trumpCardsOnTheTable.Count == 0)
                                    {
                                        CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == strengthOfSameColorCard);
                                        card.Gamer.TookenCardFromTable.Add(card);
                                    }
                                    else
                                    {
                                        StrengthOfCard maxTrampCard = trumpCardsOnTheTable.Max(p => p.Card.Strength);
                                        CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxTrampCard);
                                        card.Gamer.TookenCardFromTable.Add(card);
                                    }
                                }
                            }

                        }
                    }



                }
            }
            else
            {
                if (firstCardOnTable.Strength == StrengthOfCard.Joker)
                {
                    if (firstCardOnTable.GiveAndTake != CardColor.None)
                    {
                        List<CardsOnTheTable> cardsOnTheTable = _cardsOnTheTable.Where(p => p.Card.ColorOfCard == firstCardOnTable.GiveAndTake && p.Card.CardIsJoker() && p.Card.IsTrump).ToList();
                        if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count == 0)
                        {
                            //ტრამპი და ჯოკერი არ არის დარტყმული
                            //StrengthOfCard maxCardOfSameColor = cardsOnTheTable.Max(p => p.Card.Strength);
                            //CardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            //CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxCardOfSameColor);
                            //cardsOnTheTable.Gamer.TookenCardFromTable.Add(card);
                        }
                        else if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count == 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერი არა
                            StrengthOfCard maxTrump = cardsOnTheTable.Where(p => p.Card.IsTrump == true).Max(p => p.Card.Strength);
                            CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxTrump);
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                        else if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count != 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერიც
                            CardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                        else if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count != 0)
                        {
                            //კოზირი არ არის დარტყმულია და ჯოკერი დარტყმულია
                            CardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                    }
                }
                else
                {
                    if (firstCardOnTable.GiveAndTake != CardColor.None)
                    {
                        List<CardsOnTheTable> cardsOnTheTable = _cardsOnTheTable.Where(p => p.Card.ColorOfCard == firstCardOnTable.GiveAndTake && p.Card.CardIsJoker() && p.Card.IsTrump).ToList();
                        if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count == 0)
                        {
                            //ტრამპი და ჯოკერი არ არის დარტყმული
                            StrengthOfCard maxCardOfSameColor = cardsOnTheTable.Max(p => p.Card.Strength);
                            CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxCardOfSameColor);
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                        else if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count == 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერი არა
                            StrengthOfCard maxTrump = cardsOnTheTable.Where(p => p.Card.IsTrump == true).Max(p => p.Card.Strength);
                            CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxTrump);
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                        else if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count != 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერიც
                            CardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                        else if (cardsOnTheTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true).ToList().Count != 0)
                        {
                            //კოზირი არ არის დარტყმულია და ჯოკერი დარტყმულია
                            CardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            card.Gamer.TookenCardFromTable.Add(card);
                        }
                    }
                }
            }
        }

        public class CardsOnTheTable
        {
            public int Index { get; set; }
            public Gamer Gamer { get; set; }
            public Card Card { get; set; }
        }
    }
}