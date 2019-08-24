namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    public interface IEditableGraphSLScriptContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

    }
}