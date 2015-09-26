using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using Autofac;
using Autofac.Core;
using log4net;
using Library.ApplicationInfratructure;
using Library.ApplicationInfratructure.Modules;
using Library.Controllers;
using Library.Controllers.Borrow;
using Library.Daos;
using Library.Entities;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using Library.Features.MainWindow;
using Library.Features.ScanBook;
using Library.Features.SwipeCard;
using Library.Hardware;
using Library.Interfaces.Hardware;
using Ploeh.AutoFixture;
using Prism.Autofac;
using Prism.Events;
using Prism.Modularity;
using Prism.Mvvm;
using Module = Autofac.Module;

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
        private static readonly ILog log = LogManager.GetLogger(typeof(ContainerBootstrapper));
     
        protected override DependencyObject CreateShell()
        {

            log4net.Config.XmlConfigurator.Configure();

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
                var featureFolder = viewType.Name.Replace("View", "");
                var viewIdentifier = viewType.Name;
                var modelSuffix = "Model";
                var featureFullName = $"{assemblyName}.{featuresRoot}.{featureFolder}.{viewIdentifier}{modelSuffix}, {viewAssemblyName}";

                var viewModelWithFeatureConvention = Type.GetType(featureFullName);
                var viewModelInterfaceWithFeatureConvention = (viewModelWithFeatureConvention?.GetInterfaces())?.LastOrDefault(x => x != typeof(INotifyPropertyChanged));
                return viewModelInterfaceWithFeatureConvention ?? viewModelWithFeatureConvention;
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
        public static void initLog4()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(IOConfig));
        public static ContainerBuilder Configure(this ContainerBuilder builder)
        {
            builder.RegisterModule<LogRequestsModule>();

            builder.RegisterType<Scanner>().SingleInstance().As<IScanner>();
            builder.RegisterType<Printer>().SingleInstance().As<IPrinter>();

            builder.RegisterType<MainMenuController>().SingleInstance();
            builder.RegisterType<MainWindowController>().As<IMainWindowController>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CardReaderController>().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<BorrowController>()
                .AsImplementedInterfaces()
                .SingleInstance()
                .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
                //.OnActivated(x => x.Context.Resolve<ICardReader2>().SubscribeToBorrower(x.Context.Resolve<IEventAggregator>()));

            builder.RegisterType<MainWindowViewModel>().SingleInstance().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CardReaderViewModel>().SingleInstance().AsImplementedInterfaces();
            builder.RegisterType<BorrowingViewModel>().SingleInstance().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<ScanBookViewModel>().SingleInstance().AsImplementedInterfaces().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<MemberDAO>().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<MainWindowView>().SingleInstance();
            builder.RegisterType<BorrowingView>().SingleInstance();
            builder.RegisterType<SwipeCardView>().SingleInstance();
            builder.RegisterType<CardReaderView>().SingleInstance();

            //Entities
            //builder.RegisterType<Book>().AsImplementedInterfaces();
            //builder.RegisterType<Loan>().AsImplementedInterfaces();
            //builder.RegisterType<Member>().AsImplementedInterfaces();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            builder.RegisterType<ContentRegionModule>();  // Note this is WPF module.

            builder.RegisterModule(new MockDataProviderModule());

            return builder;
        }
    }

    public class LogRequestsModule : Module
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogRequestsModule));
        protected override void AttachToComponentRegistration(
          IComponentRegistry componentRegistry,
          IComponentRegistration registration)
        {
            base.AttachToComponentRegistration(componentRegistry, registration);
            registration.Preparing += (sender, args) =>
              log.Debug($@"Resolving concrete type {args.Component.Activator.LimitType}");
        }
    }
}

