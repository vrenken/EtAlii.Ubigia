namespace EtAlii.Ubigia.Storage.InMemory
{
    public static class IStorageConfigurationInMemoryExtension
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
