namespace EtAlii.Ubigia.Infrastructure.Tests
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using EtAlii.xTechnology.Diagnostics;
    using SignalR;
    public static class IInfrastructureConfigurationTestExtension
    {
        public static IInfrastructureConfiguration UseTestInfrastructure(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new TestInfrastructureExtension(diagnostics), 
            };
            return configuration
                .Use(extensions)
                .Use(typeof(WebApiComponentManager))
                .Use(typeof(SignalRComponentManager))
                .Use<TestInfrastructure>();
        }
    }
}