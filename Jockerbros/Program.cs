using System;
using System.Collections.Generic;
using Jockerbros.Classes;

namespace Jockerbros
{
    class Program
    {
        static void Main(string[] args)
        {
            Table table = new Table();
            // მოთამაშეების განსაზღვრა
            var gamers = new List<Gamer>
            {
                new Gamer ("Giorgi Labadze",table),
                new Gamer("Rati Devdariani",table),
                new Gamer("Giorgi Romanadze",table),
                new Gamer("Anuki Devdariani",table)
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
                play.CurrentGamer.PutCardAway(play.CurrentGamer.CardsOnHand[indexOfCard]);

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
