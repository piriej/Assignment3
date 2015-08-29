using Library.Interfaces.Controllers.Borrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Controls.Borrow
{
    public interface IBorrowUI
    {
        void displayMemberDetails(int memberID, string memberName, string memberPhone);

        void displayExistingLoan(string loanDetails);

        void displayOverDueMessage();

        void displayAtLoanLimitMessage();

        void displayOutstandingFineMessage(float amountOwing);

        void displayOverFineLimitMessage(float amountOwing);

        void displayScannedBookDetails(String bookDetails);

        void displayPendingLoan(string loanDetails);

        void displayConfirmingLoan(string loanDetails);

        void displayErrorMessage(string errorMesg);

        void setState(EBorrowState state);

    }
}
