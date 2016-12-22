namespace EtAlii.Servus.Storage.Portable
{
    using PCLStorage;

    public static class IStorageConfigurationPortableExtension
    {
        public static IStorageConfiguration UsePortableStorage(this IStorageConfiguration configuration)
        {
            var localStorage = FileSystem.Current.LocalStorage;

            var extensions = new IStorageExtension[]
            {
                new PortableStorageExtension(localStorage),
            };
            return configuration.Use(extensions);
        }

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
