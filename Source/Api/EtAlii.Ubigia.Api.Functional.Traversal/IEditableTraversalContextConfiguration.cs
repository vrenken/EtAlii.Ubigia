namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    public interface IEditableTraversalContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        TraversalParserConfiguration ParserConfiguration { get; set; }
        TraversalProcessorConfiguration ProcessorConfiguration { get; set; }
    }
}
