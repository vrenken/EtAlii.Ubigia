﻿namespace EtAlii.Ubigia.Persistence.Ntfs
{
    public static class StorageConfigurationNtfsExtension
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
