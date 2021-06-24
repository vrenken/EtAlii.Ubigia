// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Persistence
{
    using EtAlii.xTechnology.Diagnostics;

    public static class StorageConfigurationDiagnosticsExtension
    {
        public static IStorageConfiguration Use(this IStorageConfiguration configuration, IDiagnosticsConfiguration diagnostics)
        {
            var extensions = new IStorageExtension[]
            {
                new DiagnosticsStorageExtension(diagnostics),
            };
            return configuration.Use(extensions);
        }
    }
}
