using System;
using FluentAssertions;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Xunit.Extensions;

namespace IntegrationTests.LowLeveTests
{
    /// <summary>
    /// These tests utilise an Automocking, Autosubstituting, IOC Container i.e. If they are ddefinded in the container
    ///  It will use real entities if they exist, resolving their dependant graphs. Otherwise,
    /// if they don't it will use a substitute - the object returned by the container contains mocked data. This is useful as
    /// we can write end to end tests, and worry about providing the implementation later. This prevents the need for stubbing.
    /// </summary>

    public class LoanLowLevelTests
    {
        DateTime _today = DateTime.Today;
        DateTime _dueDate = DateTime.Today.AddDays(5);
        DateTime _overdueDate = DateTime.Today.AddDays(10);
        DateTime _notYetDueDate = DateTime.Today.AddDays(-10);

        [Theory, ContainerData]
        public void HappyCase_UserBorrowsAndReturnsBookBeforeDue(ILoanDAO loanDao, ILoanHelper loanHelper, IMember borrower, IBook book)
        {
            // Create and commit to the loan.
            var loan = loanDao.CreateLoan(borrower, book, _today, _dueDate);
            loan.Commit(200);

            // Check if the book is overdue.
            loan.CheckOverDue(_notYetDueDate);

            // Return the book
            loan.Complete();

            loan.State.Should().Be(LoanState.COMPLETE);

            //public void SwipeCard_WithValidBorrowerId_ReturnsLoadInformation(IScanBookController scanBookController, IBorrowController borrowController, ICardReaderViewModel cardReaderViewModel, ICardReaderController cardReaderController, IScanBookViewModel scanBookViewModel)
            //{
            //    AutoMapperConfig.RegisterMaps();

            //    // The borrow controller has been clicked.
            //    borrowController.WaitForCardSwipe();

            //    // The card is swiped with a known user.
            //    cardReaderViewModel.BorrowerId = "0001";

            //    // When the card is swiped.
            //    cardReaderController.CardSwiped(cardReaderViewModel.BorrowerId);

            //    //TODO whats it bound to
            //    scanBookViewModel.ExistingLoan.Should().NotBeNullOrEmpty();
            //}
        }
    }

    //public class bookFactory
    //{
    //    public static BookFactory getInstance()
    //    {
    //        string author
    //        var book = new Book();

    //    }
    //}
}
