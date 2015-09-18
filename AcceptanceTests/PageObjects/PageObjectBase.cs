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

        protected bool IsElementEnabled(string elementName)
        {
            var automationElement = Page.FindById(elementName);

            if (automationElement == null)
                throw new ElementNotAvailableException("Cannot find element " + elementName);

            return (bool) automationElement.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty);
        }

        protected void SetElementText(string elementName, string text)
        {
            var automationElement = Page.FindById(elementName);
            if (automationElement == null)
                throw new ElementNotAvailableException("Cannot find element " + elementName);


            var textBoxPattern = automationElement.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
            textBoxPattern?.SetValue(text);
        }

        protected void ClickElement(string elementName)
        {
            var automationElement = Page.FindById(elementName);

            if (automationElement == null)
                throw new ElementNotAvailableException("Cannot find element " + elementName);

            var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            invokePattern.Invoke();
        }
    }
}