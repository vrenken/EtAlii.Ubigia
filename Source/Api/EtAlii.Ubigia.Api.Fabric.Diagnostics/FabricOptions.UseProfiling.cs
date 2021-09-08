// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.xTechnology.MicroContainer;

    public static class FabricOptionsUseProfilingExtension
    {
        public static FabricOptions UseProfiling(this FabricOptions options)
        {
            options.Use(new IExtension[]
            {
                new LoggingFabricContextExtension(options.ConfigurationRoot),
                new ProfilingFabricContextExtension(options.ConfigurationRoot),
            });

            return options;//(IProfilingFabricContext)fabricContextFactory.Create(options)
        }
    }
}
