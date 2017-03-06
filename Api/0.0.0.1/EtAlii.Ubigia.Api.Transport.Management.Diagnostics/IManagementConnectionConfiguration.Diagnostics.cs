namespace EtAlii.Ubigia.Api.Transport.Management.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IManagementConnectionConfigurationDiagnosticsExtension
    {
        public static IManagementConnectionConfiguration Use(this IManagementConnectionConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IManagementConnectionExtension[]
            {
                new DiagnosticsManagementConnectionExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}