namespace EtAlii.Ubigia.Infrastructure.Tests
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.SignalR;
    using EtAlii.Ubigia.Infrastructure.WebApi;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.AspNet.SignalR;
    using SimpleInjector;
    using Newtonsoft.Json;
    using System;

    public class TestInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;

        internal TestInfrastructureExtension(IDiagnosticsConfiguration diagnostics)
        {
            _diagnostics = diagnostics;
        }

        public void Initialize(Container container)
        {
            var signalRDependencyResolver = new DefaultDependencyResolver();

            var scaffoldings = new IScaffolding[]
            {
                new SignalRApiScaffolding(signalRDependencyResolver),
                new SignalRUserApiScaffolding(signalRDependencyResolver),
                new SignalRAdminApiScaffolding(signalRDependencyResolver),

                new WebApiApiScaffolding<TestAuthenticationIdentityProvider>(),
                new WebApiUserApiScaffolding(),
                new WebApiAdminApiScaffolding(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}