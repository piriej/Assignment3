﻿using System;
using System.Linq;
using System.Threading;
using System.Windows.Automation;

namespace AcceptanceTests.Help
{
    static class AutomationElementHelpers
    {
        const int ImplicitWait = 3;
        const int ImplicitWaitInterval = 1000;

        static AutomationElement FindBy(AutomationElement root, int wait, PropertyCondition condition)
        {
            var waitCount = 0;
            AutomationElement node;

            do
            {
                node = root.FindFirst(
                    TreeScope.Descendants,
                    condition);
           
                if(node == null && waitCount <= wait)
                    Thread.Sleep(ImplicitWaitInterval);

                waitCount++;
            } while (node == null && waitCount <= wait);

            return node;
        }
        public static AutomationElement FindByName(this AutomationElement root, string name, int wait = ImplicitWait)
        {
            var condition = new PropertyCondition(AutomationElement.NameProperty, name);

            return FindBy(root, wait, condition);
        }

        public static AutomationElement FindByClass(this AutomationElement root, string name, int wait = ImplicitWait)
        {
            var condition = new PropertyCondition( AutomationElement.ClassNameProperty, name);

            return FindBy(root, wait, condition);
        }

        public static AutomationElement FindById(this AutomationElement root, string id, int wait = ImplicitWait)
        {
            var condition =  new PropertyCondition(AutomationElement.AutomationIdProperty, id);

            return FindBy(root, wait, condition);
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