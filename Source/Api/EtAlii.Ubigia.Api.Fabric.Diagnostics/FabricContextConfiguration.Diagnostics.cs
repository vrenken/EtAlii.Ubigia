// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class FabricContextConfigurationDiagnosticsExtension
    {
        public static TFabricContextConfiguration UseFabricDiagnostics<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, IConfiguration configurationRoot)
            where TFabricContextConfiguration : IFabricContextConfiguration
        {
            var extensions = new IExtension[]
            {
                new LoggingFabricContextExtension(configurationRoot),
            };
            return configuration.Use(extensions);
        }
    }
}
