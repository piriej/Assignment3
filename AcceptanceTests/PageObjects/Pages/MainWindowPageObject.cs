using AcceptanceTests.PageObjects.Infrastructure;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects.Pages
{
    public class MainWindowPageObject : PageObjectBase
    {
        public MainWindowPageObject() : base(ViewNames.MainWindowView, new BorrowingRegion())
        {
        }
    }
}
