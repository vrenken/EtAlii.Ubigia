namespace EtAlii.Ubigia.Persistence.InMemory
{
    public static class StorageConfigurationInMemoryExtension
    {
        public static IStorageConfiguration UseInMemoryStorage(this IStorageConfiguration configuration)
        {
            var extensions = new IStorageExtension[]
            {
                new InMemoryStorageExtension(),
            };
            return configuration
                .Use(extensions)
                .Use<InMemoryStorage>();
        }
    }
}
