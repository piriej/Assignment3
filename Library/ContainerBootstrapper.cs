using System;
using System.Globalization;
using System.Reflection;
using System.Windows;
using Autofac;
using Library.Controllers;
using Library.Hardware;
using Library.ViewModels;
using Library.Views;
using Library.Views.Borrowing;
using Library.Views.CardReaderWindow;
using Library.Views.MainWindow;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;

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

            //ViewModelLocationProvider.SetDefaultViewModelFactory((t) => _container.Resolve(t));

            // Modify the default convention to use feature folders, and separate projects for devices.
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                // Use initial convention if it can be found under views.
                var viewName = viewType.FullName;
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
                var viewModelName = string.Format(CultureInfo.InvariantCulture, " {0}Model, {1} ", viewName.Replace("Views", "ViewModels"), viewAssemblyName);
                var viewModelWithStandardConvention = Type.GetType(viewModelName);
                if (viewModelWithStandardConvention != null)
                    return viewModelWithStandardConvention;

                // Convention: The name of the view is the same as the name of the namespace
                var assemblyName = viewType.GetTypeInfo().Assembly.GetName().Name;
                var featuresRoot = "Views";
                var featureFolder = viewType.Name;
                var viewIdentifier = viewType.Name;
                var modelSuffix = "ViewModel";
                var featureFullName = $"{assemblyName}.{featuresRoot}.{featureFolder}.{viewIdentifier}{modelSuffix}, {viewAssemblyName}";
                var viewModelWithFeatureConvention = Type.GetType(featureFullName);
              
                return viewModelWithFeatureConvention;
            });

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

