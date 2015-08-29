using Library.Interfaces.Controls.Borrow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Library.Interfaces.Controllers.Borrow;

namespace Library.Controls.Borrow
{
    /// <summary>
    /// Interaction logic for ScanBookControl.xaml
    /// </summary>
    public partial class ScanBookControl : ABorrowControl
    {
        private IBorrowListener _listener;

        public ScanBookControl(IBorrowListener listener)
        {
            _listener = listener;
            InitializeComponent();
        }

        public void displayAtLoanLimitMessage()
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayConfirmingLoan(string loanDetails)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayErrorMessage(string errorMesg)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayExistingLoan(string loanDetails)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayMemberDetails(int memberID, string memberName, string memberPhone)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayOutstandingFineMessage(float amountOwing)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayOverDueMessage()
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayOverFineLimitMessage(float amountOwing)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayPendingLoan(string loanDetails)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void displayScannedBookDetails(string bookDetails)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        public void setState(EBorrowState state)
        {
            throw new ApplicationException("Illegal operation in current state");
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _listener.cancelled();
        }

        private void completeButton_Click(object sender, RoutedEventArgs e)
        {
            _listener.scansCompleted();
        }
    }
}
