namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalProcessorConfigurationUseAntlrExtension
    {
        public static TraversalProcessorConfiguration UseAntlr(this TraversalProcessorConfiguration configuration)
        {
            return configuration.Use(new IExtension[]
            {
                new AntrlParserExtension(),
                new AntlrProcessorExtension(configuration)
            });
        }
    }
}
