using System;
using System.Collections.Generic;
using System.Text;

namespace LionshubJoker.Joker
{
    public class Game
    {
        private readonly GameType _gameType;
        public GameType GameType { get { return _gameType; } }
        public Game(GameType gameType)
        {
            _gameType = gameType;
        }
        public List<CardsOnRound> LoadGame()
        {
            List<CardsOnRound> rounds = new List<CardsOnRound>();
            switch (_gameType)
            {
                case GameType.Standard:
                    rounds = LoadStandardGame();
                    break;
                case GameType.Nines:
                    rounds = LoadNinesGame();
                    break;
                case GameType.Ones:
                    rounds = LoadOnesGame();
                    break;
                default:
                    break;
            }
            return rounds;
        }
        private List<CardsOnRound> LoadStandardGame()
        {
            List<CardsOnRound> rounds = new List<CardsOnRound>();
            var cardsOnRound = Enum.GetValues(typeof(CardsOnRound));
            //Array.Sort(cardsOnRound);
            foreach (CardsOnRound round in cardsOnRound)
            {
                if (round != CardsOnRound.Nine)
                {
                    rounds.Add(round);
                }
            }
            AddNinesToRound(ref rounds);

            Array.Reverse(cardsOnRound);
            foreach (CardsOnRound round in cardsOnRound)
            {
                if (round != CardsOnRound.Nine)
                {
                    rounds.Add(round);
                }
            }
            AddNinesToRound(ref rounds);

            return rounds;
        }
        private List<CardsOnRound> LoadNinesGame()
        {
            List<CardsOnRound> rounds = new List<CardsOnRound>();
            for (int i = 0; i < 4; i++)
            {
                AddNinesToRound(ref rounds);
            }
            return rounds;
        }
        private List<CardsOnRound> LoadOnesGame()
        {
            List<CardsOnRound> rounds = new List<CardsOnRound>();
            for (int i = 0; i < 4; i++)
            {
                AddOnesToRound(ref rounds);
            }
            return rounds;
        }
        private void AddNinesToRound(ref List<CardsOnRound> rounds)
        {
            for (int i = 0; i < 4; i++)
            {
                rounds.Add(CardsOnRound.Nine);
            }
        }
        private void AddOnesToRound(ref List<CardsOnRound> rounds)
        {
            for (int i = 0; i < 4; i++)
            {
                rounds.Add(CardsOnRound.One);
            }
        }
    }
}
