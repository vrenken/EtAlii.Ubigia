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
            editableConfiguration.ParserConfiguration = new TraversalParserConfiguration().UseLapa();
            editableConfiguration.ScriptProcessorFactory = () => new LapaScriptProcessorFactory();

            return configuration;
        }
    }
}
