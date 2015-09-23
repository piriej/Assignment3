using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Features.CardReader;
using Library.Interfaces.Controllers.Borrow;

namespace Library.Interfaces.Hardware
{
    public interface ICardReaderListener
    {
        void cardSwiped(int cardData);
    }

    public interface ICardReaderEvents
    {
        event EventHandler<CardReaderModel> NotifyCardSwiped;
    }

    public interface IBorrowEvents
    {
        event EventHandler<EBorrowState> setEnabled;
    }
}
