namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class GraphTLQueryContextConfigurationUseGraphTLProfiling
    {
        public static TGraphTLQueryContextConfiguration UseGraphTLProfiling<TGraphTLQueryContextConfiguration>(this TGraphTLQueryContextConfiguration configuration)
            where TGraphTLQueryContextConfiguration : GraphTLQueryContextConfiguration
        {
            configuration.Use(new IGraphTLQueryContextExtension[]
            {
                new ProfilingGraphTLQueryContextExtension(), 
            });

            return configuration;
        }
    }
}