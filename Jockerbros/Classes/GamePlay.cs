using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public class GamePlay
    {
        public readonly IList<Gamer> _gamers;
        private readonly IList<Card> _deckOfCards;
        public GamePlay(IList<Gamer> gamer, IList<Card> DeckOfCards)
        {
            _gamers = gamer;
            _deckOfCards = DeckOfCards;
        }

        public void GameRound(int roundOfGame)
        {
            EmptyHands();
            HandOutCardsToEachPlayer(roundOfGame);
        }

        private void HandOutCardsToEachPlayer(int roundOfGame)
        {
            foreach (Gamer item in _gamers)
            {
                for (int i = 0; i < roundOfGame; i++)
                {
                    item.CardsOnHand.Add(_deckOfCards[0]);
                    _deckOfCards.RemoveAt(0);
                }
            }
        }

        private void EmptyHands()
        {
            foreach (Gamer item in _gamers)
            {
                item.CardsOnHand.Clear();
            }
        }


    }
}
