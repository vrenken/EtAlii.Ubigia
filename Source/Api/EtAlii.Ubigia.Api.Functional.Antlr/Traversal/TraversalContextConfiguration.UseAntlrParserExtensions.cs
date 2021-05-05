namespace EtAlii.Ubigia.Api.Functional.Antlr.Traversal
{
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public static class TraversalContextConfigurationUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Antlr GTL parsing to the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalContextConfiguration UseAntlrTraversalParser<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration)
            where TTraversalContextConfiguration : TraversalContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalContextConfiguration) configuration;
            editableConfiguration.ParserConfigurationProvider = () => new TraversalParserConfiguration().UseAntlr();
            editableConfiguration.ProcessorConfigurationProvider = () => new TraversalProcessorConfiguration().UseAntlr();

            return configuration;
        }
    }
}
