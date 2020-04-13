using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LionshubJoker.Joker
{
    public class PlayGame
    {

        private readonly IList<Gamer> _gamers;
        private readonly IList<Card> _deckOfCards;
        private CardsOnRound _cardsOnRound;
        private Card _trumpCard;
        public string Status { get { return CurrentGamer == null ? "Please start a round" : $"Waiting for player {CurrentGamer._name}"; } }
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
        public PlayGame(IList<Gamer> gamer, IList<Card> deckOfCards)
        {
            //_table = table;
            _gamers = gamer;
            _deckOfCards = deckOfCards;

        }
        public void StartRound(CardsOnRound cardsOnHand)
        {
            _cardsOnRound = cardsOnHand;
            EmptyHands();
            HandOutCardsToEachPlayer();
            GetTrumpCardOfRound();
        }

        private void GetTrumpCardOfRound()
        {
            if (_cardsOnRound != CardsOnRound.Nine)
            {
                _trumpCard = _deckOfCards[0];
                AllCardIsTrump(_trumpCard.ColorOfCard);
            }
        }

        private void AllCardIsTrump(CardColor color)
        {
            foreach (Gamer item in _gamers)
            {
                foreach (Card card in item.CardsOnHand)
                {
                    if (card.ColorOfCard == color)
                    {
                        card.IsTrump = true;
                    }
                }
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
                item.CardsOnHand = item.CardsOnHand.OrderByDescending(p => p.Strength).ThenBy(p => p.ColorOfCard).ToList();
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
