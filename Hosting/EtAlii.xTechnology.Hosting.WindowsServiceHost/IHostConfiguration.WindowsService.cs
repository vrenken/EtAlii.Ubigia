namespace EtAlii.xTechnology.Hosting
{

    public static class IHostConfigurationWindowsServiceHostExtension
    {
        public static IHostConfiguration UseWindowsServiceHost(
            this IHostConfiguration configuration,
            string shortServiceNameFormat, // Something like: "UIS${0}" Ubigia Infrastructure Service
            string displayNameFormat, // example: "Ubigia Infrastructure Service ({0})"
            string descriptionFormat // e.g. "Provides applications access to the Ubigia storage '{0}'"
            )
        {
            var extensions = new IHostExtension[]
            {
//                new WindowsServiceHostExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}