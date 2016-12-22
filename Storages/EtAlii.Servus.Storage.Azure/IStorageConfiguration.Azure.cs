namespace EtAlii.Servus.Storage.Azure
{
    public static class IStorageConfigurationAzureExtension
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
