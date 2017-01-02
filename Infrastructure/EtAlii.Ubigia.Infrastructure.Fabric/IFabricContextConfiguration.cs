namespace EtAlii.Ubigia.Infrastructure.Fabric
{
    using EtAlii.Ubigia.Storage;

    public interface IFabricContextConfiguration
    {
        IStorage Storage { get; }

        //IFabricContextExtension[] Extensions { get; }

        //IFabricContextConfiguration Use(IFabricContextExtension[] extensions);

        IFabricContextConfiguration Use(IStorage storage);
    }
}