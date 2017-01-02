namespace EtAlii.Ubigia.Api.Diagnostics.Profiling
{
    using EtAlii.Ubigia.Api.Functional;

    public static class ProfilingDataContextFactoryExtension
    {
        public static IProfilingDataContext CreateForProfiling(this DataContextFactory dataContextFactory, IDataContextConfiguration configuration)
        {
            configuration.Use(new IDataContextExtension[]
            {
                new ProfilingDataContextExtension(),
            });

            return (IProfilingDataContext)dataContextFactory.Create(configuration);
        }
    }
}