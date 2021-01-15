namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalContextConfigurationUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Lapa GTL parsing to the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalContextConfiguration UseLapaTraversalParser<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration)
            where TTraversalContextConfiguration : TraversalContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalContextConfiguration) configuration;
            editableConfiguration.ParserConfigurationProvider = () => new TraversalParserConfiguration().UseLapa();
            editableConfiguration.ProcessorConfigurationProvider = () => new TraversalProcessorConfiguration().UseLapa();

            return configuration;
        }
    }
}
