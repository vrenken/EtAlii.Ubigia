// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class HostOptionsUseHostDiagnosticsExtension
    {
        public static HostOptions UseHostDiagnostics(this HostOptions options)
        {
            var extensions = new IExtension[] { new DiagnosticsHostExtension(options.ConfigurationRoot) };
            return options.Use(extensions);
        }
    }
}
