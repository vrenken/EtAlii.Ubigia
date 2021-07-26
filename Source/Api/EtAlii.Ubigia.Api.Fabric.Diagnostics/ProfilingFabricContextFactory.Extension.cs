// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using Microsoft.Extensions.Configuration;

    public static class FabricContextConfigurationUseDiagnostics
    {
        public static IProfilingFabricContext CreateForProfiling(this FabricContextFactory fabricContextFactory, FabricContextConfiguration configuration, IConfigurationRoot configurationRoot)
        {
            configuration.Use(new IFabricContextExtension[]
            {
                new LoggingFabricContextExtension(configurationRoot),
                new ProfilingFabricContextExtension(configurationRoot),
            });

            return (IProfilingFabricContext)fabricContextFactory.Create(configuration);
        }
    }
}
