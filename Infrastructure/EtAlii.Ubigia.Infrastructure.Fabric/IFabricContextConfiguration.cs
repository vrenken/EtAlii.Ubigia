namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Storage;

    public interface IFabricContextConfiguration
    {
        IStorage Storage { get; }
        
        IFabricContextConfiguration Use(IStorage storage);
    }
}