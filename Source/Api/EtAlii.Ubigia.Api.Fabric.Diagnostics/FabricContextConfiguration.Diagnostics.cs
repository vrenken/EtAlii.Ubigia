// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.Diagnostics;

    public static class FabricContextConfigurationDiagnosticsExtension
    {
        public static TFabricContextConfiguration UseFabricDiagnostics<TFabricContextConfiguration>(this TFabricContextConfiguration configuration, IDiagnosticsConfiguration diagnostics)
            where TFabricContextConfiguration : IFabricContextConfiguration
        {
            var extensions = new IExtension[]
            {
                new FabricContextDiagnosticsExtension(diagnostics),
            };
            return configuration.Use(extensions);
        }
    }
}
