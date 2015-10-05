using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Entities;

namespace Library.Features.Borrowing
{
    public static class EborrowStateManager
    {
        public static EBorrowState CurrentState { get; private set; } = EBorrowState.CREATED;

        public static EBorrowState ChangeState(this EBorrowState state, IMember member)
        {
            // From state INITIALIZED ==> BORROWING_RESTRICTED || SCANNING_BOOKS

            if (CurrentState != EBorrowState.INITIALIZED) return state;

            if (member.HasOverDueLoans
                || member.HasReachedLoanLimit
                || member.HasReachedLoanLimit
                || member.HasFinesPayable)
                CurrentState = EBorrowState.BORROWING_RESTRICTED;

            else
                CurrentState = EBorrowState.SCANNING_BOOKS;

            return CurrentState;
            //    public enum EBorrowState { CREATED, INITIALIZED, SCANNING_BOOKS, CONFIRMING_LOANS, COMPLETED, BORROWING_RESTRICTED, CANCELLED }
        }

        public static EBorrowState Reset(this EBorrowState state) {
            CurrentState = EBorrowState.CREATED;
            return CurrentState;

        }

        public static EBorrowState ChangeState(this EBorrowState state)
        {
            if (CurrentState == EBorrowState.CREATED)
                CurrentState = EBorrowState.INITIALIZED;

            return CurrentState;
        }

        public static void ChangeState(this EBorrowState state, EBorrowState confirmingLoans)
        {
            if (CurrentState == EBorrowState.SCANNING_BOOKS)
            {
                CurrentState = EBorrowState.CONFIRMING_LOANS;
            }
           
        }
    }
}