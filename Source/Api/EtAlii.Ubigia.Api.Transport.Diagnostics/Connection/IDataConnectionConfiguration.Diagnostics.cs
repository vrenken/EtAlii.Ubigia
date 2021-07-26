// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class DataConnectionConfigurationDiagnosticsExtension
    {
        public static DataConnectionConfiguration UseTransportDiagnostics(this DataConnectionConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            var extensions = new IDataConnectionExtension[]
            {
                new DiagnosticsDataConnectionExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
