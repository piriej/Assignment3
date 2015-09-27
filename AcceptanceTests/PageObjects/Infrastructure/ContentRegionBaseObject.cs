using System.Windows.Automation;
using AcceptanceTests.Help;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects
{
    public class ContentRegionBaseObject : AutomationElementBase
    {
        string _regionName;

        public ContentRegionBaseObject(string regionName)
        {
            _regionName = regionName;
            AutomationElement.RootElement.FindByName(ViewNames.MainWindowView).DisplayChildren();
            _page = AutomationElement.RootElement.FindByClass(regionName);
            if (_page == null)
                throw new ElementNotAvailableException("Cannot find region:" + regionName);
        }
    }
}