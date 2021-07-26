// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class StorageConnectionConfigurationDiagnosticsExtension
    {
        public static IStorageConnectionConfiguration UseTransportDiagnostics(this IStorageConnectionConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            var extensions = new IStorageConnectionExtension[]
            {
                new DiagnosticsStorageConnectionExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
