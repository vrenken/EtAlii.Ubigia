// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    public static class StorageOptionsUseStorageDiagnosticsExtension
    {
        public static IStorageOptions UseStorageDiagnostics(this IStorageOptions options)
        {
            var extensions = new IStorageExtension[]
            {
                new DiagnosticsStorageExtension(options.ConfigurationRoot),
            };
            return options.Use(extensions);
        }
    }
}
