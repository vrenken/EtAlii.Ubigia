namespace EtAlii.Servus.Api.Functional
{
    using EtAlii.Servus.Api.Fabric;

    internal interface ITimeTraverser
    {
        IReadOnlyEntry Traverse(IReadOnlyEntry entry);
    }
}