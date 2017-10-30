namespace EtAlii.xTechnology.Hosting
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IHostConfigurationDiagnosticsExtension
    {
        public static IHostConfiguration Use(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            return configuration.Use(new DiagnosticsHostExtension(diagnostics));
        }
    }
}