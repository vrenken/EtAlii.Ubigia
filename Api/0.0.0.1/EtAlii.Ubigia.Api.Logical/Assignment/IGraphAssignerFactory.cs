namespace EtAlii.Ubigia.Api.Logical
{
    using EtAlii.Ubigia.Api.Fabric;

    public interface IGraphAssignerFactory
    {
        IGraphAssigner Create(IFabricContext fabric);
    }
}