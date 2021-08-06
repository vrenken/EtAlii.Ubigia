// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
            IConfigurationRoot configurationRoot,
            IConfigurationDetails configurationDetails)
        {
            var service = serviceFactory.Create(serviceConfiguration, configurationRoot, configurationDetails);
            service.Setup(host, system, parentModule);
            return service;
        }

        public IService Create(
            IHost host, ISystem system,
            IModule parentModule,
            IConfigurationSection serviceConfiguration,
            IConfigurationRoot configurationRoot,
            IConfigurationDetails configurationDetails)
        {
            _instanceCreator.TryCreate<IService>(serviceConfiguration, configurationRoot, configurationDetails, "service", out var service, true);
            service.Setup(host, system, parentModule);
            return service;
        }
    }
}
