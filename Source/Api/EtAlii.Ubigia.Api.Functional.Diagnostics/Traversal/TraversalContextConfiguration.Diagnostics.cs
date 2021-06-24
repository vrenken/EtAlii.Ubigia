// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Logical.Diagnostics;

    public static class TraversalContextConfigurationDiagnosticsExtension
    {
        public static TTraversalContextConfiguration UseFunctionalTraversalDiagnostics<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TTraversalContextConfiguration : FunctionalContextConfiguration
        {
            var extensions = new ITraversalContextExtension[]
            {
                new DiagnosticsTraversalContextExtension(diagnostics)
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseLogicalDiagnostics(diagnostics);
            }

            return configuration;
        }
    }
}
