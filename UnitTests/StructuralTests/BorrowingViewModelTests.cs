using System;
using Library.ApplicationInfratructure;
using Library.Controllers.Borrow;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using NSubstitute;
using Prism.Regions;
using Xunit.Extensions;

namespace UnitTests
{
    public class BorrowingViewModelTests
    {
        [Theory, AutoNSubstituteData]
        public void BorrowingViewModel_BorrowCommand_RequestsNavigationForTheContentRegionToAURI(
            IRegionManager regionManager
            , string uri
            , ICardReader2 cardReader
            , IBorrowController borrowController)
        {
            // Arrange
            var viewModel = new BorrowingViewModel(regionManager, cardReader, borrowController);

            regionManager.When(x => x.RequestNavigate(Arg.Any<string>(), Arg.Any<string>()))
                .Do(x => Console.WriteLine(@"RequestNavigate Called"));

            // Act
            viewModel.BorrowCommand.Execute(uri);

            // Assert.
            regionManager.Received().RequestNavigate(RegionNames.ContentRegion, uri);
        }

        
    }
}
