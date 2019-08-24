namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public interface IGraphSLScriptContextConfiguration : IConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; }
        IRootHandlerMappersProvider RootHandlerMappersProvider { get; }
    }
}