// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    public static class FabricContextOptionsDiagnosticsExtension
    {
        public static TFabricContextOptions UseFabricDiagnostics<TFabricContextOptions>(this TFabricContextOptions options)
            where TFabricContextOptions : IFabricContextOptions
        {
            var extensions = new IExtension[]
            {
                new LoggingFabricContextExtension(),
            };
            return options.Use(extensions);
        }
    }
}
