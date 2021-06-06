namespace EtAlii.Ubigia.Api.Functional
{
    using System;
    using EtAlii.Ubigia.Api.Functional.Traversal;

    public interface IEditableFunctionalContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        Func<TraversalParserConfiguration> ParserConfigurationProvider { get; set; }
        Func<TraversalProcessorConfiguration> ProcessorConfigurationProvider { get; set; }
    }
}
