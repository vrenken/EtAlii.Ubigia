namespace EtAlii.Servus.Infrastructure.Logical
{
    using EtAlii.Servus.Infrastructure.Fabric;

    public interface ILogicalContextConfiguration
    {
        string Name { get; }
        string Address { get; }

        IFabricContext Fabric { get; }

        //ILogicalContextExtension[] Extensions { get; }

        //ILogicalContextConfiguration Use(ILogicalContextExtension[] extensions);

        ILogicalContextConfiguration Use(string name, string address);

        ILogicalContextConfiguration Use(IFabricContext fabricContext);
    }
}