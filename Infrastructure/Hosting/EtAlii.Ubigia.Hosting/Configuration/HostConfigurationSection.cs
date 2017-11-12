namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
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
