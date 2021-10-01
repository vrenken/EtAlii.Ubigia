// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public class HostOptionsBuilder
    {
        public IHostOptions Build(IConfigurationRoot configurationRoot, ConfigurationDetails details)
		{
            // Create a host options instance.
            var options = new HostOptions(configurationRoot)
                .Use(details);

            return options;
        }
    }
}
