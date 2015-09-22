using System;
using Library.ApplicationInfratructure;
using Library.Features.Borrowing;
using Library.Interfaces.Hardware;
using NSubstitute;
using Prism.Regions;
using Xunit.Extensions;
using ICardReader = Library.Features.CardReader.ICardReader;

namespace UnitTests
{
    public class BorrowingViewModelTests
    {
        [Theory, AutoNSubstituteData]
        public void BorrowingViewModel_BorrowCommand_RequestsNavigationForTheContentRegionToAURI(
            IRegionManager regionManager
            , string uri
            , ICardReader cardReader)
        {
            // Arrange
            var viewModel = new BorrowingViewModel(regionManager, cardReader);

            regionManager.When(x => x.RequestNavigate(Arg.Any<string>(), Arg.Any<string>()))
                .Do(x => Console.WriteLine(@"RequestNavigate Called"));

            // Act
            viewModel.BorrowCommand.Execute(uri);

            // Assert.
            regionManager.Received().RequestNavigate(RegionNames.ContentRegion, uri);
        }

        
    }
}
