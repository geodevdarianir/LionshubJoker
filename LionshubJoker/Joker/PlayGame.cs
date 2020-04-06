using System;
using System.Collections.Generic;
using System.Text;

namespace LionshubJoker.Joker
{
    public class PlayGame
    {

        private readonly IList<Gamer> _gamers;
        private readonly IList<Card> _deckOfCards;
        private readonly CardsOnRound _cardsOnRound;
        private Card _trumpCard;
        public string Status { get { return $"Waiting for player {CurrentGamer._name}"; } }
        public Gamer CurrentGamer { get; set; }

        public IList<Gamer> Gamers { get { return _gamers; } }
        public Card TrumpCard
        {
            get { return _trumpCard; }
            set
            {
                if (_cardsOnRound == CardsOnRound.Nine)
                    _trumpCard = value;
                else
                    GetTrumpCardOfRound();
            }
        }
        public PlayGame(IList<Gamer> gamer, IList<Card> deckOfCards, CardsOnRound cardsOnHand)
        {
            //_table = table;
            _gamers = gamer;
            _deckOfCards = deckOfCards;
            _cardsOnRound = cardsOnHand;
        }

        public void StartRound()
        {
            EmptyHands();
            HandOutCardsToEachPlayer();
            GetTrumpCardOfRound();
            CurrentGamer = _gamers[3];
        }


        private void GetTrumpCardOfRound()
        {
            if (_cardsOnRound != CardsOnRound.Nine)
            {
                _trumpCard = _deckOfCards[0];
                _trumpCard.IsTrump = true;
            }
        }
        private void HandOutCardsToEachPlayer()
        {
            foreach (Gamer item in _gamers)
            {
                for (int i = 0; i < Convert.ToInt16(_cardsOnRound); i++)
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
