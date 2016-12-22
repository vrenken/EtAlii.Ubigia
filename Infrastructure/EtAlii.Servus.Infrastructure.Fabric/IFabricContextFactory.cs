namespace EtAlii.Servus.Infrastructure.Fabric
{
    public interface IFabricContextFactory
    {
        IFabricContext Create(IFabricContextConfiguration configuration);
    }
}