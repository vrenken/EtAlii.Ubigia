namespace EtAlii.Servus.Provisioning.Hosting
{
    using System.Configuration;

    public class WindowsServiceConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return this["name"] as string; }
        }
    }
}