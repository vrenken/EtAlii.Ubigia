// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;

    public static class LogicalContextDiagnosticsExtension
    {
        public static TLogicalContextConfiguration UseLogicalDiagnostics<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, bool alsoUseForDeeperDiagnostics = true)
            where TLogicalContextConfiguration : LogicalContextConfiguration
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(),
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFabricDiagnostics();
            }

            return configuration;
        }
    }
}
