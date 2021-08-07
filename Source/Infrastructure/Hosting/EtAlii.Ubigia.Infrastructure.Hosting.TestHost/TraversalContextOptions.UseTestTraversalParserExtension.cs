// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    /// <summary>
    /// Add the configured test GTL parsing to the options.
    /// </summary>
    public static class TraversalContextOptionsUseTestParserExtension
    {
        /// <summary>
        /// Use the text parser configured for testing.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseTestTraversalParser<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
#if USE_LAPA_PARSER_IN_TESTS
            return options.UseLapaTraversalParser();
#else
            return options.UseAntlrTraversalParser();
#endif
        }

    }
}
