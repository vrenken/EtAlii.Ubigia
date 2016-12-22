namespace EtAlii.Servus.Storage.Ntfs
{
    public static class IStorageConfigurationNtfsExtension
    {
        public static IStorageConfiguration UseNtfsStorage(this IStorageConfiguration configuration, string baseFolder)
        {
            var extensions = new IStorageExtension[]
            {
                new NtfsStorageExtension(baseFolder),
            };
            return configuration.Use(extensions);
        }
    }
}
