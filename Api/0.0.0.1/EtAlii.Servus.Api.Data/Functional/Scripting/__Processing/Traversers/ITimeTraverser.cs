namespace EtAlii.Servus.Api.Data
{
    internal interface ITimeTraverser
    {
        IReadOnlyEntry Traverse(IReadOnlyEntry entry);
    }
}