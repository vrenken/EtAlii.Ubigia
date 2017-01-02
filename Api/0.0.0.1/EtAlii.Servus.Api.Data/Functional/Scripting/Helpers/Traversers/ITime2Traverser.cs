namespace EtAlii.Servus.Api.Data
{
    internal interface ITime2Traverser
    {
        IReadOnlyEntry Traverse(IReadOnlyEntry entry);
    }
}