// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.InMemory
{
    public static class StorageOptionsUseInMemoryStorageExtension
    {
        public static IStorageOptions UseInMemoryStorage(this IStorageOptions options)
        {
            var extensions = new IStorageExtension[]
            {
                new InMemoryStorageExtension(),
            };
            return options
                .Use(extensions)
                .Use<InMemoryStorage>();
        }
    }
}
