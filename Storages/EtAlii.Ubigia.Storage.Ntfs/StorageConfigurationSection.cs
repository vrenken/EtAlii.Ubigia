namespace EtAlii.Ubigia.Storage.Ntfs
{
    using System.Configuration;
    using EtAlii.Ubigia.Api;
    using EtAlii.Ubigia.Storage;

    public class StorageConfigurationSection : ConfigurationSection, IStorageConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name
        {
            get { return this["name"] as string; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("baseFolder", IsRequired = false)]
        public string BaseFolder
        {
            get { return this["baseFolder"] as string; }
            set { this["baseFolder"] = value; }
        }

        public IStorageConfiguration ToStorageConfiguration()
        {
            var configuration = new StorageConfiguration()
                .UseNtfsStorage(BaseFolder)
                .Use(Name);

            return configuration;
        }

        //public IStorage Create()
        //{
        //    var configuration = new StorageConfiguration()
        //        .Use(Name)
        //        .UseNtfsStorage(BaseFolder);

        //    return new StorageFactory<DefaultStorage>().Create(configuration);
        //}
        //public IStorage Create(IStorageConfiguration configuration)
        //{

        //    return new StorageFactory<DefaultStorage>().Create(configuration);
        //}
    }
}