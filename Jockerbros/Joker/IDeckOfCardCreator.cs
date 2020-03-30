using System;
using System.Collections.Generic;
using System.Text;

namespace Jockerbros.Classes
{
    public interface IDeckOfCardCreator
    {
        IList<Card> CreateDeckOfCards();
    }
}
