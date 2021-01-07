namespace EtAlii.Ubigia.Api.Functional.Context
{
    using System;

    public static class GraphContextConfigurationUseAntlrParserExtensions
    {
        /// <summary>
        /// Add Antlr GCL parsing to the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TGraphContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TGraphContextConfiguration UseAntlrContextParser<TGraphContextConfiguration>(this TGraphContextConfiguration configuration)
            where TGraphContextConfiguration : GraphContextConfiguration
        {
            configuration.Use(Array.Empty<IGraphContextExtension>());
            // configuration.Use(new IGraphContextExtension[]
            // {
            //     //new LapaGraphContextExtension(configuration),
            // });

            return configuration;
        }
    }
}
