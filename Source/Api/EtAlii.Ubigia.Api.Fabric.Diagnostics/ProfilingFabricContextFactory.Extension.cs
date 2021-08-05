// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    public static class FabricContextConfigurationUseDiagnostics
    {
        public static IProfilingFabricContext CreateForProfiling(this FabricContextFactory fabricContextFactory, FabricContextOptions options)
        {
            options.Use(new IFabricContextExtension[]
            {
                new LoggingFabricContextExtension(),
                new ProfilingFabricContextExtension(),
            });

            return (IProfilingFabricContext)fabricContextFactory.Create(options);
        }
    }
}
