// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System.Reflection;
    using EtAlii.xTechnology.MicroContainer;

    public static class HostOptionsUseConsoleHostExtension
    {
        public static HostOptions UseDockerHost(this HostOptions options)
        {
            return options
                .Use(new IExtension[] { new DockerHostExtension() })
                .UseEntryAssembly(Assembly.GetCallingAssembly())
                .UseHost(o => new DockerHost(o))
                .UseWrapper(true);
        }
    }
}
