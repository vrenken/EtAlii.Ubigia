// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ManagementConnectionConfigurationDiagnosticsExtension
    {
        public static ManagementConnectionConfiguration UseTransportDiagnostics(this ManagementConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IManagementConnectionExtension[]
            {
                new DiagnosticsManagementConnectionExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}