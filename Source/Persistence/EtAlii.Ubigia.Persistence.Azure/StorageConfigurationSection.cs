namespace EtAlii.Ubigia.Persistence.Azure
{
    using System.Configuration;

    public class StorageConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name { get => this["name"] as string; set => this["name"] = value; }

        public IStorageConfiguration ToStorageConfiguration()
        {
            var configuration = new StorageConfiguration()
                .UseAzureStorage()
                .Use(Name);
            return configuration;
        }
    }
}