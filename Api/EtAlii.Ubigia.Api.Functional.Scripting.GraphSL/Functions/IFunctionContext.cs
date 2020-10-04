namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public interface IFunctionContext
    {
        IPathProcessor PathProcessor { get; }
        IItemToIdentifierConverter ItemToIdentifierConverter { get; }
    }
}