// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    public static class FabricContextConfigurationDiagnosticsExtension
    {
        public static TFabricContextConfiguration UseFabricDiagnostics<TFabricContextConfiguration>(this TFabricContextConfiguration configuration)
            where TFabricContextConfiguration : IFabricContextConfiguration
        {
            var extensions = new IExtension[]
            {
                new LoggingFabricContextExtension(),
            };
            return configuration.Use(extensions);
        }
    }
}
