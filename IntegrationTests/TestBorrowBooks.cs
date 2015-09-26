using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using AutofacContrib.NSubstitute;
using log4net;
using Library;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;
using Ploeh.AutoFixture.AutoNSubstitute;
using FluentAssertions;
using Library.ApplicationInfratructure;

namespace IntegrationTests
{
    public class TestBorrowBooks
    {
        public TestBorrowBooks()
        {
            log4net.Config.XmlConfigurator.Configure();
            IOConfig.initLog4();
        }


        [Theory, ContainerData]
        public void DummyTest(BorrowingViewModel borrowingViewModel, CardReaderViewModel cardReaderViewModel/*,*/ /*IBorrowController borrowController MemberDAO mem/*,*/ /*tst tmp*/)
        {
            //Card reader should initially be disabled.
            cardReaderViewModel.Enabled.Should().BeFalse();

            //TODO: Event on the card reader checks the status.
            var x = borrowingViewModel.Active;

            // By default we should not be borrowing.
            borrowingViewModel.Active.Should().BeFalse();

            // A user requests to borrow.
            borrowingViewModel.Active = true;
    
            // Then the Card reader is enabled.
            cardReaderViewModel.Enabled.Should().BeTrue();
        }
    }


    public class ContainerDataAttribute : AutoDataAttribute
    {
        public ContainerDataAttribute()
            : base(new Fixture().Customize(
                new ContainerCustomization(
                   new AutoSubstitute(builder => builder.Configure()).Container)))
        {

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
    }
    public void Customize(IFixture fixture)
    {
        log4net.Config.XmlConfigurator.Configure();
        ILog log = LogManager.GetLogger(typeof(ContainerCustomization));

        //fixture.ResidueCollectors.Add(new ChildContainerSpecimenBuilder(this._container));
        fixture.ResidueCollectors.Add(new ContainerSpecimenBuilder(this._container));
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
        ILog log = LogManager.GetLogger(typeof(ContainerSpecimenBuilder));

        var type = request as Type;
        if (type == null) return new NoSpecimen();

        log.Debug(@"Container Specimen Builder Resolving type: " + request.GetType());

        object service;

        var res =
            string.Join("==> ", _container.ComponentRegistry.Registrations.Select(
                r => string.Join(",", r.Services.Select(x => x.Description))));
        log.Debug(res);
       
        try
        {
            if (!_container.TryResolve(type, out service))
            {
                log.Debug(@"No Specimen found in the container");
                return new NoSpecimen();
            }
        }
        catch (Exception ex)
        {
            log.Warn("No Resolution error: " + ex.Message);
            log.Debug(@"Container Specimen Builder creating substitute for: " + request.GetType());
            return new NoSpecimen();
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

