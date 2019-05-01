namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class GraphSLScriptContextProfilingExtension
    {
        public static IGraphSLScriptContext CreateForProfiling(this GraphSLScriptContextFactory factory, GraphSLScriptContextConfiguration configuration)
        {
            configuration.Use(new IGraphSLScriptContextExtension[]
            {
                new ProfilingGraphSLScriptContextExtension(), 
            });

            return (IProfilingGraphSLScriptContext)factory.Create(configuration);
        }
    }
}