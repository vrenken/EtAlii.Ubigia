// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    /// <summary>
    /// Add the configured test GTL parsing to the configuration.
    /// </summary>
    public static class TraversalScriptContextConfigurationUseTestParserExtension
    {
        /// <summary>
        /// Use the text parser configured for testing.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalScriptContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalScriptContextConfiguration UseTestTraversalParser<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
#if USE_LAPA_PARSER_IN_TESTS
            return configuration.UseLapaTraversalParser();
#else
            return configuration.UseAntlrTraversalParser();
#endif
        }

    }
}
