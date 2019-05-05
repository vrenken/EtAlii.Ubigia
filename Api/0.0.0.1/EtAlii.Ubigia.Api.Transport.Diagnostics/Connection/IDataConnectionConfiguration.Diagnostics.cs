namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class DataConnectionConfigurationDiagnosticsExtension
    {
        public static IDataConnectionConfiguration Use(this IDataConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IDataConnectionExtension[]
            {
                new DiagnosticsDataConnectionExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}