﻿namespace EtAlii.xTechnology.Hosting
{
    using System.Linq;
    using Microsoft.Extensions.Configuration;

    public class SystemFactory
    {
        private readonly ServiceFactory _serviceFactory;
        private readonly ModuleFactory _moduleFactory;
        private readonly IInstanceCreator _instanceCreator;

        public SystemFactory(
            ServiceFactory serviceFactory, 
            ModuleFactory moduleFactory, 
            IInstanceCreator instanceCreator)
        {
            _serviceFactory = serviceFactory;
            _moduleFactory = moduleFactory;
            _instanceCreator = instanceCreator;
        }

        /// <summary>
        /// Create a host using the specified configuration section.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="systemConfiguration"></param>
        /// <param name="configurationDetails"></param>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public ISystem Create(
            IHost host, 
            IConfigurationSection systemConfiguration,
            IConfigurationDetails configurationDetails)
        {
            if(!_instanceCreator.TryCreate<ISystem>(systemConfiguration, configurationDetails, "system", out var system))
            {
                system = new DefaultSystem();
            }

            var services = systemConfiguration
                .GetAllSections("Services")
                .Select(scs => _serviceFactory.Create(host, system, null, scs, configurationDetails))
                .ToArray();

            var modules = systemConfiguration
                .GetAllSections("Modules")
                .Select(mcs => _moduleFactory.Create(host, system, null, mcs, configurationDetails))
                .ToArray();

            system.Setup(host, services, modules);
            return system;
        }
    }
}