// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalContextOptionsUseTraversalProfiling
    {
        public static TTraversalContextOptions UseTraversalProfiling<TTraversalContextOptions>(this TTraversalContextOptions options)
            where TTraversalContextOptions : FunctionalContextOptions
        {
            options.Use(new ITraversalContextExtension[]
            {
                new ProfilingTraversalContextExtension(),
            });

            return options;
        }
    }
}
