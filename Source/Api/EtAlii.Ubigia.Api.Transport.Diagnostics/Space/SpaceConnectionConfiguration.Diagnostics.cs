// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class SpaceConnectionConfigurationDiagnosticsExtension
    {
        public static SpaceConnectionConfiguration UseTransportDiagnostics(this SpaceConnectionConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            var extensions = new ISpaceConnectionExtension[]
            {
                new DiagnosticsSpaceConnectionExtension(configurationRoot),
            };
            return configuration.Use(extensions);
        }
    }
}
