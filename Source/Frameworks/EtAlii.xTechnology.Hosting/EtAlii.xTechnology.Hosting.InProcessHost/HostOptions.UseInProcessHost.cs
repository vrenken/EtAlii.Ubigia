// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    public static class HostOptionsUseInProcessHostExtension
    {
        public static IHostOptions UseInProcessHost(this IHostOptions options, HostControl hostControl)
        {
            var extensions = new IHostExtension[]
            {
                new InProcessHostExtension(hostControl),
            };
            return options.Use(extensions);
        }
    }
}
