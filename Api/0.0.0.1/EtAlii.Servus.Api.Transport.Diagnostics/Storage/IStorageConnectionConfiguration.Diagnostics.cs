namespace EtAlii.Servus.Api.Management
{
    using EtAlii.Servus.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;

    public static class IStorageConnectionConfigurationDiagnosticsExtension
    {
        public static IStorageConnectionConfiguration Use(this IStorageConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IStorageConnectionExtension[]
            {
                new DiagnosticsStorageConnectionExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}