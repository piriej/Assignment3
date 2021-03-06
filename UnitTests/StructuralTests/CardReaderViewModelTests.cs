﻿using FluentAssertions;
using Library.ApplicationInfratructure;
using Library.Features.CardReader;
using NSubstitute;
using Prism.Events;
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

        //[Theory, AutoNSubstituteData]
        //public void CardSwiped_WithAnEventListener_TriggersTheEvent(IEventAggregator eventAggregator, ICardReaderController cardReaderController ,string borrowerId, string uri)
        //{
        //    var swiped = false;

        //    // Arrange.
        //    var viewModel = new CardReaderViewModel(eventAggregator, cardReaderController) {BorrowerId = borrowerId};
        //    viewModel.NotifyCardSwiped += (src,reader) => swiped = true;

        //    // Act
        //    viewModel.CardSwipedCmd.Execute(uri);

        //    // Assert
        //    swiped.Should().BeTrue();
        //}

        //[Theory, AutoNSubstituteData]
        //public void CardSwiped_GivenAUri_NavigatesToTheUri(IEventAggregator eventAggregator, ICardReaderController cardReaderController ,IRegionManager regionManager, string borrowerId, string uri)
        //{
        //    // Arrange.
        //    var viewModel = new CardReaderViewModel(eventAggregator, cardReaderController) { BorrowerId = borrowerId, RegionManager = regionManager};
        //    regionManager.When(rm => rm.RequestNavigate(Arg.Any<string>(), Arg.Any<string>()));

        //    // Act
        //    viewModel.CardSwipedCmd.Execute(uri);

        //    // Assert
        //    regionManager.Received().RequestNavigate(RegionNames.ContentRegion, uri);
        //}

        //[Theory, AutoNSubstituteData]
        //public void CardSwiped_WithAnEventListener_ReturnsTheBorrowerIdInTheModel(IEventAggregator eventAggregator, ICardReaderController cardReaderController, IRegionManager regionManager, string borrowerId, string uri)
        //{
        //    var sut = "";

        //    // Arrange.
        //    var viewModel = new CardReaderViewModel(eventAggregator, cardReaderController) { BorrowerId = borrowerId, RegionManager = regionManager};
        //    viewModel.NotifyCardSwiped += (src, reader) => sut = reader.BorrowerId;

        //    // Act
        //    viewModel.CardSwipedCmd.Execute(uri);

        //    // Assert
        //    sut.Should().Be(borrowerId);
        //}
    }
}