namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using EtAlii.Ubigia.Api.Logical;

    public class TraversalContextConfiguration : LogicalContextConfiguration, ITraversalContextConfiguration, IEditableTraversalContextConfiguration
    {
        IFunctionHandlersProvider IEditableTraversalContextConfiguration.FunctionHandlersProvider { get => FunctionHandlersProvider ; set => FunctionHandlersProvider = value; }
        public IFunctionHandlersProvider FunctionHandlersProvider { get; private set; }

        IRootHandlerMappersProvider IEditableTraversalContextConfiguration.RootHandlerMappersProvider { get => RootHandlerMappersProvider; set => RootHandlerMappersProvider = value; }
        public IRootHandlerMappersProvider RootHandlerMappersProvider { get; private set; }

        public TraversalParserConfiguration ParserConfiguration { get; set; }
        public TraversalProcessorConfiguration ProcessorConfiguration { get; set; }

        public TraversalContextConfiguration()
        {
            FunctionHandlersProvider = EtAlii.Ubigia.Api.Functional.Traversal.FunctionHandlersProvider.Empty;
            RootHandlerMappersProvider = EtAlii.Ubigia.Api.Functional.Traversal.RootHandlerMappersProvider.Empty;
        }
    }
}
