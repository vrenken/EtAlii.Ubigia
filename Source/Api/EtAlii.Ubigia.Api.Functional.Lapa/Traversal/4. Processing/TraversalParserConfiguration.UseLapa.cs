namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalProcessorConfigurationUseLapaExtension
    {
        public static TraversalProcessorConfiguration UseLapa(this TraversalProcessorConfiguration configuration)
        {
            return configuration.Use(new IExtension[]
            {
                new LapaParserExtension(),
                new LapaProcessorExtension(configuration)
            });
        }
    }
}
