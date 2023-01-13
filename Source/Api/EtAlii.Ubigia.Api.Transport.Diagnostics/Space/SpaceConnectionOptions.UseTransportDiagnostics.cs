// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Transport.Diagnostics;

using EtAlii.xTechnology.MicroContainer;

public static class SpaceConnectionOptionsDiagnosticsExtension
{
    public static SpaceConnectionOptions UseTransportDiagnostics(this SpaceConnectionOptions options)
    {
        var extensions = new IExtension[]
        {
            new DiagnosticsSpaceConnectionExtension(options.ConfigurationRoot),
        };
        return options.Use(extensions);
    }
}
