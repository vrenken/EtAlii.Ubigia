// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting;

using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Serilog;

public abstract class HostServicesFactoryBase : IHostServicesFactory
{
    private readonly ILogger _logger = Log.ForContext<HostServicesFactoryBase>();

    public abstract IService[] Create(IConfigurationRoot configurationRoot);

    protected void TryAddService(List<IService> services, IConfigurationRoot configurationRoot, string configurationSectionName)
    {
        _logger.Debug("Trying to add service from {ConfigurationSectionName}", configurationSectionName);
        try
        {
            var configurationSection = configurationRoot.GetSection(configurationSectionName);
            if (ServiceConfiguration.TryCreate(configurationSection, configurationRoot, out var serviceConfiguration))
            {
                var service = new ServiceFactory().Create(serviceConfiguration);
                services.Add(service);
                _logger.Debug("Successfully added service from {ConfigurationSectionName}", configurationSectionName);
            }
        }
        catch (Exception e)
        {
            _logger.Fatal(e, "Unable to add service from {ConfigurationSectionName}", configurationSectionName);
            throw;
        }
    }
}
