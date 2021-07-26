// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class ManagementConnectionConfigurationDiagnosticsExtension
    {
        public static ManagementConnectionConfiguration UseTransportManagementDiagnostics(this ManagementConnectionConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            var extensions = new IManagementConnectionExtension[]
            {
                new DiagnosticsManagementConnectionExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
