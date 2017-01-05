namespace EtAlii.Ubigia.Infrastructure.Transport.SignalR
{
    using EtAlii.Ubigia.Infrastructure.Functional;
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
                .Use((container, componentFactories) => new SignalRComponentManagerFactory().Create(signalRDependencyResolver, componentFactories));
        }
    }
}