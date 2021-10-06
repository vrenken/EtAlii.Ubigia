// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.MicroContainer;

    public static class HostOptionsUseInProcessHostExtension
    {
        public static HostOptions UseInProcessHost(this HostOptions options, HostControl hostControl)
        {
            var extensions = new IExtension[]
            {
                new InProcessHostExtension(options, hostControl),
            };
            return options
                .Use(extensions)
                .UseHost(o => new InProcessHost(o))
                .UseWrapper(true);
        }
    }
}
