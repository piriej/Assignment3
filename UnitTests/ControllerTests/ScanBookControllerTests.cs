using FluentAssertions;
using Library.Features.ScanBook;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Messages;
using NSubstitute;
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
        public void ScanningABook_ThatCanBeBorrowed_ShouldBeAddedToPendingLoans(IEventAggregator eventAggregator)
        {
            var controller = new ScanBookController(eventAggregator);

            
        }
    }
}
