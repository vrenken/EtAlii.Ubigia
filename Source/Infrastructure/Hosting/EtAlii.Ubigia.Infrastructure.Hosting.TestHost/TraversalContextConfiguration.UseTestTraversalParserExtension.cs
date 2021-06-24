// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr.Traversal;

    /// <summary>
    /// Add the configured test GTL parsing to the configuration.
    /// </summary>
    public static class TraversalContextConfigurationUseTestParserExtension
    {
        /// <summary>
        /// Use the text parser configured for testing.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalContextConfiguration UseTestTraversalParser<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration)
            where TTraversalContextConfiguration : FunctionalContextConfiguration
        {
#if USE_LAPA_PARSER_IN_TESTS
            return configuration.UseLapaTraversalParser();
#else
            return configuration.UseAntlrTraversalParser();
#endif
        }

    }
}
