namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class DataConnectionConfigurationDiagnosticsExtension
    {
        public static DataConnectionConfiguration UseTransportDiagnostics(this DataConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IDataConnectionExtension[]
            {
                new DiagnosticsDataConnectionExtension(diagnostics), 
            };
            
            return configuration.Use(extensions);
        }
    }
}