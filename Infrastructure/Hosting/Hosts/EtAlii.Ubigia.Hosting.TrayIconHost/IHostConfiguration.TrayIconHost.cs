namespace EtAlii.Ubigia.Infrastructure.Hosting
{

    public static class IHostConfigurationTrayIconHostExtension
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