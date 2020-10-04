namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Persistence;

    public interface IFabricContextConfiguration
    {
        IStorage Storage { get; }
        
        IFabricContextConfiguration Use(IStorage storage);
    }
}