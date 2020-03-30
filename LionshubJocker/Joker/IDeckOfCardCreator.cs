using System;
using System.Collections.Generic;
using System.Text;

namespace LionshubJocker.Joker
{
    public interface IDeckOfCardCreator
    {
        IList<Card> CreateDeckOfCards();
    }
}
