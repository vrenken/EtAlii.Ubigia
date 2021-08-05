// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.NetCoreApp
{
    public static class StorageOptionsUseNetCoreApp
    {
        public static IStorageOptions UseNetCoreAppStorage(this IStorageOptions options, string baseFolder)
        {
            var extensions = new IStorageExtension[]
            {
                new NetCoreAppStorageExtension(baseFolder),
            };
            return options.Use(extensions);
        }
    }
}
