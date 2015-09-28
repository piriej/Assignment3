namespace Library.Features.ScanBook
{
    public interface IScanBookViewModel
    {
        int BorrowerId { get; set; }
        string Name { get; set; }
        string Contact { get; set; }
        string ExistingLoan { get; set; }
        string ErrorMessage { get; set; }
        string PendingLoans { get; set; }
        string CurrentBook { get; set; }
    }
}