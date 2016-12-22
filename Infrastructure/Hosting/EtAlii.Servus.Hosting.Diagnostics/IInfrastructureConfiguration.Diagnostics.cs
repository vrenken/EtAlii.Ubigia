namespace EtAlii.Servus.Infrastructure.Hosting
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IInfrastructureConfigurationDiagnosticsExtension
    {
        public static IHostConfiguration Use(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IHostExtension[]
            {
                new DiagnosticsHostExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}