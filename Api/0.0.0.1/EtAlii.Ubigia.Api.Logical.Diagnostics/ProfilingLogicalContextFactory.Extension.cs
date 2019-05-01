namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    public static class ProfilingLogicalContextFactoryExtension
    {
        public static IProfilingLogicalContext CreateForProfiling(this LogicalContextFactory logicalContextFactory, LogicalContextConfiguration configuration)
        {
            configuration.Use(new ILogicalContextExtension[]
            {
                new ProfilingLogicalContextExtension(), 
            });

            return (IProfilingLogicalContext)logicalContextFactory.Create(configuration);
        }
    }
}