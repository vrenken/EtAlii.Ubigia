namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    public static class LinqQueryContextProfilingExtension
    {
        public static ILinqQueryContext CreateForProfiling(this LinqQueryContextFactory factory, LinqQueryContextConfiguration configuration)
        {
            configuration.Use(new ILinqQueryContextExtension[]
            {
                new ProfilingLinqQueryContextExtension(), 
            });

            return (IProfilingLinqQueryContext)factory.Create(configuration);
        }
    }
}