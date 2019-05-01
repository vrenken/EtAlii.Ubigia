namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    public static class ProfilingFabricContextFactoryExtension
    {
        public static IProfilingFabricContext CreateForProfiling(this FabricContextFactory fabricContextFactory, FabricContextConfiguration configuration)
        {
            configuration.Use(new IFabricContextExtension[]
            {
                new ProfilingFabricContextExtension(),
            });

            return (IProfilingFabricContext)fabricContextFactory.Create(configuration);
        }
    }
}