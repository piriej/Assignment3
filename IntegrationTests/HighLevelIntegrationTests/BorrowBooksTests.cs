using System;
using Library;
using Library.Features.CardReader;
using Xunit.Extensions;
using FluentAssertions;
using Library.ApplicationInfratructure;
using Library.Controllers.Borrow;
using Library.Features.Borrowing;
using Library.Features.ScanBook;
using Library.Features.Scanner;
using Library.Interfaces.Controllers.Borrow;

// BBUC_Op2

namespace IntegrationTests
{
    public class TestBorrowBooks
    {
        public TestBorrowBooks()
        {
            log4net.Config.XmlConfigurator.Configure();
            IOConfig.initLog4();
        }


        [Theory, ContainerData]
        public void SwipeCard_WithValidBorrowerId_SetsBorrowerDetailsForBookScanner(
            IScanBookController scanBookController, IBorrowController borrowController,
            ICardReaderViewModel cardReaderViewModel, ICardReaderController cardReaderController,
            IScanBookViewModel scanBookViewModel)
        {
            AutoMapperConfig.RegisterMaps();

            // The borrow controller has been clicked.
            borrowController.WaitForCardSwipe();

            // The card is swiped with a known user.
            cardReaderViewModel.BorrowerId = "0001";

            // When the card is swiped.
            cardReaderController.CardSwiped(cardReaderViewModel.BorrowerId);

            scanBookViewModel.BorrowerId.Should().Be(1);
            scanBookViewModel.Name.Should().Be("fName1 lName1");
            scanBookViewModel.Contact.Should().Be("0001");
        }


        [Theory, ContainerData]
        public void SwipeCard_WithValidBorrowerId_ReturnsLoanInformation(IScanBookController scanBookController,
            IBorrowController borrowController, ICardReaderViewModel cardReaderViewModel,
            ICardReaderController cardReaderController, IScanBookViewModel scanBookViewModel)
        {
            AutoMapperConfig.RegisterMaps();

            // The borrow controller has been clicked.
            borrowController.WaitForCardSwipe();

            // The card is swiped with a known user.
            cardReaderViewModel.BorrowerId = "0001";

            // When the card is swiped.
            cardReaderController.CardSwiped(cardReaderViewModel.BorrowerId);

            // Then the users details are displayed in the view.
            scanBookViewModel.Name.Should().Be("fName1 lName1");

            // And the current loan list should be displayed
            scanBookViewModel.ExistingLoan.Should().NotBeNullOrEmpty();
            scanBookViewModel.ExistingLoan.Should().Contain("callNo10");
            scanBookViewModel.ExistingLoan.Should().Contain("author3");
            scanBookViewModel.ExistingLoan.Should().Contain("title10");

            scanBookViewModel.PendingLoans.Should().NotBeNullOrEmpty();
            scanBookViewModel.PendingLoans.Should().Contain("author3");
            scanBookViewModel.PendingLoans.Should().Contain("title10");
            scanBookViewModel.PendingLoans.Should().Contain("fName1 lName1");
            scanBookViewModel.PendingLoans.Should().Contain("4/10/2015");
            scanBookViewModel.PendingLoans.Should().Contain("18/10/2015");
        }


        [Theory, ContainerData]
        public void SwipeCard_WithValidBorrowerId_CardReaderIsDisabled(
            IScanBookController scanBookController
            , IBorrowController borrowController
            , ICardReaderController cardReaderController
            , ICardReaderViewModel cardReaderViewModel
            , IScanBookViewModel scanBookViewModel)
        {
            PreConditions(borrowController, cardReaderViewModel);

            // Arrange - unrestricted user.
            const string borrowerId = "1";
            cardReaderViewModel.BorrowerId = borrowerId;

            // Act - Swipe the card.
            cardReaderController.CardSwiped(borrowerId);

            // Assert - ensure that the card reader is disabled.
            cardReaderViewModel.Enabled.Should().BeFalse();
        }

        [Theory, ContainerData]
        public void SwipeCard_WithValidBorrowerId_ScannerIsEnabled(
         IScanBookController scanBookController
         , IBorrowController borrowController
         , ICardReaderController cardReaderController
         , ICardReaderViewModel cardReaderViewModel
         , IScanBookViewModel scanBookViewModel
         , IScannerController scannerController
         , IScannerViewModel scannerViewModel)
        {
            PreConditions(borrowController, cardReaderViewModel);

            // Arrange - unrestricted user.
            const string borrowerId = "1";
            cardReaderViewModel.BorrowerId = borrowerId;

            // Act - Swipe the card.
            cardReaderController.CardSwiped(borrowerId);

            // Assert - ensure that the card reader is disabled.
            scannerViewModel.Enabled.Should().BeTrue();
        }

        [Theory, ContainerData]
        public void SwipeCard_WithValidBorrowerId_ExistingLoansDisplayed(
      IScanBookController scanBookController
      , IBorrowController borrowController
      , IBorrowingViewModel borrowingViewModel
      , ICardReaderController cardReaderController
      , ICardReaderViewModel cardReaderViewModel
      , IScanBookViewModel scanBookViewModel
      , IScannerController scannerController
      , IScannerViewModel scannerViewModel)
        {
            PreConditions(borrowController, cardReaderViewModel);

            // Arrange - User with existing loans.
            const string borrowerId = "2";
            cardReaderViewModel.BorrowerId = borrowerId;

            // Act - Swipe the card.
            cardReaderController.CardSwiped(borrowerId);
            

            // Assert - ensure that the card reader is disabled.
            scanBookViewModel.ExistingLoan.Should().Contain("author1");
            scanBookViewModel.ExistingLoan.Should().Contain("title2 ");
            scanBookViewModel.ExistingLoan.Should().Contain("fName2 lName2");
            scanBookViewModel.ExistingLoan.Should().Contain(DateTime.Today.ToShortDateString());
            scanBookViewModel.ExistingLoan.Should().Contain(DateTime.Today.AddDays(14).ToShortDateString());
        }

        private void PreConditions(IBorrowController borrowController, ICardReaderViewModel cardReaderViewModel)
        {
            EborrowStateManager.CurrentState.Reset();

            // Arrange - cardReader visible and enabled
            cardReaderViewModel.Enabled = true;

            // BorrowBookCTL added as listener to cardReader.
            borrowController.WaitForCardSwipe();

            // Arrange - memberDAO exists.
            borrowController.MemberDao.Should().NotBeNull();

            // Arrange - BorrowBookCTL state == INITIALIZED
            EborrowStateManager.CurrentState.ChangeState();
            EborrowStateManager.CurrentState.Should().Be(EBorrowState.INITIALIZED);

            // Mapping is enabled.
            AutoMapperConfig.RegisterMaps();
        }
    }
}