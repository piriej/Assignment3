using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Library.ApplicationInfratructure;
using Library.Daos;
using Library.Entities;
using Library.Features.Borrowing;
using Library.Features.ScanBook;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Messages;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;
using Prism.Events;
using Xunit.Extensions;

namespace UnitTests.ControllerTests
{
    public class ScanBookControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void ScanningABook_ThatDoesntExist_ResultsInErrorMessage(IEventAggregator eventAggregator, [Frozen] IBookDAO bookDao, ScanBookModel scanBookModel, IScanBookViewModel viewModel)
        {
            // Arrange - A barcode that doesnt exist.
            //scanBookModel.Barcode = barCode;
            bookDao.GetBookByID(Arg.Any<int>()).Returns((IBook)null);
            eventAggregator.GetEvent<BorrowingStateEvent>().Returns(new BorrowingStateEvent());

            var controller = new ScanBookController(eventAggregator)
            {
                BookDao = bookDao,
                ViewModel = viewModel
            };

            // Act -Scan a book.
            controller.Scanning(scanBookModel);

            // Assert - Results in an error message.
            viewModel.ErrorMessage.Should().Be($"Book {scanBookModel.Barcode} not found");

        }

        [Theory, AutoNSubstituteData]
        public void ScanningABook_ThatIsOnLoan_ResultsInNotAvailableError(IEventAggregator eventAggregator
            , IBookDAO bookDao
            , [Frozen] IBook book
            , ILoanDAO loanDao
            , ScanBookModel scanBookModel
            , IScanBookViewModel viewModel)
        {
            // Arrange - Setup book on loan.
            eventAggregator.GetEvent<BorrowingStateEvent>().Returns(new BorrowingStateEvent());

            bookDao.GetBookByID(scanBookModel.Barcode).Returns(book);
            book.State.Returns(BookState.ON_LOAN);

            loanDao.LoanList.Returns(new List<ILoan>());

            var controller = new ScanBookController(eventAggregator)
            {
                BookDao = bookDao,
                LoanDao = loanDao,
                ViewModel = viewModel
            };

            // Act -Scan a book.
            controller.Scanning(scanBookModel);

            // Assert - 
            viewModel.ErrorMessage.Should().Be($"Book {book.ID} is not available: {book.State}");

        }


        [Theory, AutoNSubstituteData]
        public void ScanningABook_ThatsAlreadyScanned_ResultsInAlreadyScannedError(IEventAggregator eventAggregator
            , IBookDAO bookDao
            , [Frozen] IBook book
            , ILoanDAO loanDao
            , IMemberDAO memberDao
            , ScanBookModel scanBookModel
            , IBorrowingModel borrowingModel
            , ScanBookViewModel viewModel
            , List<ILoan> loanList
            , [Frozen] Member borrower)
        {
            // Arrange - Setup a book that has already been scanned.

            // Mock the events.
            eventAggregator.GetEvent<BorrowingStateEvent>().Returns(new BorrowingStateEvent());
            eventAggregator.GetEvent<ScanningRecievedEvent>().Returns(new ScanningRecievedEvent());
            eventAggregator.GetEvent<ScanningEvent>().Returns(new ScanningEvent());

            // We are currently in the scanning book state.
            borrowingModel.BorrowingState.Returns(EBorrowState.SCANNING_BOOKS);

            // Establish the current user identified on borrowing model, and returned from member dao.
            borrowingModel.ID = borrower.ID;
            memberDao.GetMemberByID(borrower.ID).Returns(borrower);

            // Configure mapping.
            AutoMapperConfig.RegisterMaps();


            // One of the loans in the loan list has a status of pending for the current borrower.
            var existingLoan = loanList.First();
            existingLoan.State.Returns(LoanState.PENDING);
            existingLoan.Borrower.Returns(borrower);

            // The loan Dao returns this loan list when requested
            loanDao.LoanList.Returns(loanList);

            // The book to be scanned is the same book as already scanned.
            bookDao.GetBookByID(scanBookModel.Barcode).Returns(existingLoan.Book);

            // The book is available for loan.
            existingLoan.Book.State.Returns(BookState.AVAILABLE);

            var controller = new ScanBookController(eventAggregator)
            {
                BookDao = bookDao,
                LoanDao = loanDao,
                MemberDao = memberDao,
                ViewModel = viewModel
            };

            // Prepare Scanner
            controller.ScanBook(borrowingModel);


            // Act -Scan a book.
            controller.Scanning(scanBookModel);

            // Assert - Already scanned error message appears.
            viewModel.ErrorMessage.Should().Be($"Book {existingLoan.Book.ID} already scanned ");
        }


        [Theory, AutoNSubstituteData]
        public void ScanningABook_ThatCanBeBorrowed_ShouldBeAddedToPendingLoans(
            IEventAggregator eventAggregator
            , [Frozen] IBookDAO bookDao
            , ILoanDAO loanDao
            , IMemberDAO memberDao
            , ScanBookModel scanBookModel
            , ScanBookViewModel viewModel
            , Book book
            , IBorrowingModel borrowingModel
            , Member borrower
            , List<ILoan> loanList )
        {
            var fixture = new Fixture();
            var loan = fixture.Build<Loan>()
                .FromFactory(() => new Loan(book, borrower, DateTime.Today, DateTime.Today.AddDays(10)))
                .Create();


            eventAggregator.GetEvent<ScanningRecievedEvent>().Returns(new ScanningRecievedEvent());
            eventAggregator.GetEvent<BorrowingStateEvent>().Returns(new BorrowingStateEvent());
            eventAggregator.GetEvent<ScanningEvent>().Returns(new ScanningEvent());

            // Arrange - Setup book on loan.
            bookDao.GetBookByID(scanBookModel.Barcode).Returns(book);

            // We are currently in the scanning book state.
            borrowingModel.BorrowingState.Returns(EBorrowState.SCANNING_BOOKS);

            // Establish the current user identified on borrowing model, and returned from member dao.
            borrowingModel.ID = borrower.ID;
            memberDao.GetMemberByID(borrower.ID).Returns(borrower);

            // Configure mapping.
            AutoMapperConfig.RegisterMaps();

            loanDao.LoanList.Returns(loanList);

            var controller = new ScanBookController(eventAggregator)
            {
                BookDao = bookDao,
                LoanDao = loanDao,
                MemberDao = memberDao,
                ViewModel = viewModel
            };

            loanDao.CreateLoan(borrower, book, Arg.Any<DateTime>(), Arg.Any<DateTime>()).Returns(loan);

            controller.ScanBook(borrowingModel);

            // Act -Scan a book.
            controller.Scanning(scanBookModel);

            // Assert - Results in an error message.
            viewModel.PendingLoans.Should().NotBeNullOrEmpty();
        }
    }
}
