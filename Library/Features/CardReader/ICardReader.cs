using Library.Interfaces.Hardware;
using Prism.Events;

namespace Library.Features.CardReader
{
    public interface ICardReader2
    {
        bool Enabled { get; set; }
        string BorrowerId { get; set; }
    }
}
