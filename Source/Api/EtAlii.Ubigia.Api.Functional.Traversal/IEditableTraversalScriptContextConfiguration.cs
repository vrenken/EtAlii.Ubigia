namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IEditableTraversalScriptContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

    }
}
