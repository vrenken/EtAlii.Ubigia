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
        /// <typeparam name="TTraversalContextOptions"></typeparam>
        /// <returns></returns>
        public static TTraversalContextOptions UseTestTraversalParser<TTraversalContextOptions>(this TTraversalContextOptions options)
            where TTraversalContextOptions : FunctionalContextOptions
        {
#if USE_LAPA_PARSER_IN_TESTS
            return options.UseLapaTraversalParser();
#else
            return options.UseAntlrTraversalParser();
#endif
        }

    }
}
