// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    public static class HostOptionsUseHostDiagnosticsExtension
    {
        public static IHostOptions UseHostDiagnostics(this IHostOptions options)
        {
            return options.Use(new DiagnosticsHostExtension(options.ConfigurationRoot));
        }
    }
}
