namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    public static class StorageConfigurationNetCoreAppExtension
    {
        public static IStorageConfiguration UseNetCoreAppStorage(this IStorageConfiguration configuration, string baseFolder)
        {
            var extensions = new IStorageExtension[]
            {
                new NetCoreAppStorageExtension(baseFolder),
            };
            return configuration.Use(extensions);
        }
    }
}