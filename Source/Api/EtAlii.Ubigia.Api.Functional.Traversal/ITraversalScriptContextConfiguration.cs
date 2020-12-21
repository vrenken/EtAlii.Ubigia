namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface ITraversalScriptContextConfiguration : IConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
    }
}
