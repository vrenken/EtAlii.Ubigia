// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    public static class ManagementConnectionOptionsDiagnosticsExtension
    {
        public static ManagementConnectionOptions UseTransportManagementDiagnostics(this ManagementConnectionOptions options)
        {
            var extensions = new IManagementConnectionExtension[]
            {
                new DiagnosticsManagementConnectionExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
