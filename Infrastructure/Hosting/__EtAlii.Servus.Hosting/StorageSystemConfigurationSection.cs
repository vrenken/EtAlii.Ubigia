namespace EtAlii.Servus.Infrastructure.Hosting
{
    using System.Configuration;
    using EtAlii.Servus.Storage;

    internal class StorageConfigurationSection : ConfigurationSection, IStorageConfiguration
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }
    }
}