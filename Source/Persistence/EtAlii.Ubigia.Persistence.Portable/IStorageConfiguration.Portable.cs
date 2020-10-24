namespace EtAlii.Ubigia.Persistence.Portable
{
    using PCLStorage;

    public static class StorageConfigurationPortableExtension
    {
        public static IStorageConfiguration UsePortableStorage(this IStorageConfiguration configuration, IFolder localStorage)
        {
            var extensions = new IStorageExtension[]
            {
                new PortableStorageExtension(localStorage),
            };
            return configuration.Use(extensions);
        }
    }
}
