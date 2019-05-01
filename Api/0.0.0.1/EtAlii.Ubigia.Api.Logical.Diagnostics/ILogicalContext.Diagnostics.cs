namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ILogicalContextDiagnosticsExtension
    {
        public static LogicalContextConfiguration Use(this ILogicalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}