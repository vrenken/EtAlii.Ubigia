namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class StorageConnectionConfigurationDiagnosticsExtension
    {
        public static IStorageConnectionConfiguration UseTransportDiagnostics(this IStorageConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IStorageConnectionExtension[]
            {
                new DiagnosticsStorageConnectionExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}