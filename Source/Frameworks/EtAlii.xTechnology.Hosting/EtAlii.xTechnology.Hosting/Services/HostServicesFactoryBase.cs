// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Collections.Generic;

    public abstract class HostServicesFactoryBase : IHostServicesFactory
    {
        public abstract IService[] Create(IHostOptions options);

        protected void TryAddService(List<IService> services, IHostOptions options, string configurationSectionName)
        {
            var configurationSection = options.ConfigurationRoot.GetSection(configurationSectionName);
            if (configurationSection != null)
            {
                if(ServiceConfiguration.TryCreate(configurationSection, options.ConfigurationRoot, options.Details, out var serviceConfiguration))
                {
                    var service = new ServiceFactory().Create(serviceConfiguration);
                    services.Add(service);
                }
            }
        }
    }
}
