// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using Microsoft.Extensions.Configuration;

    public static class StorageConfigurationDiagnosticsExtension
    {
        public static IStorageConfiguration UseStorageDiagnostics(this IStorageConfiguration configuration, IConfiguration configurationRoot)
        {
            var extensions = new IStorageExtension[]
            {
                new DiagnosticsStorageExtension(configurationRoot),
            };
            return configuration.Use(extensions);
        }
    }
}
