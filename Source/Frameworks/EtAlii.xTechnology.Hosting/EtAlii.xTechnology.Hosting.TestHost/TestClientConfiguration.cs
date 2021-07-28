namespace EtAlii.xTechnology.Hosting
{
    using Microsoft.Extensions.Configuration;

    public static class TestClientConfiguration
    {
        public static IConfigurationRoot Root { get; private set; }

        public static void Initialize(IConfigurationRoot configurationRoot) => Root = configurationRoot;
    }
}
