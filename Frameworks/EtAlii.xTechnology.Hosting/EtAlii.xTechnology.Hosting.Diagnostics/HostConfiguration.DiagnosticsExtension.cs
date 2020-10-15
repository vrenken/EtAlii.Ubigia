namespace EtAlii.xTechnology.Hosting.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class HostConfigurationDiagnosticsExtension
    {
        public static IHostConfiguration Use(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            return configuration.Use(new DiagnosticsHostExtension(diagnostics));
        }
    }
}