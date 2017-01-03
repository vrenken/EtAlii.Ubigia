namespace EtAlii.Ubigia.Infrastructure.Tests
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.AspNet.SignalR;
    using SignalR;
    public static class IInfrastructureConfigurationTestExtension
    {
        private static readonly SignalRComponentManagerFactory _signalRFactory = new SignalRComponentManagerFactory();
        private static readonly WebApiComponentManagerFactory _webApiFactory = new WebApiComponentManagerFactory();

        public static IInfrastructureConfiguration UseTestInfrastructure(this IInfrastructureConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var signalRDependencyResolver = new DefaultDependencyResolver();

            var extensions = new IInfrastructureExtension[]
            {
                new TestInfrastructureExtension(diagnostics, signalRDependencyResolver), 
            };
            return configuration
                .Use(extensions)
                .Use(_webApiFactory.Create)
                .Use(container => _signalRFactory.Create(signalRDependencyResolver))
                .Use<TestInfrastructure>();
        }
    }
}