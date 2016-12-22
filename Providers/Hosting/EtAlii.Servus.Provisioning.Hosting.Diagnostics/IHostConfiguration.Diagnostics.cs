namespace EtAlii.Servus.Provisioning.Hosting
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Management;
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;

    public static class IHostConfigurationDiagnosticsExtension
    {
        public static IHostConfiguration Use(this IHostConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IHostExtension[]
            {
                new DiagnosticsProviderHostExtension(diagnostics), 
            };
            return configuration
                .Use(extensions)
                .Use((IDataConnectionConfiguration c) => c.Use(diagnostics))
                .Use((IManagementConnectionConfiguration c) => c.Use(diagnostics))
                .Use((IDataContextConfiguration c) => c.Use(diagnostics));
        }
    }
}