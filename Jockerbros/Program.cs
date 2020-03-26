using System;
using System.Collections.Generic;
using Jockerbros.Classes;

namespace Jockerbros
{
    class Program
    {
        static void Main(string[] args)
        {
            // მოთამაშეების განსაზღვრა
            var gamers = new List<Gamer>
            {
                new Gamer("Rati Devdariani"),
                new Gamer ("Giorgi Labadze"),
                new Gamer("Giorgi Romanadze"),
                new Gamer("Anuki Devdariani")
            };


            // კარტის დასტის შექმნა
            DeckOfCardCreator deckOfCardCreator = new DeckOfCardCreator();
            // კარტის დასტა
            List<Card> deckOfCard = new List<Card>();
            deckOfCard.AddRange(deckOfCardCreator.CreateDeckOfCards());
            //foreach (Card item in deckOfCard)
            //{
            //    Console.WriteLine("{0}", item);
            //}

            // კარტის დასტის აჩეხვა
            var mixDckOfCard = new MixDeckOfCard(deckOfCard);
            //Console.WriteLine("");
            //Console.WriteLine("");
            //foreach (Card item in deckOfCard)
            //{
            //    Console.WriteLine("{0}", item);
            //}

            // პირველი ხელი /// 1 კარტის დარიგება თითო მოთამაშეზე
            GamePlay play = new GamePlay(gamers, deckOfCard);
            play.GameRound(9);

            Console.WriteLine("");
            Console.WriteLine("");
            foreach (Gamer item in play._gamers)
            {
                foreach (Card card in item.CardsOnHand)
                {
                    Console.WriteLine("Gamer: {0} - {1}", item._name, card.ToString());
                }
            }
        }
    }
}
