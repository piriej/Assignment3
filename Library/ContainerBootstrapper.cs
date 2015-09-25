using System;
using System.CodeDom;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using Autofac;
using AutoMapper;
using Library.ApplicationInfratructure;
using Library.Controllers;
using Library.Controllers.Borrow;
using Library.Daos;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using Library.Features.MainWindow;
using Library.Features.ScanBook;
using Library.Features.SwipeCard;
using Library.Hardware;
using Library.Interfaces.Daos;
using Library.Interfaces.Hardware;
using MediatR;
using Microsoft.Practices.ServiceLocation;
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using StructureMap.Diagnostics;
using StructureMap.Graph;
//using ShortBus;
using ICardReader = Library.Features.CardReader.ICardReader;
//using IMediator = ShortBus.IMediator;
//using Mediator = ShortBus.Mediator;

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
            AutoMapperConfig.RegisterMaps();

            ViewModelLocationProvider.SetDefaultViewModelFactory((t) => Container.Resolve(t));

            // Modify the default convention to use feature folders, and separate projects for devices.
            ViewModelLocationProvider.SetDefaultViewTypeToViewModelTypeResolver(viewType =>
            {
                // Use initial convention if it can be found under views.
                var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;

                // Convention: The name of the view is the same as the name of the namespace
                var assemblyName = viewType.GetTypeInfo().Assembly.GetName().Name;
                var featuresRoot = "Features";
                var featureFolder = viewType.Name.Replace("View","");
                var viewIdentifier = viewType.Name;
                var modelSuffix = "Model";
                var featureFullName = $"{assemblyName}.{featuresRoot}.{featureFolder}.{viewIdentifier}{modelSuffix}, {viewAssemblyName}";

                var viewModelWithFeatureConvention = Type.GetType(featureFullName);
                var viewModelInterfaceWithFeatureConvention = (viewModelWithFeatureConvention?.GetInterfaces())?.LastOrDefault(x => x != typeof(INotifyPropertyChanged));
                return viewModelInterfaceWithFeatureConvention??viewModelWithFeatureConvention;
            });

            return Container.Resolve<MainWindowView>();
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
            builder.Configure();
        }

    }

    public static class IOConfig
    {
        public static ContainerBuilder Configure(this ContainerBuilder builder)
        {
            builder.RegisterType<Scanner>().SingleInstance().As<IScanner>();
            builder.RegisterType<Printer>().SingleInstance().As<IPrinter>();

            builder.RegisterType<MainMenuController>().SingleInstance();
            builder.RegisterType<MainWindowController>().As<IMainWindowController>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<BorrowController>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                .OnActivated(x => x.Context.Resolve<CardReaderViewModel>().ListenToBorrower(x.Instance));


            builder.RegisterType<MainWindowViewModel>().SingleInstance().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CardReaderViewModel>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<BorrowingViewModel>().SingleInstance().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<ScanBookViewModel>().SingleInstance().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<MemberDAO>().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<MainWindowView>().SingleInstance();
            builder.RegisterType<BorrowingView>().SingleInstance();
            builder.RegisterType<SwipeCardView>().SingleInstance();
            builder.RegisterType<CardReaderView>().SingleInstance();

            builder.RegisterType<ContentRegionModule>();

            return builder;
        }
    }
}

