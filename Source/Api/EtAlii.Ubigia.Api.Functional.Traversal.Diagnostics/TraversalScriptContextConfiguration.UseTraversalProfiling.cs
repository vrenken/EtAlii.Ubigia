namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalScriptContextConfigurationUseTraversalProfiling
    {
        public static TTraversalScriptContextConfiguration UseTraversalProfiling<TTraversalScriptContextConfiguration>(this TTraversalScriptContextConfiguration configuration)
            where TTraversalScriptContextConfiguration : TraversalScriptContextConfiguration
        {
            configuration.Use(new ITraversalScriptContextExtension[]
            {
                new ProfilingTraversalScriptContextExtension(),
            });

            return configuration;
        }
    }
}
