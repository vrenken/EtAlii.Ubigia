namespace EtAlii.Ubigia.Persistence.Azure
{
    public static class StorageConfigurationAzureExtension
    {
        public static IStorageConfiguration UseAzureStorage(this IStorageConfiguration configuration)
        {
            var extensions = new IStorageExtension[]
            {
                new AzureStorageExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}
