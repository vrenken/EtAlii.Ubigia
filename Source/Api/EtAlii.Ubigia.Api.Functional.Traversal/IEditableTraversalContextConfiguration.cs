namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;
    public interface IEditableTraversalContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        Func<TraversalParserConfiguration> ParserConfigurationProvider { get; set; }
        Func<TraversalProcessorConfiguration> ProcessorConfigurationProvider { get; set; }
    }
}
