using System.Diagnostics;
using BoDi;
using TechTalk.SpecFlow;
using Xunit;

namespace AcceptanceTests
{
    [Binding]
    public class TestManager
    {
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
            process?.CloseMainWindow();
            process?.Close();
        }
    }
}
