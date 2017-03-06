namespace EtAlii.Ubigia.Api.Logical.Diagnostics
{
    using EtAlii.Ubigia.Api.Logical;

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