namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ILogicalContextDiagnosticsExtension
    {
        public static TLogicalContextConfiguration UseLogicalDiagnostics<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TLogicalContextConfiguration : LogicalContextConfiguration
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}