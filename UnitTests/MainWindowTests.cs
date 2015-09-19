using System;
using Library.ViewModels;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit;
using Prism.Regions;
using Xunit.Extensions;

namespace UnitTests
{
    public class MainWindowTests
    {
        [Theory, AutoNSubstituteData]
        public void BorrowingViewModel_BorrowCommand_RequestsNavigationForTheContentRegionToAURI(IRegionManager regionManager, string uri)
        {
            // Arrange
            var viewModel = new BorrowingViewModel(regionManager);

            regionManager.When(x => x.RequestNavigate(Arg.Any<string>(), Arg.Any<string>()))
                .Do(x => Console.WriteLine(@"RequestNavigate Called"));

            // Act
            viewModel.BorrowCommand.Execute(uri);

            // Assert.
            regionManager.Received().RequestNavigate(RegionNames.ContentRegion, uri);
        }

        
    }

    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        public AutoNSubstituteDataAttribute()
            : base(new Fixture()
            .Customize(new AutoNSubstituteCustomization()))
        {
        }
    }

    internal class AutoNSubstitutePropertyDataAttribute : CompositeDataAttribute
    {
        internal AutoNSubstitutePropertyDataAttribute(string propertyName)
            : base(
                new DataAttribute[] {
                new PropertyDataAttribute(propertyName),
                new AutoNSubstituteDataAttribute() })
        {
        }
    }
}
