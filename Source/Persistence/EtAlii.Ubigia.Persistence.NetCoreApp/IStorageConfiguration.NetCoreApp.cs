// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
