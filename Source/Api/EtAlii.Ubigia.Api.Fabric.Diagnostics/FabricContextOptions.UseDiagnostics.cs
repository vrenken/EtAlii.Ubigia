// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class FabricOptionsDiagnosticsExtension
    {
        public static FabricContextOptions UseDiagnostics(this FabricContextOptions options)
        {
            var extensions = new IExtension[]
            {
                new LoggingFabricContextExtension(options.ConfigurationRoot),
            };
            return options.Use(extensions);
        }
    }
}
