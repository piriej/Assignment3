using System.Collections.Generic;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Entities;

namespace Library.Features.Borrowing
{
    public class BorrowingModel : IBorrowingModel
    {
        public bool HasOverDueLoans { get; set; }
        public bool HasReachedLoanLimit { get; set; }
        public bool HasFinesPayable { get; set; }
        public bool HasReachedFineLimit { get; set; }
        public float FineAmount { get; set; }
        public List<ILoan> Loans { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public string EmailAddress { get; set; }
        public int ID { get; set; }
        public MemberState State { get; set; }
        public EBorrowState BorrowingState
        {
            get { return EborrowStateManager.CurrentState; }
        }
    }
}
