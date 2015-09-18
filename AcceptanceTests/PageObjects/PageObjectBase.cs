using System.Windows.Automation;
using AcceptanceTests.Help;
using AcceptanceTests.Steps.Arrange;

namespace AcceptanceTests.PageObjects
{
    public class PageObjectBase
    {
        readonly AutomationElement _page;
        string _pageName;

        protected AutomationElement Page => _page;

        protected string PageName => _pageName;

        public PageObjectBase(string pageName)
        {
            _pageName = pageName;
            _page = AutomationElement.RootElement.FindByName(pageName);
            if (_page == null)
                throw new ElementNotAvailableException("Cannot find page" + pageName);
        }

    }
}