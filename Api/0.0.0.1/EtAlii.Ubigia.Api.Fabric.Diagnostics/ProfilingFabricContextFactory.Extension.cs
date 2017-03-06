namespace EtAlii.Ubigia.Api.Fabric.Diagnostics
{
    using EtAlii.Ubigia.Api.Fabric;

    public static class ProfilingFabricContextFactoryExtension
    {
        public static IProfilingFabricContext CreateForProfiling(this FabricContextFactory fabricContextFactory, IFabricContextConfiguration configuration)
        {
            configuration.Use(new IFabricContextExtension[]
            {
                new ProfilingFabricContextExtension(),
            });

            return (IProfilingFabricContext)fabricContextFactory.Create(configuration);
        }
    }
}