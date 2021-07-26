namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public class TestClientConfiguration
    {
        public static IConfigurationRoot Root => _root;
        private static IConfigurationRoot _root;

        public static void Initialize(IConfigurationRoot configurationRoot) => _root = configurationRoot;
    }
}
