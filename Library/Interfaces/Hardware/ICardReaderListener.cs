using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Features.CardReader;

namespace Library.Interfaces.Hardware
{
    public interface ICardReaderListener
    {
        void cardSwiped(int cardData);
    }

    public interface ICardReaderListener2
    {
        event EventHandler<CardReaderModel> NotifyCardSwiped;
    }
}
