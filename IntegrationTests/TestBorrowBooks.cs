using System;
using System.Diagnostics;
using System.Linq;
using Autofac;
using Autofac.Core;
using AutofacContrib.NSubstitute;
using log4net;
using Library;
using Library.Daos;
using Library.Features.Borrowing;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;
using Ploeh.AutoFixture.AutoNSubstitute;
using Prism.Regions;

namespace IntegrationTests
{
    public class TestBorrowBooks
    {
        public TestBorrowBooks()
        {
            log4net.Config.XmlConfigurator.Configure();
            IOConfig.initLog4();
        }

        //private static readonly ILog log = LogManager.GetLogger(typeof(TestBorrowBooks));

        [Theory, ContainerData]
        //[Theory, AutoData]
        //[Theory, MyAutoData]
        public void DummyTest(BorrowingViewModel borrowingViewModel/*,*/ /*IBorrowController borrowController MemberDAO mem/*,*/ /*tst tmp*/)
        {
            //log4net.Config.XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger(typeof(TestBorrowBooks));
            log.Debug("Test2");
            var fixture = new Fixture();
            //var d = fixture.Build<Itst>();

            // The person clicks on the button on the borrower screen
            //  borrowingViewModel.Borrowing = true;

            // Then the Card reader is enabled.
            //var x = mem.GetMemberByID(2);

        }
    }
    internal class MyAutoDataAttribute : AutoDataAttribute
    {
        internal MyAutoDataAttribute()
            : base(
                new Fixture().Customize(
                    new CompositeCustomization(
                        new MyCustomization())))
        {
        }

        private class MyCustomization : ICustomization
        {
            public void Customize(IFixture fixture)
            {
                fixture.Customize<IRegionManager>(x => x.FromFactory(new RegionManagerStubSpecimenBuilder()));
            }
        }
    }


    public class RegionManagerStubSpecimenBuilder : ISpecimenBuilder
    {
        readonly IContainer _container;
        public object Create(object request, ISpecimenContext context)
        {
            return new RegionManagerStub();
        }
    }


    internal class RegionManagerStub : IRegionManager
    {
        public IRegionManager CreateRegionManager()
        {
            throw new NotImplementedException();
        }

        public IRegionManager AddToRegion(string regionName, object view)
        {
            throw new NotImplementedException();
        }

        public IRegionManager RegisterViewWithRegion(string regionName, Type viewType)
        {
            throw new NotImplementedException();
        }

        public IRegionManager RegisterViewWithRegion(string regionName, Func<object> getContentDelegate)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, Uri source, Action<NavigationResult> navigationCallback)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, Uri source)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, string source, Action<NavigationResult> navigationCallback)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, string source)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, string target, Action<NavigationResult> navigationCallback,
            NavigationParameters navigationParameters)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, Uri target, NavigationParameters navigationParameters)
        {
            throw new NotImplementedException();
        }

        public void RequestNavigate(string regionName, string target, NavigationParameters navigationParameters)
        {
            throw new NotImplementedException();
        }

        public IRegionCollection Regions { get; }
    }

    public class ContainerDataAttribute : AutoDataAttribute
    {
        public ContainerDataAttribute()
            : base(new Fixture().Customize(
                new ContainerCustomization(
                   // new ContainerBuilder().Configure().Build())))
                   //new AutoSubstitute((x) => new ContainerBuilder().Configure()).Container))) 
                   new AutoSubstitute((x) => new ContainerBuilder().Configure().Update(GetLoggingBuilder())).Container)))
                {

                }

        protected static IContainer GetLoggingBuilder()
        {
            var updater = new ContainerBuilder();
            updater.RegisterModule<LogRequestsModule>();
            return updater.Build();
            //return updater;

        }
    }
}

public class ChildContainerSpecimenBuilder : ISpecimenBuilder
{
    readonly IContainer _container;
    public ChildContainerSpecimenBuilder(IContainer container)
    {
        _container = container;
    }
    public object Create(object request, ISpecimenContext context)
    {
        var type = request as Type;
        if (type == null || type != typeof(IContainer))
        {
            return new NoSpecimen();
        }
        return _container; //chhild container?;
    }
}



public class ContainerCustomization : ICustomization
{
    readonly IContainer _container;

    public ContainerCustomization(IContainer container)
    {
        this._container = container;
        //container.ComponentRegistry.RegisterType<ContentRegionModule>();
    }
    public void Customize(IFixture fixture)
    {
        log4net.Config.XmlConfigurator.Configure();
        ILog log = LogManager.GetLogger(typeof(ContainerCustomization));

        fixture.ResidueCollectors.Add(new ChildContainerSpecimenBuilder(this._container));
        fixture.ResidueCollectors.Add(new ContainerSpecimenBuilder(this._container));
        fixture.ResidueCollectors.Add(new AutoNSubstituteCustomization().Builder);
    }
}


public class ContainerSpecimenBuilder : ISpecimenBuilder
{
    readonly IContainer _container;
    public ContainerSpecimenBuilder(IContainer container)
    {
        _container = container;

    }

    public object Create(object request, ISpecimenContext context)
    {
        ILog log = LogManager.GetLogger(typeof(ContainerCustomization));

        var type = request as Type;
        if (type == null) return new NoSpecimen();

        log.Debug(@"Container Specimen Builder Resolving type: " + request.GetType());

        object service;

        log.Debug("Container Registrations:" + _container.ComponentRegistry.Registrations.Select(r => string.Join(",", r.Services.Select(x => x.Description))));
        try
        {
            if (!_container.TryResolve(request.GetType(), out service))
            {
                //Console.WriteLine(@"Container Specimen Builder creating substitute for: " + request.GetType());
               
                log.Debug(@"No Specimen found in the container");
                return new NoSpecimen();
            }
        }
        catch (Exception)
        {
            var substitute = Substitute.For(new[] { type }, new object[] { });
            return substitute;
        }
       

        log.Debug(@"Container Specimen Builder Returning type: " + request.GetType());
        return service;
    }

    public class LogRequestsModule : Module
    {


        private static readonly ILog log = LogManager.GetLogger(typeof(Library.LogRequestsModule));
        protected override void AttachToComponentRegistration(
          IComponentRegistry componentRegistry,
          IComponentRegistration registration)
        {
            registration.Preparing += (sender, args) =>
              log.Debug($@"Resolving concrete type {args.Component.Activator.LimitType}");
        }
    }
}

