namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IFabricContextFactory
    {
        IFabricContext Create(FabricContextConfiguration configuration);
    }
}