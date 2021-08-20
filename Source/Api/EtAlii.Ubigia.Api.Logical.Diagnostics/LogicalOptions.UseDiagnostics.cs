// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;

    public static class LogicalOptionsUseDiagnosticsExtension
    {
        public static TLogicalOptions UseLogicalDiagnostics<TLogicalOptions>(this TLogicalOptions options, bool alsoUseForDeeperDiagnostics = true)
            where TLogicalOptions : LogicalOptions
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
