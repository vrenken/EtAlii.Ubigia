namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class ILogicalContextDiagnosticsExtension
    {
        public static TLogicalContextConfiguration UseLogicalDiagnostics<TLogicalContextConfiguration>(this TLogicalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TLogicalContextConfiguration : LogicalContextConfiguration
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}