// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public static class HostConfigurationInProcessHostExtension
    {
        public static IHostConfiguration UseInProcessHost(this IHostConfiguration configuration, HostControl hostControl)
        {
            var extensions = new IHostExtension[]
            {
                new InProcessHostExtension(hostControl),
            };
            return configuration.Use(extensions);
        }
    }
}