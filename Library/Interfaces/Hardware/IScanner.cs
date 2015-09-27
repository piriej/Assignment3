namespace Library.Interfaces.Hardware
{
    public interface IScanner
    {

        IScannerListener Listener { set; get; }

        bool Enabled { set; get; }
    }
}
