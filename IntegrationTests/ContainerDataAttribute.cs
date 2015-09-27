using AutofacContrib.NSubstitute;
using Library;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Xunit;

namespace IntegrationTests
{
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