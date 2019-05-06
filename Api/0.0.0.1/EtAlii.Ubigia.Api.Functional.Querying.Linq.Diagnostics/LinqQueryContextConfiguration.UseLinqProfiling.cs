namespace EtAlii.Ubigia.Api.Functional.Diagnostics
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