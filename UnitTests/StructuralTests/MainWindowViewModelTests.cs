using System;
using FluentAssertions;
using Library.Hardware;
using Library.ViewModels;
using Library.Views.MainWindow;
using NSubstitute;
using Ploeh.AutoFixture.Xunit;
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
            var viewModel = new MainWindowViewModel()
            {
                CloseWindowDelegate = () => applicationClosed = true
            };

            // Act
            viewModel.CloseWindowCommand.Execute(null);

            // Assert
            applicationClosed.Should().BeTrue();
        }

        //[Theory, AutoNSubstituteData]
        //public void MainWindowViewModel_WhenCConstructed_ShowsTheCardReader(CardReader reader, Scanner scanner,
        //    Printer printer)
        //{
        //    // Arrange
        //    var show = false;
        //    reader.When(x => x.Show())
        //        .Do(x => show = true);

        //    // Act
        //    var viewModel = new MainWindowViewModel(reader, scanner, printer);

        //    // Assert
        //    show.Should().BeTrue();
        //}
    }
}
