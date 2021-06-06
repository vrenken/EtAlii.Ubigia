namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public static class TraversalContextConfigurationUseTraversalProfiling
    {
        public static TTraversalContextConfiguration UseTraversalProfiling<TTraversalContextConfiguration>(this TTraversalContextConfiguration configuration)
            where TTraversalContextConfiguration : FunctionalContextConfiguration
        {
            configuration.Use(new ITraversalContextExtension[]
            {
                new ProfilingTraversalContextExtension(),
            });

            return configuration;
        }
    }
}
