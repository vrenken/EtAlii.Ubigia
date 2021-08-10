// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Infrastructure.Fabric.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class FabricContextConfigurationUseDiagnostics
    {
        public static TFabricContextOptions UseFabricDiagnostics<TFabricContextOptions>(this TFabricContextOptions options)
            where TFabricContextOptions : FabricContextOptions
        {
            var extensions = new IExtension[]
            {
                new FabricContextDiagnosticsExtension(options.ConfigurationRoot),
            };

            return options.Use(extensions);
        }
    }
}
