
using System.Windows;
using Autofac;
using Library.Hardware;
using Library.Interfaces.Hardware;
using Prism.Autofac;

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
            builder.RegisterType<CardReader>().SingleInstance();//.As<ICardReader>();
            builder.RegisterType<Scanner>().SingleInstance();//.As<IScanner>();
            builder.RegisterType<Printer>().SingleInstance();//.As<IPrinter>();

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

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using Autofac;
//using Prism.Autofac;
//using Prism.Modularity;

//namespace Library
//{
//    class Bootstrapper : AutofacBootstrapper
//    {

//        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
//        {
//            base.ConfigureContainerBuilder(builder);
//            builder.RegisterType<Shell>();

//            // register autofac module
//            builder.RegisterModule<MyModuleConfiguration>();
//        }

//        protected override void ConfigureModuleCatalog()
//        {
//            base.ConfigureModuleCatalog();

//            // register prism module
//            Type typeNewsModule = typeof(MyModule);
//            ModuleCatalog.AddModule(new ModuleInfo(typeNewsModule.Name, typeNewsModule.AssemblyQualifiedName));
//        }

//        protected override DependencyObject CreateShell()
//        {
//            return Container.Resolve<Shell>();
//        }

//        protected override void InitializeShell()
//        {
//            base.InitializeShell();

//            Application.Current.MainWindow = (Shell)this.Shell;
//            Application.Current.MainWindow.Show();
//        }
//    }
//}
