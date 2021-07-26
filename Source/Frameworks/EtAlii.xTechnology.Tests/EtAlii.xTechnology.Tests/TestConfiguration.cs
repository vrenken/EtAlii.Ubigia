namespace EtAlii.Ubigia.Tests
{
    using Microsoft.Extensions.Configuration;

    public static class TestConfiguration
    {
        public static IConfigurationRoot Root { get; private set; }

        public static void Initialize(IConfigurationRoot configurationRoot) => Root = configurationRoot;
    }
}
