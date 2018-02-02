namespace EtAlii.Ubigia.Infrastructure.Transport.Owin.SignalR
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using Microsoft.AspNet.SignalR;
    using EtAlii.xTechnology.MicroContainer;

    public class SignalRInfrastructureExtension : IInfrastructureExtension
    {
        private readonly IDependencyResolver _signalRDependencyResolver;

        internal SignalRInfrastructureExtension(IDependencyResolver signalRDependencyResolver)
        {
            _signalRDependencyResolver = signalRDependencyResolver;
        }

        public void Initialize(Container container)
        {
            var scaffoldings = new IScaffolding[]
            {
                new SignalRApiScaffolding(_signalRDependencyResolver),
                new SignalRUserApiScaffolding(_signalRDependencyResolver),
                new SignalRAdminApiScaffolding(_signalRDependencyResolver),
            };

            foreach (var scaffolding in scaffoldings)
            {
                scaffolding.Register(container);
            }
        }
    }
}