using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using log4net;
using Library;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using Ploeh.AutoFixture.Kernel;
using Xunit.Extensions;
using FluentAssertions;
using Library.ApplicationInfratructure;
using Library.Controllers.Borrow;
using Library.Features.ScanBook;
using NSubstitute;

namespace IntegrationTests
{
    public class TestBorrowBooks
    {
        public TestBorrowBooks()
        {
            log4net.Config.XmlConfigurator.Configure();
            IOConfig.initLog4();
        }


        //[Theory, ContainerData]
        //public void ReadCard_WhenRequested_ActivatesCardReader(BorrowingViewModel borrowingViewModel, CardReaderViewModel cardReaderViewModel/*,*/ /*IBorrowController borrowController MemberDAO mem/*,*/ /*tst tmp*/)
        //{
        //    //Card reader should initially be disabled.
        //    cardReaderViewModel.Enabled.Should().BeFalse();

        //    // By default we Should have an active borrow button.
        //    borrowingViewModel.Active.Should().BeTrue();

        //    // A user requests to borrow.
        //    borrowingViewModel.Active = true;
        //    borrowingViewModel.BorrowCommand.Execute(null);

        //    // Then the Card reader is enabled.
        //    cardReaderViewModel.Enabled.Should().BeTrue();
        //}

        //[Theory, ContainerData]
        //public void ReadCard_WhenRequested_ActivatesCardReader(IBorrowingViewModel borrowingViewModel, ICardReaderViewModel cardReaderViewModel/*,*/ /*IBorrowController borrowController MemberDAO mem/*,*/ /*tst tmp*/)
        //{
        //    //Card reader should initially be disabled.
        //    cardReaderViewModel.Enabled.Should().BeFalse();

        //    // By default we Should have an active borrow button.
        //    borrowingViewModel.Active.Should().BeTrue();

        //    // A user requests to borrow.
        //    borrowingViewModel.Active = false;
        //    //borrowingViewModel.BorrowCommand.Execute(null);

        //    // Then the Card reader is enabled.
        //    cardReaderViewModel.Enabled.Should().BeTrue();
        //}

        [Theory, ContainerData]
        public void SwipeCard_WithValidBorrowerId_SetsBorrowerDetailsForBookScanner(IScanBookController scanBookController, IBorrowController borrowController, ICardReaderViewModel cardReaderViewModel, ICardReaderController cardReaderController, IScanBookViewModel scanBookViewModel)
        {
            AutoMapperConfig.RegisterMaps();

            // The borrow controller has been clicked.
            borrowController.WaitForCardSwipe();

            // The card is swiped with a known user.
            cardReaderViewModel.BorrowerId = "0001";

            // When the card is swiped.
            cardReaderController.CardSwiped(cardReaderViewModel.BorrowerId);

            scanBookViewModel.BorrowerId.Should().Be(1);
            scanBookViewModel.Name.Should().Be("fName1 lName1");
            scanBookViewModel.Contact.Should().Be("0001");
        }
    }

    // Test borrowing restricted


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
            log.Warn("No Resolution error: "  +request.GetType() + " -->" + ex.Message );

            return new NoSpecimen();
            //log.Debug(@"Container Specimen Builder creating substitute for: " + request.GetType());
            //var substitute = Substitute.For(new[] { type }, new object[] { });
            //return substitute;
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

