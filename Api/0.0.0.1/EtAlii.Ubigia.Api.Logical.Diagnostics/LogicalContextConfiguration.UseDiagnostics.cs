namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class ILogicalContextDiagnosticsExtension
    {
        public static LogicalContextConfiguration UseLogicalDiagnostics(this LogicalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}