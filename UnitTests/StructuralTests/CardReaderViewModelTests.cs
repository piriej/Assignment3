using FluentAssertions;
using Library.ApplicationInfratructure;
using Library.Features.CardReader;
using NSubstitute;
using Prism.Regions;
using Xunit.Extensions;

namespace UnitTests.StructuralTests
{
    public class CardReaderViewModelTests
    {
        public CardReaderViewModelTests()
        {
            AutoMapperConfig.RegisterMaps();
        }

        [Theory, AutoNSubstituteData]
        public void CardSwiped_WithAnEventListener_TriggersTheEvent(IRegionManager regionManager, string borrowerId, string uri)
        {
            var swiped = false;

            // Arrange.
            var viewModel = new CardReaderViewModel(regionManager) {BorrowerId = borrowerId};
            viewModel.NotifyCardSwiped += (src,reader) => swiped = true;

            // Act
            viewModel.CardSwipedCmd.Execute(uri);

            // Assert
            swiped.Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void CardSwiped_GivenAUri_NavigatesToTheUri(IRegionManager regionManager, string borrowerId, string uri)
        {
            // Arrange.
            var viewModel = new CardReaderViewModel(regionManager) { BorrowerId = borrowerId };
            regionManager.When(rm => rm.RequestNavigate(Arg.Any<string>(), Arg.Any<string>()));

            // Act
            viewModel.CardSwipedCmd.Execute(uri);

            // Assert
            regionManager.Received().RequestNavigate(RegionNames.ContentRegion, uri);
        }

        [Theory, AutoNSubstituteData]
        public void CardSwiped_WithAnEventListener_ReturnsTheBorrowerIdInTheModel(IRegionManager regionManager, string borrowerId, string uri)
        {
            var sut = "";

            // Arrange.
            var viewModel = new CardReaderViewModel(regionManager) { BorrowerId = borrowerId };
            viewModel.NotifyCardSwiped += (src, reader) => sut = reader.BorrowerId;

            // Act
            viewModel.CardSwipedCmd.Execute(uri);

            // Assert
            sut.Should().Be(borrowerId);
        }
    }
}