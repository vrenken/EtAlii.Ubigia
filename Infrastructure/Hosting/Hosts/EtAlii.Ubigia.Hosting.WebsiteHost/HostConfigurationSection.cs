namespace EtAlii.Ubigia.Infrastructure.Hosting
{
    using System.Configuration;

    public class HostConfigurationSection : ConfigurationSection, IHostConfigurationSection
    {
        public IHostConfiguration ToHostConfiguration()
        {
            var configuration = new HostConfiguration()
                .UseWebsiteHost();
            return configuration;
        }
    }
}
