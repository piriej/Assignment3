namespace Library.Interfaces.Hardware
{
    public interface ICardReader
    {
        ICardReaderListener Listener { get; set; }

        bool Enabled { get; set; }
    }
}
