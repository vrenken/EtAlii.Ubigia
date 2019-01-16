namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class LinqQueryContextConfigurationDiagnosticsExtension 
    {
        public static ILinqQueryContextConfiguration Use(this ILinqQueryContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new ILinqQueryContextExtension[]
            {
                new DiagnosticsLinqQueryContextExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}