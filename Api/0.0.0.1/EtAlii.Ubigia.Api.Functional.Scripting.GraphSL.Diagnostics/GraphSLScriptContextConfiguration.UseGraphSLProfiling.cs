namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class GraphSLScriptContextConfigurationUseGraphSLProfiling
    {
        public static TGraphSLScriptContextConfiguration UseGraphSLProfiling<TGraphSLScriptContextConfiguration>(this TGraphSLScriptContextConfiguration configuration)
            where TGraphSLScriptContextConfiguration : GraphSLScriptContextConfiguration
        {
            configuration.Use(new IGraphSLScriptContextExtension[]
            {
                new ProfilingGraphSLScriptContextExtension(), 
            });

            return configuration;
        }
    }
}