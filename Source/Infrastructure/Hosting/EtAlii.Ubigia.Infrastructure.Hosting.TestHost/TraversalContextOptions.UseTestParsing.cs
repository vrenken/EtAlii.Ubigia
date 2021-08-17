// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Antlr;

    /// <summary>
    /// Add the configured test GTL parsing to the options.
    /// </summary>
    public static class TraversalContextOptionsUseTestParsingExtension
    {
        // TODO: is this file in the right project?

        /// <summary>
        /// Use the text parser configured for testing.
        /// </summary>
        /// <param name="options"></param>
        /// <typeparam name="TFunctionalOptions"></typeparam>
        /// <returns></returns>
        public static TFunctionalOptions UseTestParsing<TFunctionalOptions>(this TFunctionalOptions options)
            where TFunctionalOptions : FunctionalOptions
        {
#if USE_LAPA_PARSING_IN_TESTS
            return options.UseLapaParsing);
#else
            return options.UseAntlrParsing();
#endif
        }

    }
}
