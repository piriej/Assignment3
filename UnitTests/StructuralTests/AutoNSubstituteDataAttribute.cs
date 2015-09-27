using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace UnitTests
{
    //public class AutoNSubstituteDataAttribute : AutoDataAttribute
    //{
    //    public AutoNSubstituteDataAttribute()
    //        : base(new Fixture()
    //            .Customize(new AutoNSubstituteCustomization()))
    //    {
    //    }


    //}

    public class AutoNSubstituteDataAttribute : AutoDataAttribute
    {
        internal AutoNSubstituteDataAttribute()
            : base(new Fixture().Customize(new AutoNSubstituteCustomization()))
        {
        }
    }

}