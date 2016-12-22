namespace EtAlii.Servus.Infrastructure.Hosting
{

    public static class IHostConfigurationConsoleHostExtension
    {
        public static IHostConfiguration UseConsoleHost(this IHostConfiguration configuration)
        {
            var extensions = new IHostExtension[]
            {
//                new ConsoleHostExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}