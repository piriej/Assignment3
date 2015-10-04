using System.Collections.Generic;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Entities;

namespace Library.Features.Borrowing
{
    public interface IBorrowingModel
    {
        bool HasOverDueLoans { get; set; }
        bool HasReachedLoanLimit { get; set; }
        bool HasFinesPayable { get; set; }
        bool HasReachedFineLimit { get; set; }
        float FineAmount { get; set; }
        List<ILoan> Loans { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string ContactPhone { get; set; }
        string EmailAddress { get; set; }
        int ID { get; set; }
        MemberState State { get; set; }
        EBorrowState BorrowingState { get; }
    }
}