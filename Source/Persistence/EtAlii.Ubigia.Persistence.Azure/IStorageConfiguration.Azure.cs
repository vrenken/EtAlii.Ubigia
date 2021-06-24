// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure
{
    public static class StorageConfigurationAzureExtension
    {
        public static IStorageConfiguration UseAzureStorage(this IStorageConfiguration configuration)
        {
            var extensions = new IStorageExtension[]
            {
                new AzureStorageExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}
