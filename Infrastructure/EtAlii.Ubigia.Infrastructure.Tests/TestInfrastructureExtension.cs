namespace EtAlii.Ubigia.Infrastructure
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR;
    using EtAlii.Ubigia.Infrastructure.Transport.Owin.WebApi;
    using EtAlii.xTechnology.Diagnostics;
    using Microsoft.AspNet.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    public class TestInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IDiagnosticsConfiguration _diagnostics;
        private readonly IDependencyResolver _signalRDependencyResolver;

        internal TestInfrastructureExtension(IDiagnosticsConfiguration diagnostics, IDependencyResolver signalRDependencyResolver)
        {
            _diagnostics = diagnostics;
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SignalRApiScaffolding(_signalRDependencyResolver),
                new SignalRUserApiScaffolding(_signalRDependencyResolver),
                new SignalRAdminApiScaffolding(_signalRDependencyResolver),

                new WebApiApiScaffolding<TestAuthenticationIdentityProvider>(),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}