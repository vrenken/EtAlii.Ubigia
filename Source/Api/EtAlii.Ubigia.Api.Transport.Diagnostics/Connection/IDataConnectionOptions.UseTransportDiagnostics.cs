// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    public static class DataConnectionOptionsDiagnosticsExtension
    {
        public static DataConnectionOptions UseTransportDiagnostics(this DataConnectionOptions options)
        {
            var extensions = new IDataConnectionExtension[]
            {
                new DiagnosticsDataConnectionExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
