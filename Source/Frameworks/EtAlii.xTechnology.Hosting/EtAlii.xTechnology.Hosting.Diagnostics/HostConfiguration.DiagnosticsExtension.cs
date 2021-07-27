// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class HostConfigurationDiagnosticsExtension
    {
        public static IHostConfiguration UseHostDiagnostics(this IHostConfiguration configuration, IConfiguration configurationRoot)
        {
            return configuration.Use(new DiagnosticsHostExtension(configurationRoot));
        }
    }
}
