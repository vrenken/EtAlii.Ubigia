namespace EtAlii.xTechnology.Hosting
{
    public static class HostConfigurationConsoleHostExtension
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