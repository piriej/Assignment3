using AcceptanceTests.PageObjects.Infrastructure;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects.Pages
{
    public class PageObject : PageObjectBase
    {
        public PageObject() : base(ViewNames.MainWindowView, new BorrowingRegion())
        {
        }
    }
}
