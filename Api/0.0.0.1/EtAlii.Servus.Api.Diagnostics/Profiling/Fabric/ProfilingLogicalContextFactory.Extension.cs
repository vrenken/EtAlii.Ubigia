namespace EtAlii.Servus.Api.Diagnostics.Profiling
{
    using EtAlii.Servus.Api.Functional;
    using EtAlii.Servus.Api.Logical;

    public static class ProfilingLogicalContextFactoryExtension
    {
        public static IProfilingLogicalContext CreateForProfiling(this LogicalContextFactory logicalContextFactory, ILogicalContextConfiguration configuration)
        {
            configuration.Use(new ILogicalContextExtension[]
            {
                new ProfilingLogicalContextExtension(), 
            });

            return (IProfilingLogicalContext)logicalContextFactory.Create(configuration);
        }
    }
}