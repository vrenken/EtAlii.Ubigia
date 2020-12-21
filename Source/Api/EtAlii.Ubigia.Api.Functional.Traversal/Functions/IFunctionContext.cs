namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IFunctionContext
    {
        IPathProcessor PathProcessor { get; }
        IItemToIdentifierConverter ItemToIdentifierConverter { get; }
    }
}
