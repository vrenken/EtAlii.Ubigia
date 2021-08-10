// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class FabricContextOptionsDiagnosticsExtension
    {
        public static TFabricContextOptions UseFabricDiagnostics<TFabricContextOptions>(this TFabricContextOptions options)
            where TFabricContextOptions : IFabricContextOptions
        {
            var extensions = new IExtension[]
            {
                new LoggingFabricContextExtension(options.ConfigurationRoot),
            };
            return options.Use(extensions);
        }
    }
}
