namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using System.Linq;
    using EtAlii.xTechnology.Diagnostics;

    public static class ManagementConnectionConfigurationDiagnosticsExtension
    {
        public static IManagementConnectionConfiguration Use(this IManagementConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IManagementConnectionExtension[]
            {
                new DiagnosticsManagementConnectionExtension(diagnostics), 
            }.Cast<IExtension>().ToArray();
            
            return configuration.Use(extensions);
        }
    }
}