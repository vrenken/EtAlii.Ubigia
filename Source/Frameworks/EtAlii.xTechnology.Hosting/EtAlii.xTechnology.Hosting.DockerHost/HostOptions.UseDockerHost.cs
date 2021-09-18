// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    public static class HostOptionsUseConsoleHostExtension
    {
        public static IHostOptions UseDockerHost(this IHostOptions options)
        {
            var extensions = Array.Empty<IHostExtension>();
            //var extensions = new IHostExtension[]
            //[
            //    new ConsoleHostExtension(),
            //]
            return options.Use(extensions);
        }
    }
}
