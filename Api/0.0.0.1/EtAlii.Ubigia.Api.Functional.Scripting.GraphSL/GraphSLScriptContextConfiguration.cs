namespace EtAlii.Ubigia.Api.Functional
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
            FunctionHandlersProvider = Functional.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = Functional.RootHandlerMappersProvider.Empty;
        }
    }
}