// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    using EtAlii.Ubigia.Api.Functional.Antlr;

    public static class GraphContextOptionsUseTestParserExtension
    {
        /// <summary>
        /// Add the configured test GCL parsing to the options.
        /// </summary>
        public static TFunctionalOptions UseTestContextParser<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
#if USE_LAPA_PARSER_IN_TESTS
            return options.UseLapaParser();
#else
            return options.UseAntlrParser();
#endif
        }

    }
}
