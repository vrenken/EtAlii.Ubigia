namespace EtAlii.Servus.Infrastructure
{
    using EtAlii.Servus.Infrastructure.Functional;
    using EtAlii.Servus.Infrastructure.SignalR;

    public static class IInfrastructureConfigurationSignalRExtension
    {
        public static IInfrastructureConfiguration UseSignalR(this IInfrastructureConfiguration configuration)
        {
            var extensions = new IInfrastructureExtension[]
            {
                new SignalRInfrastructureExtension(),
            };
            return configuration
                .Use(extensions)
                .Use(typeof(ISignalRComponentManager));
        }
    }
}