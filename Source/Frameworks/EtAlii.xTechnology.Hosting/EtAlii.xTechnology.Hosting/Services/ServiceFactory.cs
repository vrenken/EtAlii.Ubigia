namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public class ServiceFactory
    {
        private readonly IInstanceCreator _instanceCreator;

        public ServiceFactory(IInstanceCreator instanceCreator)
        {
            _instanceCreator = instanceCreator;
        }

        public IService Create(
            IHost host, ISystem system, 
            IModule parentModule,
            IServiceFactory serviceFactory, 
            IConfigurationSection serviceConfiguration,
            IConfigurationDetails configurationDetails)
        {
            var service = serviceFactory.Create(serviceConfiguration, configurationDetails);
            service.Setup(host, system, parentModule);
            return service;
        }

        public IService Create(
            IHost host, ISystem system, 
            IModule parentModule,
            IConfigurationSection serviceConfiguration,
            IConfigurationDetails configurationDetails)
        {
            _instanceCreator.TryCreate<IService>(serviceConfiguration, configurationDetails, "service", out var service, true);
            service.Setup(host, system, parentModule);
            return service;
        }
    }
}