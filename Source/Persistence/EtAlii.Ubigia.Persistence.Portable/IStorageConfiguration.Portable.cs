// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

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
