// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class TraversalScriptContextConfigurationUseTestParserExtension
    {
        /// <summary>
        /// Use the text parser configured for testing.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalScriptContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalScriptContextConfiguration UseTestParser<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            return configuration.UseLapaParser();
        }

    }
}
