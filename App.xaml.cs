using System.Windows;
using Autofac;
using Prism;

namespace Library
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var bootstrapper = new ContainerBootstrapper();
            bootstrapper.Run(true);

        }
    }
}
