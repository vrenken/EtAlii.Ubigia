namespace EtAlii.Servus.Infrastructure.SignalR
{
    using System;
    using EtAlii.Servus.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;
    using Newtonsoft.Json;
    using SimpleInjector;

    public class SignalRInfrastructureExtension : IInfrastructureExtension
    {

        internal SignalRInfrastructureExtension()
        {
        }

        public void Initialize(Container container)
        {
            var signalRDependencyResolver = new DefaultDependencyResolver();

            var scaffoldings = new IScaffolding[]
            {
                new SignalRApiScaffolding(signalRDependencyResolver),
                new SignalRUserApiScaffolding(signalRDependencyResolver),
                new SignalRAdminApiScaffolding(signalRDependencyResolver),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}