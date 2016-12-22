namespace EtAlii.Servus.Infrastructure.Tests
{
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.WebApi;
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