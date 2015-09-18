
using System.Windows;
using Autofac;
using Library.Hardware;
using Library.Interfaces.Hardware;
using Prism.Autofac;
using Prism.Modularity;

namespace Library
{
    public class ContainerBootstrapper : AutofacBootstrapper
    {
        protected override void InitializeShell()
        {
            base.InitializeShell();

            // hook the main application window with your Shell
            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            builder.RegisterType<CardReader>().SingleInstance();
            builder.RegisterType<Scanner>().SingleInstance();
            builder.RegisterType<Printer>().SingleInstance();

            builder.RegisterType<MainWindow>().SingleInstance();

            base.ConfigureContainerBuilder(builder);
        }

        protected override DependencyObject CreateShell()
        {
            // here allow the ServiceLocator to create an instance of Shell, passing it dependencies
            return Container.Resolve<MainWindow>();
        }
    }
}

