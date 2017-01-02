namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    public static class IHostConfigurationWebsiteHostExtension
    {
        public static IHostConfiguration UseWebsiteHost(this IHostConfiguration configuration)
        {
            var extensions = new IHostExtension[]
            {
//                new WebsiteHostExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}