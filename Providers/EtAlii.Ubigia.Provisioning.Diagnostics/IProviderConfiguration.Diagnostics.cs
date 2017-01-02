namespace EtAlii.Ubigia.Provisioning
{
    using EtAlii.xTechnology.Diagnostics;

    public static class IProviderConfigurationDiagnosticsExtension
    {
        public static IProviderConfiguration Use(this IProviderConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IProviderExtension[]
            {
                new DiagnosticsProviderExtension(diagnostics), 
            };
            return configuration.Use(extensions);
        }
    }
}