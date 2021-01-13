namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    public interface IEditableTraversalContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        TraversalParserConfiguration ParserConfiguration { get; set; }
        Func<IScriptProcessorFactory> ScriptProcessorFactory { get; set; }
    }
}
