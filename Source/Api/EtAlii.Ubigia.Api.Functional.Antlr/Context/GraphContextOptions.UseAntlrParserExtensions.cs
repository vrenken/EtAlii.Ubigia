// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;

    public static class GraphContextOptionsUseAntlrParserExtensions
    {
        /// <summary>
        /// Add Antlr GCL parsing to the options.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseAntlrContextParser<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            options.Use(new IGraphContextExtension[]
            {
                new AntlrGraphContextExtension(options),
            });

            return options;
        }
    }
}
