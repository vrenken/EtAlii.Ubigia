// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    public static class StorageConnectionOptionsDiagnosticsExtension
    {
        public static IStorageConnectionOptions UseTransportDiagnostics(this IStorageConnectionOptions options)
        {
            var extensions = new IStorageConnectionExtension[]
            {
                new DiagnosticsStorageConnectionExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
