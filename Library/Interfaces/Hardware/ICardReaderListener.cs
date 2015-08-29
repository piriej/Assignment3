using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Hardware
{
    public interface ICardReaderListener
    {
        void cardSwiped(int cardData);
    }
}
