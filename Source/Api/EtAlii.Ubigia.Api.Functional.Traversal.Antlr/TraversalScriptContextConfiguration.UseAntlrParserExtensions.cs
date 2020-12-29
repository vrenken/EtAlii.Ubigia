namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalScriptContextConfigurationUseLapaParserExtensions
    {
        public static TTraversalScriptContextConfiguration UseAntlrParser<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            var editableConfiguration = (IEditableTraversalScriptContextConfiguration) configuration;
            editableConfiguration.ScriptParserFactory = () => new AntlrScriptParserFactory();
            editableConfiguration.ScriptProcessorFactory = () => new AntlrScriptProcessorFactory();

            return configuration;
        }
    }
}
