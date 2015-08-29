using Library.Interfaces.Controls.Borrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Library.Interfaces.Controllers.Borrow;

namespace Library.Controls.Borrow
{
    public class ABorrowControl : UserControl, IBorrowUI
    {
        public void displayAtLoanLimitMessage()
        {
            throw new NotImplementedException();
        }

        public void displayConfirmingLoan(string loanDetails)
        {
            throw new NotImplementedException();
        }

        public void displayErrorMessage(string errorMesg)
        {
            throw new NotImplementedException();
        }

        public void displayExistingLoan(string loanDetails)
        {
            throw new NotImplementedException();
        }

        public void displayMemberDetails(int memberID, string memberName, string memberPhone)
        {
            throw new NotImplementedException();
        }

        public void displayOutstandingFineMessage(float amountOwing)
        {
            throw new NotImplementedException();
        }

        public void displayOverDueMessage()
        {
            throw new NotImplementedException();
        }

        public void displayOverFineLimitMessage(float amountOwing)
        {
            throw new NotImplementedException();
        }

        public void displayPendingLoan(string loanDetails)
        {
            throw new NotImplementedException();
        }

        public void displayScannedBookDetails(string bookDetails)
        {
            throw new NotImplementedException();
        }

        public void setState(EBorrowState state)
        {
            throw new NotImplementedException();
        }
    }
}
