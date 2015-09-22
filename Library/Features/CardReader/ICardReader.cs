using Library.Interfaces.Hardware;

namespace Library.Features.CardReader
{
    public interface ICardReader
    {
        bool Enabled { get; set; }
        string BorrowerId { get; set; }
    }
}
