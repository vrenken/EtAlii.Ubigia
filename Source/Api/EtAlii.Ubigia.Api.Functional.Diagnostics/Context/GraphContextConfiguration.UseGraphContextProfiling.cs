namespace EtAlii.Ubigia.Api.Functional.Context
{
    public static class GraphContextConfigurationUseGraphContextProfiling
    {
        public static TGraphContextConfiguration UseGraphContextProfiling<TGraphContextConfiguration>(this TGraphContextConfiguration configuration)
            where TGraphContextConfiguration : GraphContextConfiguration
        {
            configuration.Use(new IGraphContextExtension[]
            {
                new ProfilingGraphContextExtension(),
            });

            return configuration;
        }
    }
}
