namespace EtAlii.Servus.Infrastructure.Hosting.WindowsServiceHost
{

    public static class IHostConfigurationWindowsServiceHostExtension
    {
        public static IHostConfiguration UseWindowsServiceHost(this IHostConfiguration configuration)
        {
            var extensions = new IHostExtension[]
            {
//                new WindowsServiceHostExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}