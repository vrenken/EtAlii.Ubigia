// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public abstract class HostServicesFactoryBase : IHostServicesFactory
    {
        public abstract IService[] Create(HostOptions options, IHost host);

        protected void TryAddService(List<IService> services, IHost host, HostOptions options, string configurationSectionName)
        {
            var configurationSection = options.ConfigurationRoot.GetSection(configurationSectionName);
            if (configurationSection != null && ServiceConfiguration.TryCreate(configurationSection, options.ConfigurationRoot, out var serviceConfiguration))
            {
                var service = new ServiceFactory().Create(serviceConfiguration, host);
                services.Add(service);
            }
        }
    }
}
