// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class SpaceConnectionConfigurationDiagnosticsExtension
    {
        public static SpaceConnectionConfiguration UseTransportDiagnostics(this SpaceConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ISpaceConnectionExtension[]
            {
                new DiagnosticsSpaceConnectionExtension(diagnostics),
            };
            return configuration.Use(extensions);
        }
    }
}
