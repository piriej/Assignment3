namespace Library.Features.Borrowing
{
    public interface IBorrowingViewModel
    {
        bool Active { get; set; }
        string ErrorMessage { get; set; }
    }
}