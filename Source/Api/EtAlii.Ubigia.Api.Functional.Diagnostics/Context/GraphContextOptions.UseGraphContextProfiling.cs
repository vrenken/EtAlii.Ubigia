// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    using Microsoft.Extensions.Configuration;

    public static class GraphContextOptionsUseGraphContextProfiling
    {
        public static TGraphContextOptions UseGraphContextProfiling<TGraphContextOptions>(this TGraphContextOptions options, IConfiguration configurationRoot)
            where TGraphContextOptions : FunctionalContextOptions
        {
            options.Use(new IGraphContextExtension[]
            {
                new ProfilingGraphContextExtension(configurationRoot),
            });

            return options;
        }
    }
}
