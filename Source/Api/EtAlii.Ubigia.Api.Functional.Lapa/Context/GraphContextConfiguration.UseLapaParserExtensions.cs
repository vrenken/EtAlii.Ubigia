namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class GraphContextConfigurationUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Lapa GCL parsing to the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TGraphContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TGraphContextConfiguration UseLapaContextParser<TGraphContextConfiguration>(this TGraphContextConfiguration configuration)
            where TGraphContextConfiguration : FunctionalContextConfiguration
        {
            configuration.Use(new IGraphContextExtension[]
            {
                new LapaGraphContextExtension(configuration),
            });

            return configuration;
        }
    }
}
