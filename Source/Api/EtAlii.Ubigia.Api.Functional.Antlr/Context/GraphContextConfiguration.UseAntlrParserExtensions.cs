namespace EtAlii.Ubigia.Api.Functional.Antlr.Context
{
    using EtAlii.Ubigia.Api.Functional.Context;

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
            configuration.Use(new IGraphContextExtension[]
            {
                new AntlrGraphContextExtension(configuration),
            });

            return configuration;
        }
    }
}
