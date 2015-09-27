using System;
using Autofac;
using Ploeh.AutoFixture.Kernel;

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