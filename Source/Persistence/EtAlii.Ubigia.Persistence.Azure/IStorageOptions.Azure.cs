// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Azure;

using EtAlii.xTechnology.MicroContainer;

public static class StorageOptionsUseAzureStorageExtension
{
    public static IStorageOptions UseAzureStorage(this IStorageOptions options)
    {
        var extensions = new IExtension[]
        {
            new AzureStorageExtension(),
        };
        return options.Use(extensions);
    }
}
