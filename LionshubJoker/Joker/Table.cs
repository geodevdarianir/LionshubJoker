﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LionshubJoker.Joker
{
    public class Table
    {
        public class CardAndGamerOnTable
        {
            public int Index { get; set; }
            public Gamer Gamer { get; set; }
            public Card Card { get; set; }
        }
        public class FourCardsAndGamersOnTable
        {
            public List<CardAndGamerOnTable> _fourCardAndGamerOnTable { get; set; }
            public FourCardsAndGamersOnTable()
            {
                _fourCardAndGamerOnTable = new List<CardAndGamerOnTable>();
            }
        }
        public readonly FourCardsAndGamersOnTable _fourCardsAndGamersListOnTheTable = new FourCardsAndGamersOnTable();
        public Table()
        {
            _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable = new List<CardAndGamerOnTable>();
        }
        public void PlaceCardsOnTheTable(Card card, Gamer gamer)
        {

            _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Add(new CardAndGamerOnTable()
            {
                Card = card,
                Gamer = gamer,
            });
        }
        public void TakeCardsFromTable()
        {
            CardAndGamerOnTable firstCardAndGamer = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable[0];
            //variable cardsAndGamerOnTable არის იმდენი კარტი(მისი მფლობელი მოთამაში თურთ) რამდენსაც გვაძლევს ეს ფილტრი. ანუ პირველი კარტის ცვეტის ნაირი კარტები.
            List<CardAndGamerOnTable> cardsAndGamersOnTableOfSameColor = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.ColorOfCard == firstCardAndGamer.Card.ColorOfCard).ToList();
            // cardsAndGamersOnTable.Count == 0 ? _fourCardsAndGamersListOnTheTable._cardsOnTheTable[0].Card.Strength :
            StrengthOfCard strengthOfCardsAndGamersOnTable = cardsAndGamersOnTableOfSameColor.Where(p => p.Card.CardIsJoker() == false).Max(p => p.Card.Strength);

            if (firstCardAndGamer.Card.CardIsJoker() == false)
            {
                //პირველი კარტი არ არის ჯოკერი
                if (cardsAndGamersOnTableOfSameColor.Count == _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Count && _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker()).ToList().Count == 0)
                {
                    // თუ მაგიდაზე ყველა ერთი ცვეტია და არ არის ჯოკერი, მაშინ წაიყვანოს ყველაზე მაღალმა ცვეტის მატარებელმა მოთამაშემ...
                    CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == strengthOfCardsAndGamersOnTable);
                    card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                    card.Gamer.CurrentGamerAfterOneRound = true;
                }
                else if (cardsAndGamersOnTableOfSameColor.Count != _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Count && _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker()).ToList().Count == 0)
                {
                    // როცა მაგიდაზე ერთი ცვეტის კარტი არ არის და არც ჯოკერია => მიყავს ამ ცვეტის მაღალ კარტს თუ კოზირი არაა მაგიდაზე.
                    List<CardAndGamerOnTable> trumpCardsOnTheTable = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump).ToList();
                    if (trumpCardsOnTheTable.Count == 0)
                    {
                        //მიყავს ამ ცვეტის მაღალ კარტს
                        CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == strengthOfCardsAndGamersOnTable);
                        card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                        card.Gamer.CurrentGamerAfterOneRound = true;
                    }
                    else
                    {
                        //მიყავს კოზირს
                        StrengthOfCard maxTrampCard = trumpCardsOnTheTable.Max(p => p.Card.Strength);
                        CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == maxTrampCard);
                        card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                        card.Gamer.CurrentGamerAfterOneRound = true;
                    }
                }
                else if (_fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker()).ToList().Count != 0)
                {
                    //თუ მაგიდაზე ჯოკერი არის დადებული
                    List<CardAndGamerOnTable> jokersOnTable = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker()).ToList();
                    if (jokersOnTable.Count == 1)
                    {
                        //როცა ერთი ჯოკერი გვაქვს
                        CardAndGamerOnTable gamerWithJoker = jokersOnTable[0];
                        //როცა ჯოკერი არის ჯოკრის სიძლიერის
                        if (gamerWithJoker.Card.Strength == StrengthOfCard.Joker)
                        {
                            gamerWithJoker.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                            gamerWithJoker.Gamer.CurrentGamerAfterOneRound = true;
                        }
                        else if (gamerWithJoker.Card.Strength == StrengthOfCard.LowJoker)
                        {
                            //როცა ჯოკერი არის შეცურებული
                            if (cardsAndGamersOnTableOfSameColor.Count == _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Count)
                            {
                                // თუ მაგიდაზე ყველა ერთი ცვეტია და არ არის ჯოკერი, მაშინ წაიყვანოს ყველაზე მაღალმა ცვეტის მატარებელმა მოთამაშემ
                                CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == strengthOfCardsAndGamersOnTable);
                                card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                card.Gamer.CurrentGamerAfterOneRound = true;
                            }
                            else
                            {
                                // როცა მაგიდაზე ერთი ცვეტის კარტი არ არის და არც ჯოკერია => მიყავს ამ ცვეტის მაღალ კარტს თუ კოზირი არაა მაგიდაზე.
                                List<CardAndGamerOnTable> trumpCardsOnTheTable = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump).ToList();
                                if (trumpCardsOnTheTable.Count == 0)
                                {
                                    CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == strengthOfCardsAndGamersOnTable);
                                    card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                    card.Gamer.CurrentGamerAfterOneRound = true;
                                }
                                else
                                {
                                    StrengthOfCard maxTrampCard = trumpCardsOnTheTable.Max(p => p.Card.Strength);
                                    CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == maxTrampCard);
                                    card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                    card.Gamer.CurrentGamerAfterOneRound = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        //როცა ორივე ჯოკერი დადებულია მაგიდაზე
                        if (jokersOnTable.Count > 1)
                        {
                            CardAndGamerOnTable jokerCard1 = jokersOnTable[0];
                            CardAndGamerOnTable jokerCard2 = jokersOnTable[0];
                            //როცა პირველი და მეორე ჯოკერიც ჯოკრის სიძლიერისაა
                            if (jokerCard1.Card.Strength == StrengthOfCard.Joker && jokerCard2.Card.Strength == StrengthOfCard.Joker)
                            {
                                jokerCard2.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                jokerCard2.Gamer.CurrentGamerAfterOneRound = true;
                            }
                            //როცა პირველი შეცურებულია და მეორე ჯოკერი ჯოკრავს
                            else if (jokerCard1.Card.Strength == StrengthOfCard.LowJoker && jokerCard2.Card.Strength == StrengthOfCard.Joker)
                            {
                                jokerCard2.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                jokerCard2.Gamer.CurrentGamerAfterOneRound = true;
                            }
                            //როცა პირველი ჯოკრავს და მეორე შეცურებული
                            else if (jokerCard1.Card.Strength == StrengthOfCard.Joker && jokerCard2.Card.Strength == StrengthOfCard.LowJoker)
                            {
                                jokerCard1.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                jokerCard1.Gamer.CurrentGamerAfterOneRound = true;
                            }
                            //როცა პირველი არის შეცურებული და მეორეც შეცურებული
                            else if (jokerCard1.Card.Strength == StrengthOfCard.LowJoker && jokerCard2.Card.Strength == StrengthOfCard.LowJoker)
                            {
                                if (cardsAndGamersOnTableOfSameColor.Count == _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Count)
                                {
                                    // თუ მაგიდაზე ყველა ერთი ცვეტია და არ არის ჯოკერი, მაშინ წაიყვანოს ყველაზე მაღალმა ცვეტის მატარებელმა მოთამაშემ...
                                    CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == strengthOfCardsAndGamersOnTable);
                                    card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                    card.Gamer.CurrentGamerAfterOneRound = true;
                                }
                                else
                                {
                                    // როცა მაგიდაზე ერთი ცვეტის კარტი არ არის და არც ჯოკერია => მიყავს ამ ცვეტის მაღალ კარტს თუ კოზირი არაა მაგიდაზე.
                                    List<CardAndGamerOnTable> trumpCardsOnTheTable = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump).ToList();
                                    if (trumpCardsOnTheTable.Count == 0)
                                    {
                                        //თუ ყველა სხვადასხვა ცვეტისაა, პირველი რა ცვეტიც ჩამოვიდა ის წაიყვანს ანუ პირველი წაიყვანს იმიტო რო უნიკალურია
                                        CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == strengthOfCardsAndGamersOnTable);
                                        card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                        card.Gamer.CurrentGamerAfterOneRound = true;
                                    }
                                    else
                                    {
                                        StrengthOfCard maxTrampCard = trumpCardsOnTheTable.Max(p => p.Card.Strength);
                                        CardAndGamerOnTable card = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.First(p => p.Card.Strength == maxTrampCard);
                                        card.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                        card.Gamer.CurrentGamerAfterOneRound = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                //პირველი კარტია ჯოკერი
                if (firstCardAndGamer.Card.Strength == StrengthOfCard.Joker)
                {
                    //პირველი ჯოკერი ითხოვს ვიშს
                    if (firstCardAndGamer.Card.GiveAndTake != CardColor.None)
                    {
                        //GiveAndTake => ვსაზღვრავ პირველი ჯოკრის გარდა, ვინმე ჩამოვიდა თუ არა ჯოკერს მის შემდეგ. მეორის GiveAndTake= None
                        List<CardAndGamerOnTable> cardsAndGamerOnTable = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.ColorOfCard == firstCardAndGamer.Card.GiveAndTake && p.Card.CardIsJoker() && p.Card.IsTrump).ToList();
                        if (cardsAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && cardsAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count == 0)
                        {
                            //ტრამპი არ არის ჩამოსული და მეორე ჯოკერი არ არის ჩამოსული
                            //StrengthOfCard maxCardOfSameColor = cardsOnTheTable.Max(p => p.Card.Strength);
                            //CardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            //CardsOnTheTable card = _cardsOnTheTable.First(p => p.Card.Strength == maxCardOfSameColor);
                            //cardsOnTheTable.Gamer.TookenCardFromTable.Add(card);

                            firstCardAndGamer.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                            firstCardAndGamer.Gamer.CurrentGamerAfterOneRound = true;
                        }
                        else if (cardsAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && cardsAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count == 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერი არა
                            CardColor colorOfTrumCard = cardsAndGamerOnTable.Where(p => p.Card.IsTrump == true).First().Card.ColorOfCard;
                            if (firstCardAndGamer.Card.GiveAndTake == colorOfTrumCard)
                            {
                                //მიყავს ჯოკერს
                                firstCardAndGamer.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);


                            }
                            else
                            {
                                // მიყავს კოზირს
                                StrengthOfCard trumpStrenght = cardsAndGamerOnTable.Where(p => p.Card.IsTrump).Max(p => p.Card.Strength);
                                CardAndGamerOnTable cardAndGamerOnTable = cardsAndGamerOnTable.Where(p => p.Card.Strength == trumpStrenght).First();
                                cardAndGamerOnTable.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                            }

                            //StrengthOfCard maxTrump = cardsOnTheTable.Where(p => p.Card.IsTrump == true).Max(p => p.Card.Strength);
                            //FourCardsOnTheTable card = _fourCardsAndGamersListOnTheTable._cardsOnTheTable.First(p => p.Card.Strength == maxTrump);
                            //card.Gamer.TakenCardFromTable.Add(card);
                        }
                        else if (cardsAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && cardsAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count != 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერიც
                            CardAndGamerOnTable jokerGamer = cardsAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).First();
                            jokerGamer.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                        }
                        else if (cardsAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && cardsAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count != 0)
                        {
                            //კოზირი არ არის დარტყმული და ჯოკერი დარტყმულია
                            //FourCardsOnTheTable card = cardsOnTheTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList()[0];
                            //card.Gamer.TakenCardFromTable.Add(card);
                            CardAndGamerOnTable jokerGamer = cardsAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).First();
                            jokerGamer.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                        }
                    }
                }
                else
                {
                    // აცხადებს რომელ ცვეტს უნდა გაატანოს
                    if (firstCardAndGamer.Card.GiveAndTake != CardColor.None)
                    {
                        //List<CardAndGamerOnTable> cardsOnTheTable = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.ColorOfCard == firstCardAndGamer.Card.GiveAndTake && p.Card.CardIsJoker() && p.Card.IsTrump).ToList();
                        if (_fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count == 0)
                        {
                            //ტრამპი და ჯოკერი არ არის დარტყმული
                            if (_fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.ColorOfCard == firstCardAndGamer.Card.GiveAndTake).Count() != 0)
                            {
                                firstCardAndGamer.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                firstCardAndGamer.Gamer.CurrentGamerAfterOneRound = true;
                            }
                            else
                            {
                                StrengthOfCard strengthOfCard = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.ColorOfCard == firstCardAndGamer.Card.GiveAndTake).Max(p => p.Card.Strength);
                                CardAndGamerOnTable winnnerGamer = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.Strength == strengthOfCard).First();
                                winnnerGamer.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                                winnnerGamer.Gamer.CurrentGamerAfterOneRound = true;
                            }
                        }
                        else if (_fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 &&
                            _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count == 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერი არა
                            StrengthOfCard maxTrump = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Max(p => p.Card.Strength);
                            CardAndGamerOnTable winner = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.Strength == maxTrump).First();
                            winner.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                            winner.Gamer.CurrentGamerAfterOneRound = true;
                        }
                        else if (_fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count != 0 && _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count != 0)
                        {
                            //კოზირი დარტყმულია და ჯოკერიც
                            CardAndGamerOnTable winner = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).First();
                            winner.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                            winner.Gamer.CurrentGamerAfterOneRound = true;
                        }
                        else if (_fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.IsTrump == true).ToList().Count == 0 && _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).ToList().Count != 0)
                        {
                            //კოზირი არ არის დარტყმულია და ჯოკერი დარტყმულია
                            CardAndGamerOnTable winner = _fourCardsAndGamersListOnTheTable._fourCardAndGamerOnTable.Where(p => p.Card.CardIsJoker() == true && p.Card.GiveAndTake == CardColor.None).First();
                            winner.Gamer.TakenFourCardAndGamerFromTable.Add(_fourCardsAndGamersListOnTheTable);
                            winner.Gamer.CurrentGamerAfterOneRound = true;
                        }
                    }
                }
            }
        }
    }
}