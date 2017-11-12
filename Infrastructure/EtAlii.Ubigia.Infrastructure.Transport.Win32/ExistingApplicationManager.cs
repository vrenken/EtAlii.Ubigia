namespace EtAlii.Ubigia.Infrastructure.Transport.Owin
{
    using EtAlii.Ubigia.Infrastructure.Transport;
    using global::Owin;

    public class ExistingApplicationManager : IApplicationManager 
    {
        private readonly IAppBuilder _applicationBuilder;

        public ExistingApplicationManager(IAppBuilder applicationBuilder)
        {
            _applicationBuilder = applicationBuilder;
        }

        public void Start(IComponentManager[] componentManagers)
        {
            foreach (var componentManager in componentManagers)
            {
                componentManager.Start(_applicationBuilder);
            }
        }

        public void Stop(IComponentManager[] componentManagers)
        {
            foreach (var componentManager in componentManagers)
            {
                componentManager.Stop();
            }
        }
    }
}
