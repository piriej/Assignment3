//using System;
//using Autofac;
//using Autofac.Builder;
//using Autofac.Core.Lifetime;

//namespace IntegrationTests
//{
//    public static class ContainerFixture
//    {
//        private static readonly IContainer Container;

//        static ContainerFixture()
//        {
//            //Container = ContainerConfig.CreateContainer(); // This is what my production App_Start calls
//            AppDomain.CurrentDomain.DomainUnload += (sender, args) => Container.Dispose();
//        }

//        public static ILifetimeScope GetTestLifetimeScope(Action<ContainerBuilder> modifier = null)
//        {
//            return Container.BeginLifetimeScope(MatchingScopeLifetimeTags.RequestLifetimeScopeTag, cb => {
//                ExternalMocks(cb);
//                if (modifier != null)
//                    modifier(cb);
//            });
//        }

//        private static void ExternalMocks(ContainerBuilder cb)
//        {
//            cb.Register(_ => new StaticDateTimeProvider(DateTimeOffset.UtcNow.AddMinutes(1)))
//                .AsImplementedInterfaces()
//                .AsSelf()
//                .InstancePerTestRun();
//            // Other overrides of externals to the application ...
//        }
//    }

//    public static class RegistrationExtensions
//    {
//        // This extension method makes the registrations in the ExternalMocks method clearer in intent - I create a HTTP request lifetime around each test since I'm using my container in a web app
//        public static IRegistrationBuilder<TLimit, TActivatorData, TStyle> InstancePerTestRun
//            <TLimit, TActivatorData, TStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TStyle> registration,
//                params object[] lifetimeScopeTags)
//        {
//            return registration.InstancePerRequest(lifetimeScopeTags);
//        }
//    }
//}
