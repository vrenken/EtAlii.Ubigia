namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalScriptContextConfigurationUseLapaParserExtensions
    {
        public static TTraversalScriptContextConfiguration UseLapaParser<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;
            editableConfiguration.ScriptParserFactory = () => new ScriptParserFactory();
            editableConfiguration.ScriptProcessorFactory = () => new ScriptProcessorFactory();

            return configuration;
        }
    }
}
