namespace Library.Features.CardReader
{
    public interface ICardReaderViewModel
    {
        bool Enabled { get; set; }
        string BorrowerId { get; set; }
    }
}