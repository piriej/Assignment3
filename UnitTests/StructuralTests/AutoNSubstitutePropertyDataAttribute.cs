using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace UnitTests
{
    public class AutoNSubstitutePropertyDataAttribute : CompositeDataAttribute
    {
        internal AutoNSubstitutePropertyDataAttribute(string propertyName)
            : base(
                new DataAttribute[] {
                    new PropertyDataAttribute(propertyName),
                    new AutoNSubstituteDataAttribute() })
        {
        }
    }
}