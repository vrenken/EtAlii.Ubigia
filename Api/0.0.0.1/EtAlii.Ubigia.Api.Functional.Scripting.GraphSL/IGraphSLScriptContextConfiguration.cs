namespace EtAlii.Ubigia.Api.Functional
{
    public interface IGraphSLScriptContextConfiguration : IConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
    }
}