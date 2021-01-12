namespace EtAlii.Ubigia.Api.Functional.Traversal
{
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
            editableConfiguration.ScriptParserFactory = () => new AntlrScriptParserFactory();
            editableConfiguration.ScriptProcessorFactory = () => new AntlrScriptProcessorFactory();

            return configuration;
        }
    }
}
