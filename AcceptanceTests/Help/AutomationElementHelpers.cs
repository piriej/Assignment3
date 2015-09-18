using System;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace AcceptanceTests.Help
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

        public static void DisplayChildren(this AutomationElement aeRoot)
        {
            var separator = string.Concat(Enumerable.Repeat("*", 20));
            var collection = aeRoot.FindAll(TreeScope.Descendants, Condition.TrueCondition);

            Console.WriteLine(separator);
            Console.WriteLine(@"Number of elements:{0}", collection.Count);
            foreach (AutomationElement ae in collection)
            {
                Console.WriteLine(@"Name,Label,ClassName:{0},{1},{2}", ae.Current.Name, ae.Current.AutomationId,
                    ae.Current.ClassName);
            }
            Console.WriteLine(separator);
        }

    }
}