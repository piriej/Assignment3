using Library;
using Library.Hardware;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace UnitTests
{
    public class MainWindowTests
    {
        [Theory, AutoData]
        public void MainWindow_ShouldInitialiseChildWindows(CardReader cardReader, Scanner scanner, Printer printer)
        {
           // MainMenuController controller = new MainMenuController();
        }
    }
}
