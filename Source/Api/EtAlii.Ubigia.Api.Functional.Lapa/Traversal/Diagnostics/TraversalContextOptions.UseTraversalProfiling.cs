// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalContextOptionsUseTraversalProfiling
    {
        public static TFunctionalOptions UseTraversalProfiling<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : IFunctionalOptions
        {
            options.Use(new ITraversalContextExtension[]
            {
                new ProfilingTraversalContextExtension(),
            });

            return options;
        }
    }
}
