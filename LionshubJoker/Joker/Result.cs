﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LionshubJoker.Joker
{
    public class Result
    {
        private int _endResult = 0;
        public Score ShouldScore { get; set; } = 0;
        public Score IsScore { get; set; } = 0;
        public Score MaxScore { get; set; } = 0;
        public int EndResult { get => GetResult(); private set => _endResult = value; }

        private int GetResult()
        {
            if (ShouldScore != Score.Pass)
            {
                if (IsScore == Score.Pass)
                {
                    _endResult = 0;
                }
                else if (ShouldScore == IsScore)
                {
                    if (ShouldScore == MaxScore)
                    {
                        _endResult = Convert.ToInt32(MaxScore) * 100;
                    }
                    else
                    {
                        _endResult = Enum.GetValues(typeof(Score)).Cast<Score>().ToList().IndexOf(ShouldScore)  * 50;
                    }
                }
                else
                {
                    _endResult = Enum.GetValues(typeof(Score)).Cast<Score>().ToList().IndexOf(ShouldScore)  * 10;
                }
            }
            else
            {
                _endResult = Enum.GetValues(typeof(Score)).Cast<Score>().ToList().IndexOf(ShouldScore)  * 10;
            }
            return _endResult;
        }
    }
}
