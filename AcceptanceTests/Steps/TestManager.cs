using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using AcceptanceTests.Help;
using BoDi;
using Library.ApplicationInfratructure;
using TechTalk.SpecFlow;


namespace AcceptanceTests
{
   

    [Binding]
    public class TestManager
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool DestroyWindow(IntPtr hwnd);

        readonly IObjectContainer _objectContainer;

        public TestManager(IObjectContainer objectContainer)
        {
            this._objectContainer = objectContainer;
        }

        [Before]
        public void SetUp()
        {
            var process = Process.Start(@"C:\projects\assignment3\ITC515_NewLibrary_CSharp\Library\bin\Debug\Library.exe");
            _objectContainer.RegisterInstanceAs(process);
        }

        [After]
        public void TearDown()
        {
            var process = _objectContainer.Resolve<Process>();

            var element = AutomationElement.RootElement.FindByName(ViewNames.MainWindowView);
            element.SetFocus();

            process?.CloseMainWindow();
            process?.Close();
        }
    }
}
