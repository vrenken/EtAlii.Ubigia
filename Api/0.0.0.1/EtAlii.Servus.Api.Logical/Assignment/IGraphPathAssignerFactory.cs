namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface IGraphPathAssignerFactory
    {
        IGraphPathAssigner Create(IFabricContext fabric);
    }
}