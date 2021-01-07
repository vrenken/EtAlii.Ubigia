namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalScriptContextConfigurationUseLapaTraversalParserExtensions
    {
        /// <summary>
        /// Add Lapa GTL parsing to the configuration.
        /// </summary>
        /// <param name="configuration"></param>
        /// <typeparam name="TTraversalScriptContextConfiguration"></typeparam>
        /// <returns></returns>
        public static TTraversalScriptContextConfiguration UseLapaTraversalParser<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;
            editableConfiguration.ScriptParserFactory = () => new LapaScriptParserFactory();
            editableConfiguration.ScriptProcessorFactory = () => new LapaScriptProcessorFactory();

            return configuration;
        }
    }
}
