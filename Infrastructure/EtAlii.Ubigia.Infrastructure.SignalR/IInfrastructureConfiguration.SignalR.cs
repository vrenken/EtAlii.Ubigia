namespace EtAlii.Ubigia.Infrastructure
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.SignalR;
    using Microsoft.AspNet.SignalR;

    public static class IInfrastructureConfigurationSignalRExtension
    {
        public static IInfrastructureConfiguration UseSignalR(this IInfrastructureConfiguration configuration)
        {
            var signalRDependencyResolver = new DefaultDependencyResolver();

            var extensions = new IInfrastructureExtension[]
            {
                new SignalRInfrastructureExtension(signalRDependencyResolver),
            };
            return configuration
                .Use(extensions)
                .Use((container, components) => new SignalRComponentManagerFactory().Create(signalRDependencyResolver, components));
        }
    }
}