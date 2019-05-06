namespace EtAlii.Ubigia.Api.Functional
{
    public interface IEditableGraphSLScriptContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

    }
}