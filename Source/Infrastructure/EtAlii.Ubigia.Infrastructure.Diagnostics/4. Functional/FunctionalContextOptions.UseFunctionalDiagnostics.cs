// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Infrastructure.Diagnostics;

using EtAlii.xTechnology.MicroContainer;
using EtAlii.Ubigia.Infrastructure.Functional;

public static class FunctionalContextOptionsUseFunctionalDiagnostics
{
    public static FunctionalContextOptions UseFunctionalDiagnostics(this FunctionalContextOptions options)
    {
        var extensions = new IExtension[]
        {
            new FunctionalContextDiagnosticsExtension(options.ConfigurationRoot),
        };

        return options.Use(extensions);
    }
}
