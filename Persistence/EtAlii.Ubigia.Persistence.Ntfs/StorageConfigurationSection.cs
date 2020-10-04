﻿namespace EtAlii.Ubigia.Persistence.Ntfs
{
    using System.Configuration;

    public class StorageConfigurationSection : ConfigurationSection, IStorageConfigurationSection
    {
        [ConfigurationProperty("name", IsRequired = false)]
        public string Name { get => this["name"] as string; set => this["name"] = value; }

        [ConfigurationProperty("baseFolder", IsRequired = false)]
        public string BaseFolder { get => this["baseFolder"] as string; set => this["baseFolder"] = value; }

        public IStorageConfiguration ToStorageConfiguration()
        {
            var configuration = new StorageConfiguration()
                .UseNtfsStorage(BaseFolder)
                .Use(Name);

            return configuration;
        }
    }
}