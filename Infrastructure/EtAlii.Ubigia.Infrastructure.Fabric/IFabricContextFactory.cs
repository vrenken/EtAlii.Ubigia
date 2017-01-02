namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    public interface IFabricContextFactory
    {
        IFabricContext Create(IFabricContextConfiguration configuration);
    }
}