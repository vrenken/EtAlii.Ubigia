namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface ITraversalContextConfiguration : IConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
    }
}
