namespace EtAlii.Ubigia.Api.Functional.Querying
{
    public static class LinqQueryContextConfigurationUseLinqProfiling
    {
        public static TLinqQueryContextConfiguration UseLinqProfiling<TLinqQueryContextConfiguration>(this TLinqQueryContextConfiguration configuration)
        where TLinqQueryContextConfiguration : LinqQueryContextConfiguration
        {
            configuration.Use(new ILinqQueryContextExtension[]
            {
                new ProfilingLinqQueryContextExtension(), 
            });

            return configuration;
        }
    }
}