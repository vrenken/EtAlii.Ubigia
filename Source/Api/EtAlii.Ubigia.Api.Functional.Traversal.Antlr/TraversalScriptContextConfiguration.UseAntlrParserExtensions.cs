namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalScriptContextConfigurationUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Antlr GTL parsing to the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalScriptContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalScriptContextConfiguration UseAntlrTraversalParser<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;
            editableConfiguration.ScriptParserFactory = () => new AntlrScriptParserFactory();
            editableConfiguration.ScriptProcessorFactory = () => new AntlrScriptProcessorFactory();

            return configuration;
        }
    }
}
