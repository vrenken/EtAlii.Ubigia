namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalParserConfigurationUseLapaExtension
    {
        public static TraversalParserConfiguration UseLapa(this TraversalParserConfiguration configuration)
        {
            return configuration.Use(new[] {new LapaParserExtension()});
        }
    }
}
