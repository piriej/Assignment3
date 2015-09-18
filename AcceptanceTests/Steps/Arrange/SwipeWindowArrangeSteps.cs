using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTests.PageObjects;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Arrange
{
    [Binding]
    public sealed class SwipeWindowArrangeSteps
    {
        [Given(@"The loan self service station prompts the user to swipe their card")]
        public void GivenTheLoanSelfServiceStationPromptsTheUserToSwipeTheirCard()
        {
            // Identify the window components.
            //Thread.Sleep(2000);
            //var app = AutomationElement.RootElement.FindByName("MainWindow");
            //var automationElement = app.FindById("borrowButton");

            //automationElement.Should().NotBeNull();

            //automationElement.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty).Should().Be(true);

            //var patterns = automationElement.GetSupportedPatterns();
            //var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            //invokePattern.Invoke();
            var pageObject = new MainWindowPageObject();

            pageObject.IsBorrowButtonEnabled().Should().BeTrue("The borrow button is not enabled.");

            pageObject.ClickBorrowButton();
        }

    }
}
