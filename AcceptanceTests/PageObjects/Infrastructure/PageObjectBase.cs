using System;
using System.Windows.Automation;
using AcceptanceTests.Help;

namespace AcceptanceTests.PageObjects.Infrastructure
{
    public class PageObjectBase : AutomationElementBase
    {
        public PageObjectBase(string pageName)
        {
            _pageName = pageName;
            _page = AutomationElement.RootElement.FindByName(pageName);
            if (_page == null)
                throw new ElementNotAvailableException("Cannot find page: " + pageName);
        }

        public PageObjectBase(string pageName, ContentRegionBaseObject contentRegionContainer) : this(pageName)
        {
            ContentRegionContainer = contentRegionContainer;
        }

        ContentRegionBaseObject _contentRegionContainer;

        ContentRegionBaseObject ContentRegionContainer
        {
            get
            {
                if (_contentRegionContainer == null)
                    throw new ElementNotAvailableException("No Region Defined");
                return _contentRegionContainer;
            }
            set
            {
                _contentRegionContainer = value;
            }
        }

        public T ContentRegion<T>() where T : ContentRegionBaseObject, new()
        {
            Console.WriteLine(ContentRegionContainer.GetType());
            Console.WriteLine(typeof(T));

            if (typeof(T).Name == ContentRegionContainer.GetType().Name)
                return (T)ContentRegionContainer;

            ContentRegionContainer = new T();
            return (T)ContentRegionContainer;
        }

        public bool ContentRegionIs<T>() where T : ContentRegionBaseObject, new()
        {
            try
            {
                ContentRegionContainer = new T();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}