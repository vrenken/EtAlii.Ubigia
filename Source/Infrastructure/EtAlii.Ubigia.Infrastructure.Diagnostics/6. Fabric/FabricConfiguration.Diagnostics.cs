// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    public static class FabricContextConfigurationUseDiagnostics
    {
        public static TFabricContextConfiguration UseFabricDiagnostics<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, Microsoft.Extensions.Configuration.IConfiguration configurationRoot)
            where TFabricContextConfiguration : IExtensible
        {
            var extensions = new IExtension[]
            {
                new FabricContextDiagnosticsExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
