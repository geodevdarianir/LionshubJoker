using System;
using System.Collections.Generic;
using Jockerbros.Classes;

namespace Jockerbros
{
    class Program
    {
        static void Main(string[] args)
        {
            var gamers = new List<Gamer>
            {
                new Gamer("Rati Devdariani"),
                new Gamer ("Giorgi Labadze"),
                new Gamer("Giorgi Romanadze"),
                new Gamer("Anuki Devdariani")
            };


            DeckOfCardCreator deckOfCardCreator = new DeckOfCardCreator();
            List<Card> deckOfCard = new List<Card>();
            deckOfCard.AddRange(deckOfCardCreator.CreateDeckOfCards());
            foreach (Card item in deckOfCard)
            {
                Console.WriteLine("{0}", item);
            }

            var mixDckOfCard = new MixDeckOfCard(deckOfCard);
            Console.WriteLine("");
            Console.WriteLine("");
            foreach (Card item in deckOfCard)
            {
                Console.WriteLine("{0}", item);
            }
        }
    }
}
