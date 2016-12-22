namespace EtAlii.Servus.Api.Logical
{
    using EtAlii.Servus.Api.Fabric;

    public interface IAssignmentContext
    {
        IFabricContext Fabric { get; }
    }
}