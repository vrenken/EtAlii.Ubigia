namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Fabric.Diagnostics;

    public static class LogicalContextDiagnosticsExtension
    {
        public static TLogicalContextConfiguration UseLogicalDiagnostics<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics, bool alsoUseForDeeperDiagnostics = true)
            where TLogicalContextConfiguration : LogicalContextConfiguration
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(diagnostics),
            };

            configuration = configuration.Use(extensions);
            if (alsoUseForDeeperDiagnostics)
            {
                configuration = configuration.UseFabricDiagnostics(diagnostics);
            }

            return configuration;
        }
    }
}
