namespace EtAlii.Ubigia.Provisioning.Hosting
{
    using System.Configuration;

    public class WindowsServiceConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name => this["name"] as string;
    }
}