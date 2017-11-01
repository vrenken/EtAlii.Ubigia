namespace EtAlii.xTechnology.Hosting
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