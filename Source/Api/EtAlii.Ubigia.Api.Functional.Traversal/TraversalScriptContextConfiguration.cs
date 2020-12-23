namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;
    using System;

    public class TraversalScriptContextConfiguration : LogicalContextConfiguration, ITraversalScriptContextConfiguration, IEditableTraversalScriptContextConfiguration
    {
        IFunctionHandlersProvider IEditableTraversalScriptContextConfiguration.FunctionHandlersProvider { get => FunctionHandlersProvider ; set => FunctionHandlersProvider = value; }
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        IRootHandlerMappersProvider IEditableTraversalScriptContextConfiguration.RootHandlerMappersProvider { get => RootHandlerMappersProvider; set => RootHandlerMappersProvider = value; }
        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public Func<IScriptParserFactory> ScriptParserFactory { get; set; }
        public Func<IScriptProcessorFactory> ScriptProcessorFactory { get; set; }

        public TraversalScriptContextConfiguration()
        {
            FunctionHandlersProvider = EtAlii.Ubigia.Api.Functional.Traversal.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = EtAlii.Ubigia.Api.Functional.Traversal.RootHandlerMappersProvider.Empty;
        }
    }
}
