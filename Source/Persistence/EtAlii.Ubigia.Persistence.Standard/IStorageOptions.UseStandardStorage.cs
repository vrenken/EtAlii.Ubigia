// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Standard;

using EtAlii.xTechnology.MicroContainer;

public static class StorageOptionsUseStandardStorage
{
    public static IStorageOptions UseStandardStorage(this IStorageOptions options, string baseFolder)
    {
        var extensions = new IExtension[]
        {
            new StandardStorageExtension(baseFolder),
        };
        return options.Use(extensions);
    }
}
