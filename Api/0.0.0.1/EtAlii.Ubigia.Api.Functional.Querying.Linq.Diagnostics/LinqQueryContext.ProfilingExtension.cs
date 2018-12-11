namespace EtAlii.Ubigia.Api.Functional.Diagnostics
{
    using EtAlii.Ubigia.Api.Functional;

    public static class LinqQueryContextProfilingExtension
    {
        public static ILinqQueryContext CreateForProfiling(this LinqQueryContextFactory factory, ILinqQueryContextConfiguration configuration)
        {
            configuration.Use(new ILinqQueryContextExtension[]
            {
                new ProfilingLinqQueryContextExtension(), 
            });

            return (IProfilingLinqQueryContext)factory.Create(configuration);
        }
    }
}