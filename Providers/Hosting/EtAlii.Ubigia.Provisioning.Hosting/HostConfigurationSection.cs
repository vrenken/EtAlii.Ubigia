namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.Configuration;
    using EtAlii.xTechnology.Hosting;

    public class HostConfigurationSection : ConfigurationSection, IHostConfigurationSection
    {
        public IHostConfiguration ToHostConfiguration()
        {
            var configuration = new HostConfiguration();
            return configuration;
        }
    }
}
