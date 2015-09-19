using System;
using System.Windows;
using Autofac;
using Library.Controllers;
using Library.Hardware;
using Library.ViewModels;
using Library.Views;
using Prism.Autofac;
using Prism.Modularity;

namespace Library
{
    public class ContainerBootstrapper : AutofacBootstrapper
    {
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            // register prism module
            Type contentRegionModule = typeof(ContentRegionModule);
            ModuleCatalog.AddModule(new ModuleInfo(contentRegionModule.Name, contentRegionModule.AssemblyQualifiedName));
        }

        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            base.InitializeShell();

            Application.Current.MainWindow = (Window)Shell;
            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainerBuilder(ContainerBuilder builder)
        {
            base.ConfigureContainerBuilder(builder);

            //builder.RegisterType<CardReader>().SingleInstance();
            builder.RegisterType<CardReaderWindow>().SingleInstance();
            builder.RegisterType<Scanner>().SingleInstance();
            builder.RegisterType<Printer>().SingleInstance();
            builder.RegisterType<MainMenuController>().SingleInstance();

            // View Models
            builder.RegisterType<MainWindowViewModel>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CardReaderWindowViewModel>().SingleInstance();
            builder.RegisterType<BorrowingViewModel>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies); 
            builder.RegisterType<ScanBookViewModel>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies); 

            builder.RegisterType<SwipeCard>().Named("SwipeCard", typeof(SwipeCard));

            builder.RegisterType<MainWindow>().SingleInstance();
            builder.RegisterType<Borrowing>().SingleInstance();
            builder.RegisterType<SwipeCard>().SingleInstance();

            builder.RegisterType<ContentRegionModule>();

        }

    }
}

