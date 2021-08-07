// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class GraphContextOptionsUseGraphContextProfiling
    {
        public static TFunctionalOptions UseGraphContextProfiling<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            options.Use(new IGraphContextExtension[]
            {
                new ProfilingGraphContextExtension(),
            });

            return options;
        }
    }
}
