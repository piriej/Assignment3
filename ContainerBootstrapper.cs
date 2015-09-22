using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using Autofac;
using Library.ApplicationInfratructure;
using Library.Controllers;
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
using Prism.Autofac;
using Prism.Modularity;
using Prism.Mvvm;
using ShortBus;
using ICardReader = Library.Features.CardReader.ICardReader;
using IMediator = ShortBus.IMediator;
using Mediator = ShortBus.Mediator;

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

            //builder.RegisterType<CardReader>().SingleInstance();
       
            builder.RegisterType<Scanner>().SingleInstance().As<IScanner>();
            builder.RegisterType<Printer>().SingleInstance().As<IPrinter>();

            builder.RegisterType<MainMenuController>().SingleInstance();

            // View Models
            builder.RegisterType<MainWindowViewModel>().SingleInstance().As<IDisplay>().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<CardReaderViewModel>().SingleInstance()
                .As<ICardReader>()
                .As<ICardReaderEvents>();
                //.As<ICardReaderEvents>();
                //.As<ICardReaderListener>();
            builder.RegisterType<BorrowingViewModel>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies); 
            builder.RegisterType<BorrowingViewModel>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies); 
            builder.RegisterType<ScanBookViewModel>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //builder.RegisterType<BookDAO>().As<IBookDAO>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            //builder.RegisterType<LoanDAO>().As<ILoanDAO>().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
            builder.RegisterType<MemberDAO>().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //IBookDAO bookDAO, ILoanDAO loanDAO, IMemberDAO memberDAO,


            //builder.RegisterType<SwipeCardView>().Named("SwipeCard", typeof(SwipeCardView));
            builder.RegisterType<MainWindowView>().SingleInstance();
            builder.RegisterType<BorrowingView>().SingleInstance();
            builder.RegisterType<SwipeCardView>().SingleInstance();
            builder.RegisterType<CardReaderView>().SingleInstance();

            builder.RegisterType<ContentRegionModule>();


            // Mediator Module here...
            var assembly = typeof(ScanBookViewModel).Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IAsyncRequestHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IQueryHandler<,>))
                .AsImplementedInterfaces();

            builder.RegisterType<Mediator>().AsImplementedInterfaces().InstancePerLifetimeScope();
            //builder.RegisterType<CheckedMediator>().AsImplementedInterfaces().InstancePerLifetimeScope();

            // to allow ShortBus to resolve lifetime-scoped dependencies properly, 
            // we really can't use the default approach of setting the static (global) dependency resolver, 
            // since that resolves instances from the root scope passed into it, rather than 
            // the current lifetime scope at the time of resolution.  
            // Resolving from the root scope can cause resource leaks, or in the case of components with a 
            // specific scope affinity (AutofacWebRequest, for example) it would fail outright, 
            // since that scope doesn't exist at the root level.

            builder.RegisterType<ShortBus.Autofac.AutofacDependencyResolver>()
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<Mediator>().As<IMediator>();

        }

    }
}

