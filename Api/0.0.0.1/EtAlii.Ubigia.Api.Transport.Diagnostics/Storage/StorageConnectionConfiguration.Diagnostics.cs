namespace EtAlii.Ubigia.Api.Transport.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class StorageConnectionConfigurationDiagnosticsExtension
    {
        public static IStorageConnectionConfiguration Use(this IStorageConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IStorageConnectionExtension[]
            {
                new DiagnosticsStorageConnectionExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}