// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;
    using Microsoft.Extensions.Configuration;

    public static class LogicalContextDiagnosticsExtension
    {
        public static TLogicalContextConfiguration UseLogicalDiagnostics<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, IConfigurationRoot configurationRoot, bool alsoUseForDeeperDiagnostics = true)
            where TLogicalContextConfiguration : LogicalContextConfiguration
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(configurationRoot),
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFabricDiagnostics(configurationRoot);
            }

            return configuration;
        }
    }
}
