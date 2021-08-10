// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence.Portable
{
    using EtAlii.xTechnology.MicroContainer;
    using PCLStorage;

    public static class StorageOptionsPortableExtensionExtension
    {
        public static IStorageOptions UsePortableStorage(this IStorageOptions options, IFolder localStorage)
        {
            var extensions = new IExtension[]
            {
                new PortableStorageExtension(localStorage),
            };
            return options.Use(extensions);
        }
    }
}
