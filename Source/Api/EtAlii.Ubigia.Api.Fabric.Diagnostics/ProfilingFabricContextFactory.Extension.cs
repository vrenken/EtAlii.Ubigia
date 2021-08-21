// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class FabricContextConfigurationUseDiagnostics
    {
        public static IProfilingFabricContext CreateForProfiling(this FabricContextFactory fabricContextFactory, FabricContextOptions options)
        {
            options.Use(new IExtension[]
            {
                new LoggingFabricContextExtension(options.ConfigurationRoot),
                new ProfilingFabricContextExtension(options.ConfigurationRoot),
            });

            return (IProfilingFabricContext)fabricContextFactory.Create(options);
        }
    }
}
