// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using Microsoft.Extensions.Configuration;
    using IConfiguration = EtAlii.Ubigia.IConfiguration;

    public static class FabricContextConfigurationUseDiagnostics
    {
        public static TFabricContextConfiguration UseFabricDiagnostics<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, IConfigurationRoot configurationRoot)
            where TFabricContextConfiguration : IConfiguration
        {
            var extensions = new IExtension[]
            {
                new FabricContextDiagnosticsExtension(configurationRoot),
            };

            return configuration.Use(extensions);
        }
    }
}
