// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class GraphContextOptionsUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Lapa GCL parsing to the configuration.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseLapaContextParser<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            options.Use(new IGraphContextExtension[]
            {
                new LapaGraphContextExtension(options),
            });

            return options;
        }
    }
}
