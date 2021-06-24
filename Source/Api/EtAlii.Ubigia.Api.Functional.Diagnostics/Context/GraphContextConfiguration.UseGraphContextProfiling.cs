// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class GraphContextConfigurationUseGraphContextProfiling
    {
        public static TGraphContextConfiguration UseGraphContextProfiling<TGraphContextConfiguration>(this TGraphContextConfiguration configuration)
            where TGraphContextConfiguration : FunctionalContextConfiguration
        {
            configuration.Use(new IGraphContextExtension[]
            {
                new ProfilingGraphContextExtension(),
            });

            return configuration;
        }
    }
}
