using System;
using System.Collections.Generic;
using LionshubJoker.Joker;

namespace LionshubJoker
{
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Table table = new Table();
    //        // მოთამაშეების განსაზღვრა
    //        var gamers = new List<Gamer>
    //        {
    //            new Gamer (1,"Giorgi Labadze",table),
    //            new Gamer(2,"Rati Devdariani",table),
    //            new Gamer(3,"Giorgi Romanadze",table),
    //            new Gamer(4,"Anuki Devdariani",table)
    //        };


    //        // კარტის დასტის შექმნა
    //        DeckOfCardCreator deckOfCardCreator = new DeckOfCardCreator();
    //        // კარტის დასტა
    //        List<Card> deckOfCard = new List<Card>();
    //        deckOfCard.AddRange(deckOfCardCreator.CreateDeckOfCards());

    //        // კარტის დასტის აჩეხვა
    //        var mixDckOfCard = new MixDeckOfCard(deckOfCard);

    //        // პირველი ხელი /// 1 კარტის დარიგება თითო მოთამაშეზე
    //        PlayGame play = new PlayGame(gamers, deckOfCard, CardsOnRound.Four);
    //        play.StartRound();
    //        Console.WriteLine("");
    //        Console.WriteLine("TrumpCard {0}", play.TrumpCard.ToString());

    //        while (table._cardsOnTheTable.Count != gamers.Count)
    //        {
    //            int indexOfGamer = gamers.IndexOf(play.CurrentGamer);
    //            play.CurrentGamer.AllowCardsForTable(play.TrumpCard);
    //            Console.WriteLine(play.Status);
    //            foreach (Card card in play.CurrentGamer.CardsOnHand)
    //            {
    //                Console.WriteLine("Gamer: {0} - {1}", play.CurrentGamer._name, card.ToString());

    //            }
    //            int indexOfCard = Convert.ToInt32(Console.ReadLine());

    //            if (play.CurrentGamer.CardsOnHand[indexOfCard].CardIsJoker())
    //            {
    //                Console.WriteLine($"{play.CurrentGamer.Name} needs max Color : ");
    //                Array colors = Enum.GetValues(typeof(CardColor));
    //                foreach (var item in colors)
    //                {
    //                    Console.WriteLine($"{Convert.ToInt32(item)}: {item.ToString()}");
    //                }
    //                int indexOfColor = Convert.ToInt32(Console.ReadLine());
    //                switch (indexOfColor)
    //                {
    //                    case (int)CardColor.Spades:
    //                        play.CurrentGamer.AllowMaxCardsForTable(CardColor.Spades, play.TrumpCard);
    //                        break;
    //                    case (int)CardColor.Hearts:
    //                        play.CurrentGamer.AllowMaxCardsForTable(CardColor.Hearts, play.TrumpCard);
    //                        break;
    //                    case (int)CardColor.Diamonds:
    //                        play.CurrentGamer.AllowMaxCardsForTable(CardColor.Diamonds, play.TrumpCard);
    //                        break;
    //                    case (int)CardColor.Clubs:
    //                        play.CurrentGamer.AllowMaxCardsForTable(CardColor.Clubs, play.TrumpCard);
    //                        break;
    //                    case (int)CardColor.None:
    //                        break;
    //                    default:
    //                        break;
    //                }
    //            }
    //            play.CurrentGamer.PutCardAway(play.CurrentGamer.CardsOnHand[indexOfCard]);
    //            if (indexOfGamer == gamers.Count - 1)
    //            {
    //                play.CurrentGamer = gamers[0];
    //            }
    //            else
    //            {
    //                play.CurrentGamer = gamers[indexOfGamer + 1];
    //            }
    //        }
    //    }
    //}
    
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table();
            // მოთამაშეების განსაზღვრა
            var gamers = new List<Gamer>
            {
                new Gamer (1,"Giorgi Labadze",table),
                new Gamer(2,"Rati Devdariani",table),
                new Gamer(3,"Giorgi Romanadze",table),
                new Gamer(4,"Anuki Devdariani",table)
            };


            // კარტის დასტის შექმნა
            DeckOfCardCreator deckOfCardCreator = new DeckOfCardCreator();
            // კარტის დასტა
            List<Card> deckOfCard = new List<Card>();
            deckOfCard.AddRange(deckOfCardCreator.CreateDeckOfCards());

            // კარტის დასტის აჩეხვა
            var mixDckOfCard = new MixDeckOfCard(deckOfCard);

            // პირველი ხელი /// 1 კარტის დარიგება თითო მოთამაშეზე
            PlayGame play = new PlayGame(gamers, deckOfCard, CardsOnRound.Four);
            play.StartRound();
            Console.WriteLine("");
            Console.WriteLine("TrumpCard {0}", play.TrumpCard.ToString());

            while (table._cardsOnTheTable.Count != gamers.Count)
            {
                int indexOfGamer = gamers.IndexOf(play.CurrentGamer);
                play.CurrentGamer.AllowCardsForTable(play.TrumpCard);
                Console.WriteLine(play.Status);
                foreach (Card card in play.CurrentGamer.CardsOnHand)
                {
                    Console.WriteLine("Gamer: {0} - {1}", play.CurrentGamer._name, card.ToString());

                }
                int indexOfCard = Convert.ToInt32(Console.ReadLine());
                if (play.CurrentGamer.CardsOnHand[indexOfCard].CardIsJoker() || 
                                table._cardsOnTheTable[0].Card.CardId == 28 || 
                                table._cardsOnTheTable[0].Card.CardId == 1)
                {
                    Console.WriteLine($"{play.CurrentGamer.Name} needs max Color : ");
                    Array colors = Enum.GetValues(typeof(CardColor));
                    foreach (var item in colors)
                    {
                        Console.WriteLine($"{Convert.ToInt32(item)}: {item.ToString()}");
                    }
                    int indexOfColor = Convert.ToInt32(Console.ReadLine());
                    play.CurrentGamer.PutCardAway(play.CurrentGamer.CardsOnHand[indexOfCard]);
                    if (indexOfGamer == gamers.Count - 1)
                    {
                        play.CurrentGamer = gamers[0];
                    }
                    else
                    {
                        play.CurrentGamer = gamers[indexOfGamer + 1];
                    }
                    switch (indexOfColor)
                    {
                        case (int)CardColor.Spades:
                            play.CurrentGamer.AllowMaxCardsForTable(CardColor.Spades, play.TrumpCard);
                            break;
                        case (int)CardColor.Hearts:
                            play.CurrentGamer.AllowMaxCardsForTable(CardColor.Hearts, play.TrumpCard);
                            break;
                        case (int)CardColor.Diamonds:
                            play.CurrentGamer.AllowMaxCardsForTable(CardColor.Diamonds, play.TrumpCard);
                            break;
                        case (int)CardColor.Clubs:
                            play.CurrentGamer.AllowMaxCardsForTable(CardColor.Clubs, play.TrumpCard);
                            break;
                        case (int)CardColor.None:
                            break;
                        default:
                            break;
                    }
                }
                play.CurrentGamer.PutCardAway(play.CurrentGamer.CardsOnHand[indexOfCard]);
                if (!(table._cardsOnTheTable[0].Card.CardId == 28) &&
                    !(table._cardsOnTheTable[0].Card.CardId == 1))
                {
                    
                    if (indexOfGamer == gamers.Count - 1)
                    {
                        play.CurrentGamer = gamers[0];
                    }
                    else
                    {
                        play.CurrentGamer = gamers[indexOfGamer + 1];
                    }
                }
                
            }
        }
    }
}
