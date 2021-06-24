// Copyright (c) Peter Vrenken. All rights reserved. See the license in https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalContextConfigurationUseTraversalProfiling
    {
        public static TTraversalContextConfiguration UseTraversalProfiling<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration)
            where TTraversalContextConfiguration : FunctionalContextConfiguration
        {
            configuration.Use(new ITraversalContextExtension[]
            {
                new ProfilingTraversalContextExtension(),
            });

            return configuration;
        }
    }
}
