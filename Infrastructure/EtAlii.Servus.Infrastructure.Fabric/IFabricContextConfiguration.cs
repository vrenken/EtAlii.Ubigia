namespace EtAlii.Servus.Infrastructure.Fabric
{
    using EtAlii.Servus.Storage;

    public interface IFabricContextConfiguration
    {
        IStorage Storage { get; }

        //IFabricContextExtension[] Extensions { get; }

        //IFabricContextConfiguration Use(IFabricContextExtension[] extensions);

        IFabricContextConfiguration Use(IStorage storage);
    }
}