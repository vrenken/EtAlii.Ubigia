// This file seems not in the right place. Should be moved to somewhere better.
// ReSharper disable once CheckNamespace
namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class GraphContextConfigurationUseTestParserExtension
    {
        /// <summary>
        /// Add the configured test GCL parsing to the configuration.
        /// </summary>
        public static TGraphContextConfiguration UseTestContextParser<TGraphContextConfiguration>(this TGraphContextConfiguration configuration)
            where TGraphContextConfiguration : GraphContextConfiguration
        {
#if USE_LAPA_PARSER_IN_TESTS
            return configuration.UseLapaContextParser();
#else
            return configuration.UseAntlrContextParser();
#endif
        }

    }
}
