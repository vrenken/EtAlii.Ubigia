namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Functional;

    public static class GraphSLScriptContextProfilingExtension
    {
        public static IGraphSLScriptContext CreateForProfiling(this GraphSLScriptContextFactory factory, IGraphSLScriptContextConfiguration configuration)
        {
            configuration.Use(new IGraphSLScriptContextExtension[]
            {
                new ProfilingGraphSLScriptContextExtension(), 
            });

            return (IProfilingGraphSLScriptContext)factory.Create(configuration);
        }
    }
}