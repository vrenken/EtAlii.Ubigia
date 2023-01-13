// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Ntfs;

using EtAlii.xTechnology.MicroContainer;

public static class StorageOptionsNtfsExtensionExtension
{
    public static IStorageOptions UseNtfsStorage(this IStorageOptions options, string baseFolder)
    {
        var extensions = new IExtension[]
        {
            new NtfsStorageExtension(baseFolder),
        };
        return options.Use(extensions);
    }
}
