using Autofac;
using log4net;
using Ploeh.AutoFixture;

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