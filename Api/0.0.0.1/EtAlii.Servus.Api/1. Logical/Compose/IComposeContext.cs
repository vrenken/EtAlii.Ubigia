namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface IComposeContext
    {
        IFabricContext Fabric { get; }
    }
}