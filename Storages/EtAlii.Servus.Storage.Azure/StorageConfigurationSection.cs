namespace EtAlii.Servus.Storage.Azure
{
    using System.Configuration;
    using EtAlii.Servus.Api;
    using EtAlii.Servus.Storage;

    public class StorageConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }

        public IStorageConfiguration ToStorageConfiguration()
        {
            var configuration = new StorageConfiguration()
                .UseAzureStorage()
                .Use(Name);
            return configuration;
        }

        //public IStorage Create()
        //{
        //    var configuration = new StorageConfiguration()
        //        .Use(Name)
        //        .UseInMemoryStorage();
        //    return new StorageFactory<DefaultStorage>().Create(configuration);
        //}

        //public IStorage Create(IStorageConfiguration configuration)
        //{
        //    return new StorageFactory<DefaultStorage>().Create(configuration);
        //}
    }
}