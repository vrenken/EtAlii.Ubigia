// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.xTechnology.MicroContainer;

    public static class TraversalContextOptionsUseTraversalProfiling
    {
        public static FunctionalOptions UseTraversalProfiling(this FunctionalOptions options)
        {
            options.Use(new IExtension[]
            {
                new ProfilingPathParserExtension(),
                new ProfilingTraversalContextExtension(),
            });

            return options;
        }
    }
}
