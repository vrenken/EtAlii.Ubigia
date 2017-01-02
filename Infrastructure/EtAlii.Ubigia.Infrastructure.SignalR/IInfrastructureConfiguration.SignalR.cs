namespace EtAlii.Ubigia.Infrastructure
{
    using EtAlii.Ubigia.Infrastructure.Functional;
    using EtAlii.Ubigia.Infrastructure.SignalR;

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