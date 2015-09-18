using System.Threading;
using System.Windows.Automation;

namespace AcceptanceTests.Steps.Arrange
{
    static class AutomationElementHelpers
    {
        const int ImplicitWait = 2;
        const int ImplicitWaitInterval = 1000;

        public static AutomationElement FindByName(this AutomationElement root, string name, int wait = ImplicitWait)
        {
            var waitCount = 0;
            AutomationElement node;
            do
            {
                Thread.Sleep(ImplicitWaitInterval);
                node = root.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.NameProperty, name));
                waitCount++;
            } while (node == null || waitCount == wait);

            return node;
        }

        public static AutomationElement FindById(this AutomationElement root, string id, int wait = ImplicitWait)
        {

            var waitCount = 0;
            AutomationElement node;
            do
            {
                Thread.Sleep(ImplicitWaitInterval);
                node = root.FindFirst(
                    TreeScope.Descendants,
                    new PropertyCondition(AutomationElement.AutomationIdProperty, id));
                waitCount++;
            } while (node == null || waitCount == wait);

            return node;
        }

    }
}