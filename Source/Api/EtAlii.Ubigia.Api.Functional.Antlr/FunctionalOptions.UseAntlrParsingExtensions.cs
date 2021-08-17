// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia.Api.Functional.Antlr
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Context;
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    public static class GraphContextOptionsUseAntlrParsingExtension
    {
        /// <summary>
        /// Add Antlr GCL/GTL parsing to the options.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseAntlrParsing<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
            options.Use(new IFunctionalExtension[]
            {
                new AntlrGraphContextExtension(options),
                new AntrlParserExtension()
            });

            return options;
        }
    }
}
