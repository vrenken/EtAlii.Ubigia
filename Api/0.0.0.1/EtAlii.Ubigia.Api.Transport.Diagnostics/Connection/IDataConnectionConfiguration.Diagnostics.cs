namespace EtAlii.Ubigia.Api.Diagnostics
{
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.Ubigia.Api.Transport.Diagnostics;

    public static class DataConnectionConfigurationDiagnosticsExtension
    {
        public static IDataConnectionConfiguration Use(this IDataConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IDataConnectionExtension[]
            {
                new DiagnosticsDataConnectionExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}