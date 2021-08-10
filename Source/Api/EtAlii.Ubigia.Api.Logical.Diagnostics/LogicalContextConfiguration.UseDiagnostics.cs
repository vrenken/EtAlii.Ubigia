// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;

    public static class LogicalContextDiagnosticsExtension
    {
        public static TLogicalContextOptions UseLogicalDiagnostics<TLogicalContextOptions>(this TLogicalContextOptions options, bool alsoUseForDeeperDiagnostics = true)
            where TLogicalContextOptions : LogicalContextOptions
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(options.ConfigurationRoot),
            };

            options = options.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                options = options.UseFabricDiagnostics();
            }

            return options;
        }
    }
}
