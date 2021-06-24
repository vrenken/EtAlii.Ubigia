// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public class HostConfigurationBuilder
    {
        public IHostConfiguration Build(IConfigurationRoot applicationConfiguration, ConfigurationDetails details)
		{
            var hostSection = applicationConfiguration.GetSection("Host");

            var systemConfigurations = hostSection.GetAllSections("Systems");

            // Create a host instance.
            var configuration = new HostConfiguration()
                .Use(systemConfigurations)
                .Use(details);

            return configuration;
        }
    }
}
