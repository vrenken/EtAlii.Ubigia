namespace EtAlii.Ubigia.Api.Functional
{
    public interface IFunctionContext
    {
        IPathProcessor PathProcessor { get; }
        IItemToIdentifierConverter ItemToIdentifierConverter { get; }
    }
}