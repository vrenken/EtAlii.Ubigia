namespace EtAlii.xTechnology.Hosting
{
    using System.Configuration;

    public class HostConfigurationSection : ConfigurationSection, IHostConfigurationSection
    {
        public IHostConfiguration ToHostConfiguration()
        {
            var configuration = new HostConfiguration()
                .UseTrayIconHost();
            return configuration;
        }
    }
}
