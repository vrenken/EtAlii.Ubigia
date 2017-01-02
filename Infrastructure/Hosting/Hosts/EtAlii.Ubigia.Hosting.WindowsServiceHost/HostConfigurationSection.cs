namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Configuration;
    using EtAlii.Ubigia.Infrastructure.Hosting.WindowsServiceHost;

    public class HostConfigurationSection : ConfigurationSection, IHostConfigurationSection
    {
        public IHostConfiguration ToHostConfiguration()
        {
            var configuration = new HostConfiguration()
                .UseWindowsServiceHost();
            return configuration;
        }
    }
}
