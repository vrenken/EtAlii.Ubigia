namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.xTechnology.Diagnostics;

    public static class ILogicalContextDiagnosticsExtension
    {
        public static ILogicalContextConfiguration Use(this ILogicalContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ILogicalContextExtension[]
            {
                new DiagnosticsLogicalContextExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}