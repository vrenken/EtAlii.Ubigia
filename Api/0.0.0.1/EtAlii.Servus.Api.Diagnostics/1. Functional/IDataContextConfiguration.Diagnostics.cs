namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IDataContextConfigurationDiagnosticsExtension 
    {
        public static IDataContextConfiguration Use(this IDataContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IDataContextExtension[]
            {
                new DiagnosticsDataContextExtension(diagnostics)
            };
            return configuration.Use(extensions);
        }
    }
}