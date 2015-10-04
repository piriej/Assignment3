using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using log4net;
using Ploeh.AutoFixture.Kernel;

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