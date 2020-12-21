namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class GraphXLQueryContextConfigurationUseGraphXLProfiling
    {
        public static TGraphXLQueryContextConfiguration UseGraphXLProfiling<TGraphXLQueryContextConfiguration>(this TGraphXLQueryContextConfiguration configuration)
            where TGraphXLQueryContextConfiguration : GraphXLQueryContextConfiguration
        {
            configuration.Use(new IGraphXLQueryContextExtension[]
            {
                new ProfilingGraphXLQueryContextExtension(),
            });

            return configuration;
        }
    }
}
