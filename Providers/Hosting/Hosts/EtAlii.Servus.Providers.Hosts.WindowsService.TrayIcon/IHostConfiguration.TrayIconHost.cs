namespace EtAlii.Servus.Provisioning.Hosting
{
    public static class IHostConfigurationTrayIconProviderExtension
    {
        public static IHostConfiguration UseTrayIconHost(this IHostConfiguration configuration)
        {
            var extensions = new IHostExtension[]
            {
                new TrayIconHostExtension(), 
            };
            return configuration.Use(extensions);
        }
    }
}