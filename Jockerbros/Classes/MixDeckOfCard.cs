using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class MixDeckOfCard
    {
        public MixDeckOfCard(List<Card> DeckOfCard)
        {
            if (DeckOfCard == null) { throw new ArgumentException("DeckOfCard"); }
            var random = new Random();
            for (int i = 0; i < 10000; i++)
            {
                int randomIndex = random.Next(DeckOfCard.Count);
                var card = DeckOfCard[randomIndex];
                DeckOfCard.RemoveAt(randomIndex);
                DeckOfCard.Add(card);
            }
        }
    }
}
