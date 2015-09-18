using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Automation;
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

            //List<AutomationElement> messages = new List<AutomationElement>();
            //TreeWalker walker = new TreeWalker(new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.DataItem));

            //AutomationElement row = walker.GetFirstChild(AutomationElement.RootElement);
            //while (row != null)
            //{
            //    //if (/*some condition*/)
            //    messages.Add(row);
            //    row = walker.GetNextSibling(row);
            //}

            //// MainWindow - MainMenuControl
            //var automationElement = AutomationElement.RootElement.FindFirst(TreeScope.Children,
            //   new PropertyCondition(AutomationElement.NameProperty, "MainMenuControl"));
            Thread.Sleep(2000);
            var app = AutomationElement.RootElement.FindByName("MainWindow");
            var automationElement = app.FindById("borrowButton");
           // var test = AutomationElement.RootElement.FindAll(TreeScope.Descendants, )
            AutomationElementCollection collection = app.FindAll(TreeScope.Descendants, Condition.TrueCondition);
            Console.WriteLine("****************************************************************************");
            Console.WriteLine("Any?" + collection.Count);
            foreach (AutomationElement ae in collection)
            {
                Console.WriteLine("Here:" + ae.Current.Name + "," + ae.Current.AutomationId + "," + ae.Current.ClassName);
            }
            //var automationElement2 = AutomationElement.RootElement.Find("label");
            Console.WriteLine("****************************************************************************");
            automationElement.Should().NotBeNull();

            automationElement.GetCurrentPropertyValue(AutomationElement.IsEnabledProperty).Should().Be(true);

            var patterns = automationElement.GetSupportedPatterns();
            var invokePattern = automationElement.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
            invokePattern.Invoke();

            ////AutomationElement child = walker.GetFirstChild(el);
            //var point = automationElement.GetClickablePoint();
            //Mouse.Move((int)point.X, (int)point.Y);
            //Mouse.Click(MouseButton.Left);

            //Condition windowCondition = new PropertyCondition(AutomationElement.NameProperty, "Window Title");

            // AutomationElement window = AutomationElement.RootElement.FindFirst(TreeScope.Element, windowCondition);


        }

    }

    static class AutomationElementHelpers
    {
        public static AutomationElement FindByName(this AutomationElement root, string name)
        {
            return root.FindFirst(
             TreeScope.Descendants,
             new PropertyCondition(AutomationElement.NameProperty, name));
        }

        public static AutomationElement FindById(this AutomationElement root, string name)
        {
            return root.FindFirst(
             TreeScope.Descendants,
             new PropertyCondition(AutomationElement.AutomationIdProperty, name));
        }

    }
}
