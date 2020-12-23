namespace EtAlii.Ubigia.Api.Functional.Traversal
{
    using System;

    public interface IEditableTraversalScriptContextConfiguration
    {
        IFunctionHandlersProvider FunctionHandlersProvider { get; set; }

        IRootHandlerMappersProvider RootHandlerMappersProvider { get; set; }

        Func<IScriptParserFactory> ScriptParserFactory { get; set; }
        Func<IScriptProcessorFactory> ScriptProcessorFactory { get; set; }
    }
}
