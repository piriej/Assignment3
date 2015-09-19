using FluentAssertions;
using Library.ViewModels;
using Xunit.Extensions;

namespace UnitTests.StructuralTests
{
    public class MainWindowViewModelTests
    {
        [Theory, AutoNSubstituteData]
        public void CloseWindowCommand_WhenCalled_ClosesTheApplication()
        {
            // Arrange
            var applicationClosed = false;
            var viewModel = new MainWindowViewModel {CloseWindowDelegate = () => applicationClosed = true};

            // Act
            viewModel.CloseWindowCommand.Execute(null);

            // Assert
            applicationClosed.Should().BeTrue();
        }
    }
}
