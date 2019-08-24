namespace EtAlii.Ubigia.Api.Functional.Scripting
{
    using EtAlii.Ubigia.Api.Logical;

    public class GraphSLScriptContextConfiguration : LogicalContextConfiguration, IGraphSLScriptContextConfiguration, IEditableGraphSLScriptContextConfiguration
    {
        IFunctionHandlersProvider IEditableGraphSLScriptContextConfiguration.FunctionHandlersProvider { get => FunctionHandlersProvider ; set => FunctionHandlersProvider = value; }
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        IRootHandlerMappersProvider IEditableGraphSLScriptContextConfiguration.RootHandlerMappersProvider { get => RootHandlerMappersProvider; set => RootHandlerMappersProvider = value; }
        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public GraphSLScriptContextConfiguration()
        {
            FunctionHandlersProvider = EtAlii.Ubigia.Api.Functional.Scripting.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = EtAlii.Ubigia.Api.Functional.Scripting.RootHandlerMappersProvider.Empty;
        }
    }
}