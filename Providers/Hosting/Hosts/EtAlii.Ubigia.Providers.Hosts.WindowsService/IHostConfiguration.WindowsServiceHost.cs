namespace EtAlii.Ubigia.Provisioning.Hosting
{
    public static class IHostConfigurationWindowsServiceProviderExtension
    {
        public static IHostConfiguration UseWindowsServiceHost(this IHostConfiguration configuration)
        {
            var extensions = new IHostExtension[]
            {
                new WindowsServiceHostExtension(), 
            };
            return configuration.Use(extensions);
        }
    }
}