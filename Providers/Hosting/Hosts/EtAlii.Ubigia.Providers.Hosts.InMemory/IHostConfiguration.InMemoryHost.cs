namespace EtAlii.Ubigia.Provisioning.Hosting
{

    public static class IHostConfigurationInMemoryProviderExtension
    {
        public static IHostConfiguration UseInMemoryHost(this IHostConfiguration configuration)
        {
            var extensions = new IHostExtension[]
            {
                new InMemoryHostExtension(), 
            };
            return configuration.Use(extensions);
        }
    }
}