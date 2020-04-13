using System;
using System.Collections.Generic;
using System.Text;

namespace LionshubJoker.Joker
{
    public class Game
    {
        private readonly GameType _gameType;
        private readonly List<Gamer> _gamers;
        public GameType GameType { get { return _gameType; } }
        public List<Gamer> Gamers { get; set; }
        public Game(GameType gameType, List<Gamer> gamers)
        {
            _gameType = gameType;
            _gamers = gamers;
        }
        public List<RoundsAndGamers> LoadGame()
        {
            List<RoundsAndGamers> rounds = new List<RoundsAndGamers>();
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
        private List<RoundsAndGamers> LoadStandardGame()
        {
            List<RoundsAndGamers> rounds = new List<RoundsAndGamers>();
            var cardsOnRound = Enum.GetValues(typeof(CardsOnRound));
            foreach (CardsOnRound round in cardsOnRound)
            {
                if (round != CardsOnRound.Nine)
                {
                    switch (round)
                    {
                        case CardsOnRound.One:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.One, CurrentGamer = _gamers[0] });
                            break;
                        case CardsOnRound.Two:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Two, CurrentGamer = _gamers[1] });
                            break;
                        case CardsOnRound.Three:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Three, CurrentGamer = _gamers[2] });
                            break;
                        case CardsOnRound.Four:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Four, CurrentGamer = _gamers[3] });
                            break;
                        case CardsOnRound.Five:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Five, CurrentGamer = _gamers[0] });
                            break;
                        case CardsOnRound.Six:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Six, CurrentGamer = _gamers[1] });
                            break;
                        case CardsOnRound.Seven:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Seven, CurrentGamer = _gamers[2] });
                            break;
                        case CardsOnRound.Eight:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Eight, CurrentGamer = _gamers[3] });
                            break;
                    }
                }
            }
            AddNinesToRound(ref rounds);

            Array.Reverse(cardsOnRound);
            foreach (CardsOnRound round in cardsOnRound)
            {
                if (round != CardsOnRound.Nine)
                {
                    switch (round)
                    {
                        case CardsOnRound.One:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.One, CurrentGamer = _gamers[3] });
                            break;
                        case CardsOnRound.Two:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Two, CurrentGamer = _gamers[2] });
                            break;
                        case CardsOnRound.Three:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Three, CurrentGamer = _gamers[1] });
                            break;
                        case CardsOnRound.Four:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Four, CurrentGamer = _gamers[0] });
                            break;
                        case CardsOnRound.Five:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Five, CurrentGamer = _gamers[3] });
                            break;
                        case CardsOnRound.Six:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Six, CurrentGamer = _gamers[2] });
                            break;
                        case CardsOnRound.Seven:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Seven, CurrentGamer = _gamers[1] });
                            break;
                        case CardsOnRound.Eight:
                            rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Eight, CurrentGamer = _gamers[0] });
                            break;
                        default:
                            break;
                    }
                }
            }
            AddNinesToRound(ref rounds);

            return rounds;
        }
        private List<RoundsAndGamers> LoadNinesGame()
        {
            List<RoundsAndGamers> rounds = new List<RoundsAndGamers>();
            for (int i = 0; i < _gamers.Count; i++)
            {
                AddNinesToRound(ref rounds);
            }
            return rounds;
        }
        private List<RoundsAndGamers> LoadOnesGame()
        {
            List<RoundsAndGamers> rounds = new List<RoundsAndGamers>();
            for (int i = 0; i < 4; i++)
            {
                AddOnesToRound(ref rounds);
            }
            return rounds;
        }
        private void AddNinesToRound(ref List<RoundsAndGamers> rounds)
        {
            for (int i = 0; i < _gamers.Count; i++)
            {
                rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.Nine, CurrentGamer = _gamers[i] });
            }
        }
        private void AddOnesToRound(ref List<RoundsAndGamers> rounds)
        {
            for (int i = 0; i < 4; i++)
            {
                rounds.Add(new RoundsAndGamers { Hand = CardsOnRound.One, CurrentGamer = _gamers[i] });
            }
        }
    }
}
