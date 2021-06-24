// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting
{
    using System;

    public static class HostConfigurationConsoleHostExtension
    {
        public static IHostConfiguration UseConsoleHost(this IHostConfiguration configuration)
        {
            var extensions = Array.Empty<IHostExtension>();
            //var extensions = new IHostExtension[]
            //[
            //    new ConsoleHostExtension(),
            //]
            return configuration.Use(extensions);
        }
    }
}
